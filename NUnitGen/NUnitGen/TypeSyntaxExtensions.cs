using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitGen {
    public static class TypeSyntaxExtensions {
        public static string GetTypeName(this TypeSyntax value)
        {// kind of tricky way to get type name
            return value.GetText().ToString().Trim(' ');
        }
    }
}
