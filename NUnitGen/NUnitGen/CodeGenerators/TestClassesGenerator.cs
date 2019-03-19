using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnitGen.Data;
using NUnitGen.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace NUnitGen.CodeGenerators {
    public class TestClassesGenerator {

        private static string variableNameAlphabet => "abcdefghijklmABCDEFGHIJKLMNOPQRSTUVWXYZnopqrst0123456789uvwxyz";

        public static string Generate(List<ClassMetadata> classInfo)
        {
            var result = string.Empty;
            foreach (ClassMetadata Class in classInfo)
            {
                NamespaceDeclarationSyntax namespaceDeclaration = NamespaceDeclaration(
                    QualifiedName(IdentifierName(Class.NameSpace), IdentifierName("NUnitTests")));

                CompilationUnitSyntax testClass = CompilationUnit()
                    .WithUsings(GetDefaultUsings(Class))
                    .WithMembers(SingletonList<MemberDeclarationSyntax>(
                        namespaceDeclaration
                        .WithMembers(SingletonList<MemberDeclarationSyntax>(

                            ClassDeclaration(Class.Name + "NUnitTests")

                            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))

                            .WithAttributeLists(
                                SingletonList(
                                    AttributeList(
                                        SingletonSeparatedList(
                                            Attribute(
                                                IdentifierName("TestFixture"))))))
                            .WithMembers(GetMethodsAndProperties(Class))))));

                result += testClass.NormalizeWhitespace().ToFullString();
            }

            return result;
        }

        private static SyntaxList<MemberDeclarationSyntax> GetTestClassDependencies(ClassMetadata Class)
        {
            var result = new List<MemberDeclarationSyntax>();

            result.Add(PropertyDeclaration(
                ParseTypeName(Class.Name), Class.Name.GetTestClassName())
                 .AddModifiers(Token(SyntaxKind.PrivateKeyword))
                .AddAccessorListAccessors(
                    AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                    AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                ));

            if (Class.Dependencies == null)
                return new SyntaxList<MemberDeclarationSyntax>(result);

            foreach (var dependency in Class.Dependencies)
            {
                result.Add(
                    PropertyDeclaration(ParseTypeName(dependency.Type.GetTypeName().GetMockObjectDeclaration()), dependency.Name)
                    .AddModifiers(Token(SyntaxKind.PrivateKeyword))
                    .AddAccessorListAccessors(
                    AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                    AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                ));
            }

            return new SyntaxList<MemberDeclarationSyntax>(result);
        }

        private static SyntaxList<UsingDirectiveSyntax> GetDefaultUsings(ClassMetadata Class)
        {
            return new SyntaxList<UsingDirectiveSyntax>(new List<UsingDirectiveSyntax>()
            {
                 UsingDirective(IdentifierName("System")),
                 UsingDirective(IdentifierName("Moq")),
                 UsingDirective
                 (
                    QualifiedName
                    (
                        IdentifierName("NUnit"),
                        IdentifierName("Framework")
                    )
                 ),
                 UsingDirective
                 (
                    IdentifierName(Class.NameSpace)
                 )
            }
            );
           
        }

        private static SyntaxList<MemberDeclarationSyntax> GetMethodsAndProperties(ClassMetadata Class)
        {
            var methodsAndProperties = new List<MemberDeclarationSyntax>(GetTestClassDependencies(Class));

            //setup method

            methodsAndProperties.Add(
                MethodDeclaration(
            PredefinedType(
                Token(SyntaxKind.VoidKeyword)),
            Identifier("Setup"))
            .WithAttributeLists(
                SingletonList(
                    AttributeList(
                        SingletonSeparatedList(
                            Attribute(
                                IdentifierName("SetUp"))))))
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithBody(GetSetupMethodBody(Class))
            );

            //every other method

            foreach (var method in Class.Methods)
            {
                methodsAndProperties.Add(MethodDeclaration(
                PredefinedType(
                    Token(SyntaxKind.VoidKeyword)),
                Identifier(method.Name + "Test"))
                .WithAttributeLists(
                    SingletonList(
                        AttributeList(
                            SingletonSeparatedList(
                                Attribute(
                                    IdentifierName("Test"))))))
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithBody(GetTestMethodBody(method, Class)));
            }
             
            return new SyntaxList<MemberDeclarationSyntax>(methodsAndProperties);
        }

        private static BlockSyntax GetTestMethodBody(MethodMetadata method, ClassMetadata Class)
        {
            var statements = new List<StatementSyntax>();

            int paramCounter = 0;

            var parametersNames = new List<string>();

            //Arrange

            foreach (var parameter in method.parameters)
            {
                var variableName = GenerateVariableName(8);

                statements.Add(ParseStatement(
                    parameter.Type.GetTypeName() 
                    + " "
                    + variableName
                    + " = "
                    + DefaultValuesProvider.GetDefaultValueOfType(parameter.Type.GetTypeName())
                    + ";"
                    ));

                parametersNames.Add(variableName);

                ++paramCounter;
            }


            if (method.ReturnType.GetTypeName() == "void")
            {
                statements.Add(ParseStatement($"Assert.DoesNotThrow({Class.Name.GetTestClassName()}.{method.Name}({parametersNames.AsMethodParameters()}));"));
                return Block(statements);
            }

            //Act

            statements.Add(ParseStatement(
                method.ReturnType.GetTypeName() 
                + " " + "actual = " 
                + Class.Name.GetTestClassName() 
                + "." 
                + method.Name 
                + "(" + parametersNames.AsMethodParameters() + ");"));

            //Assert

            statements.Add(ParseStatement(
                method.ReturnType.GetTypeName()
                + " expected"
                + " = "
                + DefaultValuesProvider.GetDefaultValueOfType(method.ReturnType.GetTypeName())
                + ";"
                ));

            statements.Add(ParseStatement(
               "Assert.That(actual, Is.EqualTo(expected));"));

            statements.Add(ParseStatement(
              "Assert.Fail(\"autogenerated\");"));

            return Block(statements);
        }

        private static BlockSyntax GetSetupMethodBody(ClassMetadata Class)
        {
            if (Class.Dependencies == null || Class.Dependencies.Count() == 0)
                return Block();

            var statements = new List<StatementSyntax>();

                        foreach (var dependency in Class.Dependencies)
            {
                statements.Add(ParseStatement(dependency.Name + " = new " + dependency.Type.GetTypeName().GetMockObjectDeclaration() + "();"));
            }

            statements.Add(ParseStatement(Class.Name.GetTestClassName() + " = new " + Class.Name + "("+ Class.Dependencies.Select(x =>x.Name).ToList().AsMethodParameters()  +");"));

            return Block(new SyntaxList<StatementSyntax>(statements));           
        }

        private static string GenerateVariableName(int length)
        {
            var builder = new StringBuilder();

            for(int i = 0; i < length; ++i)
            {
                builder.Append(variableNameAlphabet[new Random(Guid.NewGuid().GetHashCode()).Next(0, 120000000) % variableNameAlphabet.Length]);
            }
            // just to make sure that it'll be identifier name
            return builder.ToString().Replace(" ", "").Insert(0, "W");
        }
    }
}
