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

        private static List<MethodMetadata> GetClassMethods(ClassDeclarationSyntax Class)
        {
            var methods = Class.DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Where(method => method.Modifiers
                .Any(modifier => modifier.ToString() == "public"));

            var result = new List<MethodMetadata>();

            foreach(var method in methods)
            {
                result.Add(new MethodMetadata() { Name = method.Identifier.ToString(), ReturnType = method.ReturnType, parameters = GetParametersMetadata(method) });
            }

            return result;
        }

        private static List<ParameterMetadata> GetParametersMetadata(MethodDeclarationSyntax method)
        {
            return method.ParameterList.Parameters.Select(param => new ParameterMetadata()
            { Name = ((IdentifierNameSyntax)param.Type)
                .Identifier
                .Value
                .ToString(),
                Type = param.Type
            }).ToList();
        }

        private static IEnumerable<ParameterMetadata> GetClassDependencies(ClassDeclarationSyntax Class)
        {
            var constructors = Class.DescendantNodes()
                .OfType<ConstructorDeclarationSyntax>()
                .Where(method => method.Modifiers
                .Any(modifier => modifier.ToString() == "public"));

            foreach(var constructor in constructors)
            {
                var dependencies = constructor.ParameterList.Parameters.Where(param => 
                ((IdentifierNameSyntax)param.Type)
                .Identifier
                .Value
                .ToString()
                .StartsWith("I"));

                if (!dependencies.Count().Equals(0))
                    return dependencies.Select(param => new ParameterMetadata()
                    {
                        Name = param.Identifier.Value.ToString(),
                        Type = param.Type
                    });
            }

            // no dependencies were found

            return null;
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
                    NameSpace = ((NamespaceDeclarationSyntax)Class.Parent).Name.ToString(),
                    Dependencies = GetClassDependencies(Class),
                    //TypeInfo = Class.Type
                    
                });
            }

            return TestClassesGenerator.Generate(classesInfo);
        }
    }
}
