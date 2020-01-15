using MSSQLPlanToSpExecuteSql.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MSSQLPlanToSpExecuteSql.Handlers
{
    class ParmNodeHandler
    {
        private IEnumerable<IParser> _parsers;

        public ParmNodeHandler()
        {
            _parsers = PrepareParsers();
        }

        public bool TryGetParmDataFromXmlNode(XmlNode xmlNode, out ParmData parmData)
        {
            parmData = null;

            foreach (var parser in _parsers)
            {
                if (parser.TryParseNode(xmlNode, out parmData))
                {
                    return true;
                }
            }
            return false;
        }

        private IEnumerable<IParser> PrepareParsers()
        {
            return new IParser[]
            {
                new ParserNumber(),
                new ParserBinary(),
                new ParserDateTime(),
                new ParserString(),
                new ParserGuid(),
                new ParserUnknown()
            };
        }
    }
}
