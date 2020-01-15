using MSSQLPlanToSpExecuteSql.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MSSQLPlanToSpExecuteSql.Handlers
{
    interface IParser
    {
        bool TryParseNode(XmlNode xmlNode, out ParmData parmData);
    }
}
