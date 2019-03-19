using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitGen {
    public static class ListStringExtensions {
        public static string AsMethodParameters(this List<string> parameters)
        {
            if (parameters.Count == 0)
                return string.Empty;

            var res = string.Empty;
            foreach(var param in parameters)
            {
                res += param;
                res += ",";
            }

            //remove last ',' char
            return res.Substring(0, res.Length - 1); 
        }
    }
}
