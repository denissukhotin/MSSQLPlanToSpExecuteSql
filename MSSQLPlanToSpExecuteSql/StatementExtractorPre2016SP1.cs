using MSSQLPlanToSpExecuteSql.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace MSSQLPlanToSpExecuteSql
{
    class StatementExtractorPre2016SP1 : StatementExtractor
    {
        protected override List<ParmData> ExtractParmsData(XmlNode parameterListNode)
        {
            List<ParmData> result = new List<ParmData>();
            
           foreach (XmlNode parmNode in parameterListNode.ChildNodes)
            {
                string parmValue = "";

                if (parmNode.Attributes["ParameterCompiledValue"] != null)
                {
                    parmValue = parmNode.Attributes["ParameterCompiledValue"].Value;
                }
                else if (parmNode.Attributes["ParameterRuntimeValue"] != null)
                {
                    parmValue = parmNode.Attributes["ParameterRuntimeValue"].Value;
                }

                if (string.IsNullOrEmpty(parmValue))
                {
                    continue;
                }

                string parmName = parmNode.Attributes["Column"].Value;
                string parmType = "UNKNOWN";                

                if (parmValue.Length >= 2
                    && parmValue.Substring(0, 1) == "(" && parmValue.Substring(parmValue.Length - 1, 1) == ")")
                {
                    char[] chars = { '(', ')' };
                    parmValue = parmValue.Trim(chars);

                    short shortVal;
                    int intVal;
                    long longVal;
                    
                    if (parmValue.IndexOf('.') != -1)
                    {
                        parmValue = parmValue.TrimEnd('0').TrimEnd('.');
                        parmType = string.Format("numeric({0},{1})",
                            parmValue.Length - (parmValue.IndexOf('.') != -1 ? 1 : 0),
                            parmValue.IndexOf('.') != -1 ? parmValue.Substring(parmValue.IndexOf('.')).Length - 1 : 0);
                    }
                    else
                    if (short.TryParse(parmValue, out shortVal))
                    {
                        parmType = "smallint";
                    }
                    else if (int.TryParse(parmValue, out intVal))
                    {
                        parmType = "smallint";
                    }
                    else if (long.TryParse(parmValue, out longVal))
                    {
                        parmType = "bigint";
                    }
                }
                else if (parmValue.Length >= 2
                    && parmValue.Substring(0, 2) == "0x")
                {
                    parmType = "varbinary(max)";
                }
                else if (parmValue.Length >= 2
                    && parmValue.Substring(0, 1) == "'" && parmValue.Substring(parmValue.Length - 1, 1) == "'")
                {
                    char[] chars = { '\'', '\'' };
                    DateTime dateTimeVal;
                    string parmValueTrimmed = parmValue.Trim(chars);
                    if (DateTime.TryParse(parmValueTrimmed, out dateTimeVal))
                    {
                        parmType = "datetime";
                        parmValue = "{ts '" + Regex.Replace(parmValueTrimmed, "\\.0*$", "") + "'}";
                    }
                    else
                    {
                        parmType = "varchar(" + parmValue.Length + ")";
                    }
                }
                else if (parmValue.Length >= 8
                    && parmValue.Substring(0, 6) == "{guid'" && parmValue.Substring(parmValue.Length - 2, 2) == "'}")
                {
                    parmType = "uniqueidentifier";
                }                
                else if (parmValue.Length >= 3
                    && parmValue.Substring(0, 2) == "N'" && parmValue.Substring(parmValue.Length - 1, 1) == "'")
                {
                    parmType = "nvarchar(" + parmValue.Length + ")";
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
    }
}
