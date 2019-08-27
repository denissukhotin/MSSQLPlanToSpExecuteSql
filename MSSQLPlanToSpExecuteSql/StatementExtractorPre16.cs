using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MSSQLPlanToSpExecuteSql
{
    class StatementExtractorPre16 : StatementExtractor
    {
        protected override List<ParmData> ExtractParmsData(XmlNode parameterListNode)
        {
            List<ParmData> result = new List<ParmData>();

            foreach (XmlNode parmNode in parameterListNode.ChildNodes)
            {
                string parmName = parmNode.Attributes["Column"].Value;
                string parmType = "UNKNOWN";
                string parmValue = parmNode.Attributes["ParameterCompiledValue"].Value;

                if (parmValue.Substring(0, 1) == "(" && parmValue.Substring(parmValue.Length - 1, 1) == ")")
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
                else if (parmValue.Substring(0, 1) == "'" && parmValue.Substring(parmValue.Length - 1, 1) == "'")
                {
                    char[] chars = { '\'', '\'' };
                    DateTime dateTimeVal;

                    if (DateTime.TryParse(parmValue.Trim(chars), out dateTimeVal))
                    {
                        parmType = "datetime";
                        parmValue = "{ts " + parmValue + "}";
                    }
                    else
                    {
                        parmType = "varchar(" + parmValue.Length + ")";
                    }
                }
                else if (parmValue.Substring(0, 2) == "N'" && parmValue.Substring(parmValue.Length - 1, 1) == "'")
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

            return result;
        }
    }
}
