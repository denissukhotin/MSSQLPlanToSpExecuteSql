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
    class ParserString : BaseParser
    {
        public override bool TryParseNode(XmlNode xmlNode, out ParmData parmData)
        {
            if (base.TryParseNode(xmlNode, out parmData))
            {
                if (ParmValue.Length >= 2
                    && ParmValue.Substring(0, 1) == "'" && ParmValue.Substring(ParmValue.Length - 1, 1) == "'")
                {
                    ParmType = "varchar(" + Math.Max(ParmValue.Length - 2, 1) + ")";
                    parmData = BuildParmData();
                    return true;
                }
                else if (ParmValue.Length >= 3
                    && ParmValue.Substring(0, 2) == "N'" && ParmValue.Substring(ParmValue.Length - 1, 1) == "'")
                {
                    ParmType = "nvarchar(" + Math.Max(ParmValue.Length - 3, 1) + ")";
                    parmData = BuildParmData();
                    return true;
                }
            }
            return false;
        }
    }
}
