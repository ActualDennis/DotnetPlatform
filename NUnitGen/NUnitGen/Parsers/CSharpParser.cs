using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnitGen.CodeGenerators;
using NUnitGen.Data;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitGen.Parsers {
    public static class CSharpParser {

        public static async Task<Tuple<string, string>> GenerateNUnitTestClasses(Tuple<string, string> fileText)
        {
            var treeRoot = await CSharpSyntaxTree.ParseText(fileText.Item2).GetRootAsync();

            return new Tuple<string,string>(
                Path.GetFileNameWithoutExtension(fileText.Item1) + "NUnitTest.cs",
                GenerateTestClass(treeRoot));
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
            //there may be more than one class in one file
            var classesInfo = new List<ClassMetadata>();

            var classes = new List<ClassDeclarationSyntax>(root.DescendantNodes().OfType<ClassDeclarationSyntax>());

            foreach(var Class in classes)
            {
                classesInfo.Add(new ClassMetadata()
                {
                    Methods = GetClassMethods(Class),
                    Name = Class.Identifier.ToString(),
                    NameSpace = ((NamespaceDeclarationSyntax)Class.Parent).Name.ToString()
                });
            }

            return TestClassesGenerator.Generate(classesInfo);
        }
    }
}
