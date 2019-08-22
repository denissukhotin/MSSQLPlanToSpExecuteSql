using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MSSQLPlanToSpExecuteSql
{
    class StatementExtractor
    {
        private XmlDocument PlanXml;
        private XmlNamespaceManager XmlnsManager;

        private StatementExtractor()
        {

        }

        public StatementExtractor(XmlDocument planXml)
        {
            PlanXml = planXml;
            XmlnsManager = new XmlNamespaceManager(PlanXml.NameTable);
            XmlnsManager.AddNamespace("p", "http://schemas.microsoft.com/sqlserver/2004/07/showplan");
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

                        parmList += (string.IsNullOrEmpty(parmList) ? "" : ",");
                        parmList += parmName + " " + parmType;

                        parmValues += (string.IsNullOrEmpty(parmValues) ? "" : "," + Environment.NewLine);
                        parmValues += parmName + " = " + parmValue;
                    }
                }

                sql = "exec sp_executesql N'" + statementText + "'";
                sql += (string.IsNullOrEmpty(parmList) ? "" : "," + Environment.NewLine + "@params = N'" + parmList + "'");
                sql += (string.IsNullOrEmpty(parmValues) ? "" : "," + Environment.NewLine) + parmValues;
            }

            return sql;
        }

        public static List<string> ConvertPlanToStatementList(XmlDocument planXml)
        {
            var extractor = new StatementExtractor(planXml);
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
