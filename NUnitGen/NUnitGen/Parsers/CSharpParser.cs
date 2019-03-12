using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnitGen.Data;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitGen.Parsers {
    public static class CSharpParser {

        public static async Task<Dictionary<string, string>> GenerateNUnitTestClasses(Dictionary<string, string> filesText)
        {
            var result = new Dictionary<string, string>();

            foreach(var item in filesText)
            { 
               var treeRoot = await CSharpSyntaxTree.ParseText(item.Value).GetRootAsync();

                result.Add(item.Key, GenerateTestClass(treeRoot));
            }
        }

        private static List<string> GetClassMethods(ClassDeclarationSyntax Class)
        {
            return new List<string>(
                Class.DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Where(method => method.Modifiers
                .Any(modifier => modifier.ToString() == "public"))
                .Select(element => element.Identifier.ToString()));
        }

        private static string GenerateTestClass(SyntaxNode root)
        {
            var classesInfo = new List<ClassMetadata>();

            var classes = new List<ClassDeclarationSyntax>(root.DescendantNodes().OfType<ClassDeclarationSyntax>());

            foreach(var Class in classes)
            {
                classesInfo.Add(new ClassMetadata()
                {
                    Methods = GetClassMethods(Class),
                    Name = Class.Identifier.ToString(),
                    NameSpace = DefaultValues.DefaultNameSpace
                });
            }

            
        }
    }
}
