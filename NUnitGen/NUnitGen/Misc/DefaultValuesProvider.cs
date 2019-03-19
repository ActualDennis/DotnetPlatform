using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitGen.Misc {
    public static class DefaultValuesProvider {
        public static string GetDefaultValueOfType(string typeName)
        {
            switch (typeName)
            {
                case "bool":
                    return "false";
                case "char":
                    return "'\0'";
                case "byte":
                case "Int64":
                case "Int32":
                case "Int16":
                case "int":
                case "long":
                case "short":
                    return "0";
                case "string":
                case "String":
                    return "\"\"";
                default:
                    return "null";
            }
        }
    }
}
