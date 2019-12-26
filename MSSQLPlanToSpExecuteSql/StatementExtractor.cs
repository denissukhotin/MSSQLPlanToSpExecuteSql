using MSSQLPlanToSpExecuteSql.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace MSSQLPlanToSpExecuteSql
{
    class StatementExtractor
    {
        protected XmlDocument PlanXml;
        protected XmlNamespaceManager XmlnsManager;

        private static readonly Version version2016SP1 = new Version("13.0.4001.0");

        protected StatementExtractor()
        {
        }

        static StatementExtractor Create(XmlDocument planXml)
        {
            XmlNamespaceManager xmlnsManager = new XmlNamespaceManager(planXml.NameTable);
            xmlnsManager.AddNamespace("p", "http://schemas.microsoft.com/sqlserver/2004/07/showplan");

            XmlNode showPlanNode = planXml.SelectSingleNode("/p:ShowPlanXML", xmlnsManager);

            if (showPlanNode == null)
            {
                throw new InvalidOperationException("ShowPlanXML node missing.");
            }

            StatementExtractor extractor = null;

            var version = new Version(showPlanNode.Attributes["Build"].Value);

            if (version >= version2016SP1)
            {
                extractor = new StatementExtractor()
                {
                    PlanXml = planXml,
                    XmlnsManager = xmlnsManager
                }; 
            }
            else
            {
                extractor = new StatementExtractorPre2016SP1()
                {
                    PlanXml = planXml,
                    XmlnsManager = xmlnsManager
                };

            }

            return extractor;
        }

        public List<Statement> Convert()
        {
            List<Statement> results = new List<Statement>();

            var nodes = PlanXml.SelectNodes("//p:Statements", XmlnsManager);
            foreach (XmlNode node in nodes)
            {
                foreach(XmlNode nodeStatement in node.ChildNodes)
                {
                    Statement statement = ConvertStatement(nodeStatement);
                    results.Add(statement);
                }
            }

            return results;
        }

        private Statement ConvertStatement(XmlNode node)
        {
            string statementText = node.Attributes["StatementText"].Value;
            List<ParmData> parmDataList = null;

            if (!string.IsNullOrEmpty(statementText))
            {
                if (statementText[0] == '(')
                {
                    var stack = new Stack<int>();
                    int i = 0;

                    while (i < statementText.Length)
                    {
                        switch (statementText[i])
                        {
                            case '(':
                                stack.Push(i);
                                break;

                            case ')':
                                stack.Pop();
                                break;

                            default:
                                break;
                        }

                        if (!stack.Any())
                        {
                            break;
                        }
                        i++;
                    }

                    statementText = statementText.Substring(i + 1, statementText.Length - i - 1);
                }

                XmlNode parameterListNode = null;

                switch (node.Name)
                {
                    case "StmtCursor":
                        parameterListNode = node.SelectSingleNode("p:CursorPlan/p:Operation/p:QueryPlan/p:ParameterList", XmlnsManager);
                        break;

                    default:
                        parameterListNode = node.SelectSingleNode("p:QueryPlan/p:ParameterList", XmlnsManager);
                        break;
                }
                if (parameterListNode != null)
                {
                    parmDataList = ExtractParmsData(parameterListNode);
                }
            }

            return new Statement()
            {
                SpExecSql = BuildSpExecSql(statementText, parmDataList),
                DirectSql = BuildDirectSql(statementText, parmDataList)
            };
        }

        protected virtual List<ParmData> ExtractParmsData(XmlNode parameterListNode)
        {
            List<ParmData> result = new List<ParmData>();

            foreach (XmlNode parmNode in parameterListNode.ChildNodes)
            {
                string parmValue = "";

                string parmName = parmNode.Attributes["Column"].Value;
                string parmType = parmNode.Attributes["ParameterDataType"].Value;

                if (parmNode.Attributes["ParameterCompiledValue"] != null)
                {
                    parmValue = parmNode.Attributes["ParameterCompiledValue"].Value;
                }
                else if (parmNode.Attributes["ParameterRuntimeValue"] != null)
                {
                    parmValue = parmNode.Attributes["ParameterRuntimeValue"].Value;
                }

                if (parmValue[0] == '(')
                {
                    char[] chars = { '(', ')' };
                    parmValue = parmValue.Trim(chars);
                }
                if (parmType.StartsWith("datetime"))
                {
                    char[] chars = { '\'', '\'' };
                    DateTime dateTimeVal;
                    string parmValueTrimmed = parmValue.Trim(chars);
                    if (DateTime.TryParse(parmValueTrimmed, out dateTimeVal))
                    {
                        parmValue = "{ts '" + Regex.Replace(parmValueTrimmed, "\\.0*$", "") + "'}";
                    }
                }

                result.Add(new ParmData()
                {
                    Name = parmName,
                    Type = parmType,
                    Value = parmValue
                });
            }

            result.Reverse();

            return result;
        }

        private string BuildSpExecSql(string statementText, List<ParmData> parmDataList)
        {
            string parmList = "";
            string parmValues = "";
            string sql = "";

            if (parmDataList != null)
            {
                parmList = string.Join(",", parmDataList.Select(p => p.Name + " " + p.Type));
                parmValues = string.Join("," + Environment.NewLine, parmDataList.Select(p => p.Name + " = " + p.Value));
            }

            sql = "exec sp_executesql N'" + statementText.Replace("'", "''") + "'";
            sql += (string.IsNullOrEmpty(parmList) ? "" : "," + Environment.NewLine + "@params = N'" + parmList + "'");
            sql += (string.IsNullOrEmpty(parmValues) ? "" : "," + Environment.NewLine) + parmValues;

            return sql;
        }
        private string BuildDirectSql(string statementText, List<ParmData> parmDataList)
        {
            if (parmDataList == null)
            {
                return statementText;
            }

            string sWork = statementText;
            int startIdx = sWork.IndexOf("'");
            int endIdx = 0;
            Dictionary<string, string> literals = new Dictionary<string, string>();
            int occurance = 0;

            while (startIdx != -1)
            {
                endIdx = sWork.IndexOf("'", startIdx + 1);
                while (endIdx > 0 && endIdx < sWork.Length - 1 && sWork[endIdx + 1] == '\'')
                {
                    endIdx = sWork.IndexOf("'", endIdx + 2);
                }

                if (endIdx == -1)
                {
                    return "Failed to put parameters: " + statementText;
                }

                occurance++;
                string placeholder = string.Format("#lit{0}#", occurance);
                literals.Add(placeholder, sWork.Substring(startIdx, endIdx - startIdx + 1));

                sWork = sWork.Remove(startIdx, endIdx - startIdx + 1);
                sWork = sWork.Insert(startIdx, placeholder);

                startIdx = sWork.IndexOf("'");
            }

            var variableValidChars = @"@[\p{L}{\p{Nd}}$#_][\p{L}{\p{Nd}}@$#_]*";

            foreach (Match m in Regex.Matches(sWork, variableValidChars).Cast<Match>().OrderByDescending(m => m.Index))
            {
                var paramName = m.Value;
                
                if (parmDataList.Exists(p => p.Name == paramName))
                {
                    var parmData = parmDataList.First(p => p.Name == paramName);

                    sWork = sWork.Remove(m.Index, paramName.Length);
                    sWork = sWork.Insert(m.Index, parmData.Value);
                }                
            }

            var literalsEnum = literals.GetEnumerator();
            while (literalsEnum.MoveNext())
            {
                sWork = sWork.Replace(literalsEnum.Current.Key, literalsEnum.Current.Value);
            }

            return sWork;
        }

        public static List<Statement> ConvertPlanToStatementList(XmlDocument planXml)
        {
            var extractor = Create(planXml);
            return extractor.Convert();
        }

        public static List<Statement> ConvertPlanToStatementList(string planXmlStr)
        {
            XmlDocument planXml = new XmlDocument();
            planXml.LoadXml(planXmlStr);

            return ConvertPlanToStatementList(planXml);
        }
    }
}
