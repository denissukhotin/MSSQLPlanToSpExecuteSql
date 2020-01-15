using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MSSQLPlanToSpExecuteSql.Contracts;

namespace MSSQLPlanToSpExecuteSql.Handlers
{
    class BaseParser : IParser
    {
        protected string ParmValue;
        protected string ParmName;
        protected string ParmType;

        public virtual bool TryParseNode(XmlNode xmlNode, out ParmData parmData)
        {
            ParmValue = "";
            ParmName = "";
            ParmType = "";

            parmData = null;

            if (xmlNode.Attributes["ParameterCompiledValue"] != null)
            {
                ParmValue = xmlNode.Attributes["ParameterCompiledValue"].Value;
            }
            else if (xmlNode.Attributes["ParameterRuntimeValue"] != null)
            {
                ParmValue = xmlNode.Attributes["ParameterRuntimeValue"].Value;
            }

            if (string.IsNullOrEmpty(ParmValue))
            {
                return false;
            }

            ParmName = xmlNode.Attributes["Column"].Value;
            
            if (string.IsNullOrEmpty(ParmName))
            {
                return false;
            }

            return true;
        }

        protected ParmData BuildParmData()
        {
            return new ParmData()
            {
                Name = ParmName,
                Value = ParmValue,
                Type = ParmType
            };
        }
    }
}
