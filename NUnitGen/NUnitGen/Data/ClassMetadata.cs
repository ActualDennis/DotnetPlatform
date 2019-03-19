using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitGen.Data {
    public class ClassMetadata {
        public List<MethodMetadata> Methods { get; set; }

        public string Name { get; set; }

        public string NameSpace { get; set; }

        public TypeSyntax TypeInfo { get; set; }

        public IEnumerable<ParameterMetadata> Dependencies { get; set; }
    }
}
