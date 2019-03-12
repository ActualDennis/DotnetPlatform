﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnitGen.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace NUnitGen.CodeGenerators {
    public class TestClassesGenerator {
        public static string Generate(List<ClassMetadata> classInfo)
        {
            var result = string.Empty;
            foreach (ClassMetadata Class in classInfo)
            {
                NamespaceDeclarationSyntax namespaceDeclaration = NamespaceDeclaration(
                    QualifiedName(IdentifierName(Class.Name), IdentifierName("NUnitTests")));

                CompilationUnitSyntax testClass = CompilationUnit()
                    .WithUsings(GetDefaultUsings(Class))
                    .WithMembers(SingletonList<MemberDeclarationSyntax>(
                        namespaceDeclaration
                        .WithMembers(SingletonList<MemberDeclarationSyntax>(ClassDeclaration(Class.Name + "NUnitTests")
                            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                            .WithMembers(GetMethods(Class))))));

                result += testClass.NormalizeWhitespace().ToFullString();
            }

            return result;
        }

        private static SyntaxList<UsingDirectiveSyntax> GetDefaultUsings(ClassMetadata Class)
        {
            return new SyntaxList<UsingDirectiveSyntax>(new List<UsingDirectiveSyntax>()
            {
                 UsingDirective(IdentifierName("System")),
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

        private static SyntaxList<MemberDeclarationSyntax> GetMethods(ClassMetadata Class)
        {
            var result = new List<MemberDeclarationSyntax>
            {
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
            .WithBody(Block())
            };

            foreach (var method in Class.Methods)
            {
                result.Add(MethodDeclaration(
                PredefinedType(
                    Token(SyntaxKind.VoidKeyword)),
                Identifier(method + "Test"))
                .WithAttributeLists(
                    SingletonList(
                        AttributeList(
                            SingletonSeparatedList(
                                Attribute(
                                    IdentifierName("Test"))))))
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithBody(Block(ParseStatement("Assert.Fail(\"Autogenerated.\");"))));
            }
             
            return new SyntaxList<MemberDeclarationSyntax>(result);
        }
    }
}
