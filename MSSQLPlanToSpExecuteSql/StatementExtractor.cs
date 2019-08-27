using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MSSQLPlanToSpExecuteSql
{
    class StatementExtractor
    {
        protected XmlDocument PlanXml;
        protected XmlNamespaceManager XmlnsManager;

        private static readonly Version version16 = new Version("1.6");

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

            var version = new Version(showPlanNode.Attributes["Version"].Value);

            if (version >= version16)
            {
                extractor = new StatementExtractor()
                {
                    PlanXml = planXml,
                    XmlnsManager = xmlnsManager
                }; 
            }
            else
            {
                extractor = new StatementExtractorPre16()
                {
                    PlanXml = planXml,
                    XmlnsManager = xmlnsManager
                };

            }

            return extractor;
        }

        public List<string> Convert()
        {
            List<string> results = new List<string>();

            var nodes = PlanXml.SelectNodes("//p:Statements", XmlnsManager);
            foreach (XmlNode node in nodes)
            {
                foreach(XmlNode nodeStatement in node.ChildNodes)
                {
                    string statement = ConvertStatement(nodeStatement);
                    if (! string.IsNullOrEmpty(statement))
                    {
                        results.Add(statement);
                    }
                }
            }

            return results;
        }

        private string ConvertStatement(XmlNode node)
        {
            string statementText = node.Attributes["StatementText"].Value;
            string sql = "";

            if (!string.IsNullOrEmpty(statementText))
            {
                statementText = statementText.Replace("'", "''");
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

                string parmList = "";
                string parmValues = "";

                if (parameterListNode != null)
                {
                    List<ParmData> parmDataList = ExtractParmsData(parameterListNode);

                    parmList = string.Join(",", parmDataList.Select(p => p.Name + " " + p.Type));
                    parmValues = string.Join("," + Environment.NewLine, parmDataList.Select(p => p.Name + " = " + p.Value));                    
                }

                sql = "exec sp_executesql N'" + statementText + "'";
                sql += (string.IsNullOrEmpty(parmList) ? "" : "," + Environment.NewLine + "@params = N'" + parmList + "'");
                sql += (string.IsNullOrEmpty(parmValues) ? "" : "," + Environment.NewLine) + parmValues;
            }

            return sql;
        }

        protected virtual List<ParmData> ExtractParmsData(XmlNode parameterListNode)
        {
            List<ParmData> result = new List<ParmData>();

            foreach (XmlNode parmNode in parameterListNode.ChildNodes)
            {
                string parmName = parmNode.Attributes["Column"].Value;
                string parmType = parmNode.Attributes["ParameterDataType"].Value;
                string parmValue = parmNode.Attributes["ParameterCompiledValue"].Value;

                if (parmValue[0] == '(')
                {
                    char[] chars = { '(', ')' };
                    parmValue = parmValue.Trim(chars);
                }

                result.Add(new ParmData()
                {
                    Name = parmName,
                    Type = parmType,
                    Value = parmValue
                });
            }

            return result;
        }

        public static List<string> ConvertPlanToStatementList(XmlDocument planXml)
        {
            var extractor = Create(planXml);
            return extractor.Convert();
        }

        public static List<string> ConvertPlanToStatementList(string planXmlStr)
        {
            XmlDocument planXml = new XmlDocument();
            planXml.LoadXml(planXmlStr);

            return ConvertPlanToStatementList(planXml);
        }
    }
}
