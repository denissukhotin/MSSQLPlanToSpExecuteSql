using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MSSQLPlanToSpExecuteSql.Contracts;

namespace MSSQLPlanToSpExecuteSql.Handlers
{
    class ParserBinary : BaseParser
    {
        public override bool TryParseNode(XmlNode xmlNode, out ParmData parmData)
        {
            if (base.TryParseNode(xmlNode, out parmData))
            {
                if (ParmValue.Length >= 2
                    && ParmValue.Substring(0, 2) == "0x")
                {
                    ParmType = "varbinary(max)";

                    parmData = BuildParmData();
                    return true;
                }
            }
            return false;
        }
    }
}
