using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Builder
{
    /// <summary>
    /// Help class to build a method
    /// </summary>
    public class MethodBuilder
    {
        private readonly string name;
        private Type returnType;
        private SyntaxList<AttributeListSyntax> attributes;
        private ParameterListSyntax parameters;
        private BlockSyntax body;
        private string comment;
        private SyntaxTokenList modifiers;

        public MethodBuilder(string name)
        {
            this.name = name.Replace(" ", "_");
        }

        /// <summary>
        /// Set parameters for method
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public MethodBuilder WithParameters(ParameterListSyntax parameters)
        {
            this.parameters = parameters;
            return this;
        }

        /// <summary>
        /// Set return type of method 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public MethodBuilder WithReturnType(Type type)
        {
            returnType = type;
            return this;
        }

        /// <summary>
        /// Set attributes of method
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public MethodBuilder WithAttributes(SyntaxList<AttributeListSyntax> attributes)
        {
            this.attributes = new SyntaxList<AttributeListSyntax>();
            this.attributes = this.attributes.AddRange(attributes);
            return this;
        }

        /// <summary>
        /// Set body of the method
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public MethodBuilder WithBody(BlockSyntax body)
        {
            this.body = body;
            return this;
        }

        /// <summary>
        /// Set xml comments of method
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public MethodBuilder WithXmlComments(string comment)
        {
            this.comment = comment;
            return this;
        }

        /// <summary>
        /// Set the method to starts 
        /// </summary>
        public MethodBuilder WithModifiers(SyntaxTokenList modifiers)
        {
            this.modifiers = modifiers;
            return this;
        }

        /// <summary>
        /// Build the final method declaration syntax 
        /// </summary>
        /// <returns></returns>
        public MethodDeclarationSyntax Build()
        {
            var method = BuildMethodBase();
            method = BuildModifiers(method);
            method = BuildAttributes(method);
            method = BuildXmlComments(method);
            method = BuildParameters(method);
            method = BuildBody(method);
            return method;
        }

        private MethodDeclarationSyntax BuildMethodBase()
        {
            if (returnType != null)
            {
                return SyntaxFactory.MethodDeclaration(SyntaxFactory.IdentifierName(returnType.Name),
                        SyntaxFactory.Identifier(name));
            }
            else
            {
                return SyntaxFactory.MethodDeclaration(
                        SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                        SyntaxFactory.Identifier(name));
            }
        }

        private MethodDeclarationSyntax BuildModifiers(MethodDeclarationSyntax method)
        {
            if (modifiers != null || !modifiers.Any())
            {
                return method;
            }

            return method.WithModifiers(modifiers);
        }


        private MethodDeclarationSyntax BuildXmlComments(MethodDeclarationSyntax method)
        {
            if (string.IsNullOrEmpty(comment))
                return method;
            var summary = new List<SyntaxToken>();
            summary.Add(SyntaxFactory.XmlTextNewLine(SyntaxFactory.TriviaList(), "\n", "\n", SyntaxFactory.TriviaList()));
            var commentLines = comment.Split(new[] { "\n" }, StringSplitOptions.None);
            for (int n = 0; n < commentLines.Length; n++)
            {
                var fixedCommentLine = $" {commentLines[n]}";
                if (n != commentLines.Length - 1)
                    fixedCommentLine += "\n";
                summary.Add(SyntaxFactory.XmlTextLiteral(SyntaxFactory.TriviaList(SyntaxFactory.DocumentationCommentExterior("///")), fixedCommentLine, fixedCommentLine, SyntaxFactory.TriviaList()));
            }
            summary.Add(SyntaxFactory.XmlTextNewLine(SyntaxFactory.TriviaList(), "\n", "\n", SyntaxFactory.TriviaList()));
            summary.Add(SyntaxFactory.XmlTextLiteral(SyntaxFactory.TriviaList(SyntaxFactory.DocumentationCommentExterior("///")), " ", " ", SyntaxFactory.TriviaList()));
            var trivia = SyntaxFactory.Trivia(
                SyntaxFactory.DocumentationCommentTrivia(
                    SyntaxKind.SingleLineDocumentationCommentTrivia, 
                    SyntaxFactory.List<XmlNodeSyntax>(new XmlNodeSyntax[]
                    {
                            SyntaxFactory.XmlText().WithTextTokens(SyntaxFactory.TokenList(SyntaxFactory.XmlTextLiteral(SyntaxFactory.TriviaList(SyntaxFactory.DocumentationCommentExterior("///")), " ", " ", SyntaxFactory.TriviaList()))),
                            SyntaxFactory.XmlElement(SyntaxFactory.XmlElementStartTag(SyntaxFactory.XmlName(SyntaxFactory.Identifier("summary"))), SyntaxFactory.XmlElementEndTag(SyntaxFactory.XmlName(SyntaxFactory.Identifier("summary"))))
                                .WithContent(SyntaxFactory.SingletonList<XmlNodeSyntax>(SyntaxFactory.XmlText().WithTextTokens(SyntaxFactory.TokenList(summary)))),
                            SyntaxFactory.XmlText().WithTextTokens(SyntaxFactory.TokenList(SyntaxFactory.XmlTextNewLine(SyntaxFactory.TriviaList(), "\n", "\n", SyntaxFactory.TriviaList())))
                    })));
            return method.WithLeadingTrivia(trivia);
        }

        private MethodDeclarationSyntax BuildAttributes(MethodDeclarationSyntax method)
        {
            return !attributes.Any() ? method : method.WithAttributeLists(SyntaxFactory.List(attributes));
        }

        private MethodDeclarationSyntax BuildParameters(MethodDeclarationSyntax method)
        {
            return parameters == null ? method : method.WithParameterList(parameters);
        }

        private MethodDeclarationSyntax BuildBody(MethodDeclarationSyntax method)
        {
            return body == null ? method : method.WithBody(body);
        }
    }
}
