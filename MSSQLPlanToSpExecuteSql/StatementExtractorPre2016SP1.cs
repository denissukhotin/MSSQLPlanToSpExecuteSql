using MSSQLPlanToSpExecuteSql.Contracts;
using MSSQLPlanToSpExecuteSql.Handlers;
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
            var result = new List<ParmData>();
            var parmNodeHandler = new ParmNodeHandler();
            
            foreach (XmlNode parmNode in parameterListNode.ChildNodes)
            {
                if (parmNodeHandler.TryGetParmDataFromXmlNode(parmNode, out ParmData parmData))
                {
                    result.Add(parmData);
                }                
            }

            result.Reverse();

            return result;
        }
    }
}
