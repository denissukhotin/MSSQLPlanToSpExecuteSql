using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MSSQLPlanToSpExecuteSql.Contracts;

namespace MSSQLPlanToSpExecuteSql.Handlers
{
    class ParserUnknown : BaseParser
    {
        public override bool TryParseNode(XmlNode xmlNode, out ParmData parmData)
        {
            if (base.TryParseNode(xmlNode, out parmData))
            {
                ParmType = "UNKNOWN";
                parmData = new ParmData()
                {
                    Name = ParmName,
                    Value = ParmValue,
                    Type = ParmType
                };
                return true;
            }
            return false;
        }
    }
}
