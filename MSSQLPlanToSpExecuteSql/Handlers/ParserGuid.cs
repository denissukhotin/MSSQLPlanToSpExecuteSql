using MSSQLPlanToSpExecuteSql.Contracts;
using System;
using System.Xml;

namespace MSSQLPlanToSpExecuteSql.Handlers
{
    class ParserGuid : BaseParser
    {
        public override bool TryParseNode(XmlNode xmlNode, out ParmData parmData)
        {
            if (base.TryParseNode(xmlNode, out parmData))
            {
                if (ParmValue.Length >= 8
                    && ParmValue.Substring(0, 6) == "{guid'" && ParmValue.Substring(ParmValue.Length - 2, 2) == "'}")
                {
                    ParmType = "uniqueidentifier";
                    parmData = BuildParmData();
                    return true;
                }
            }
            return false;
        }
    }
}
