using MSSQLPlanToSpExecuteSql.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace MSSQLPlanToSpExecuteSql.Handlers
{
    class ParserDateTime : BaseParser
    {
        public override bool TryParseNode(XmlNode xmlNode, out ParmData parmData)
        {
            if (base.TryParseNode(xmlNode, out parmData))
            {
                if (ParmValue.Length >= 2
                    && ParmValue.Substring(0, 1) == "'" && ParmValue.Substring(ParmValue.Length - 1, 1) == "'")
                {
                    char[] chars = { '\'', '\'' };
                    DateTime dateTimeVal;
                    string parmValueTrimmed = ParmValue.Trim(chars);
                    if (DateTime.TryParse(parmValueTrimmed, out dateTimeVal))
                    {
                        ParmType = "datetime";
                        parmValueTrimmed = "{ts '" + Regex.Replace(parmValueTrimmed, "\\.0*$", "") + "'}";

                        parmData = BuildParmData();
                        parmData.Value = parmValueTrimmed;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
