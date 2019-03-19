using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitGen.Data {
    public class MethodMetadata {
        public string Name;
        public TypeSyntax ReturnType;
        public List<ParameterMetadata> parameters;
    }
}
