using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MSSQLPlanToSpExecuteSql.Contracts;

namespace MSSQLPlanToSpExecuteSql.Handlers
{
    class ParserNumber : BaseParser
    {
        public override bool TryParseNode(XmlNode xmlNode, out ParmData parmData)
        {
            if (base.TryParseNode(xmlNode, out parmData))
            {
                if (ParmValue.Length >= 2
                    && ParmValue.Substring(0, 1) == "(" && ParmValue.Substring(ParmValue.Length - 1, 1) == ")")
                {
                    string parsedValue = ParmValue;
                        
                    char[] chars = { '(', ')' };
                    parsedValue = parsedValue.Trim(chars);

                    short shortVal;
                    int intVal;
                    long longVal;

                    if (parsedValue.IndexOf('.') != -1)
                    {
                        parsedValue = parsedValue.TrimEnd('0').TrimEnd('.');
                        ParmType = string.Format("numeric({0},{1})",
                            parsedValue.Length - (parsedValue.IndexOf('.') != -1 ? 1 : 0),
                            parsedValue.IndexOf('.') != -1 ? parsedValue.Substring(parsedValue.IndexOf('.')).Length - 1 : 0);
                    }
                    else
                    if (short.TryParse(parsedValue, out shortVal))
                    {
                        ParmType = "smallint";
                    }
                    else if (int.TryParse(parsedValue, out intVal))
                    {
                        ParmType = "smallint";
                    }
                    else if (long.TryParse(parsedValue, out longVal))
                    {
                        ParmType = "bigint";
                    }

                    parmData = BuildParmData();
                    parmData.Value = parsedValue;
                    return true;
                }
            }
            return false;
        }
    }
}
