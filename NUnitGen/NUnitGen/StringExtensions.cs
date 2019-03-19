using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitGen {
    public static class StringExtensions {

        /// <summary>
        /// Gets name of class to be tested, and returns its name ,
        /// which'll be used inside NUnit test class as a private field.
        /// </summary>
        /// <returns></returns>
        public static string GetTestClassName(this string className)
        {
            return "_" + className.ToLower()[0] + className.Substring(1);
        }

        /// <summary>
        /// Gets name of dependency and returns its mock object representation
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns></returns>
        public static string GetMockObjectDeclaration(this string dependency)
        {
            return "Mock" + "<" + dependency + ">";
        }
    }
}
