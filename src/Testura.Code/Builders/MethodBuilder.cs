using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Helpers.Common;
using Testura.Code.Models;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Attribute = Testura.Code.Models.Attribute;

namespace Testura.Code.Builders
{
    /// <summary>
    /// Help class to build a method
    /// </summary>
    public class MethodBuilder
    {
        private readonly string _name;
        private readonly List<Attribute> _attributes;
        private readonly List<Parameter> _parameters;
        private readonly List<Modifiers> _modifiers;
        private Type _returnType;
        private BlockSyntax _body;
        private string _comment;

        public MethodBuilder(string name)
        {
            _name = name.Replace(" ", "_");
            _parameters = new List<Parameter>();
            _attributes = new List<Attribute>();
            _modifiers = new List<Modifiers>();
        }

        /// <summary>
        /// Set parameters for method
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public MethodBuilder WithParameters(params Parameter[] parameters)
        {
            _parameters.Clear();
            _parameters.AddRange(parameters);
            return this;
        }

        /// <summary>
        /// Set return type of method 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public MethodBuilder WithReturnType(Type type)
        {
            _returnType = type;
            return this;
        }

        /// <summary>
        /// Set attributes of method
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public MethodBuilder WithAttributes(params Attribute[] attributes)
        {
            _attributes.Clear();
            _attributes.AddRange(attributes);
            return this;
        }

        /// <summary>
        /// Set body of the method
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public MethodBuilder WithBody(BlockSyntax body)
        {
            _body = body;
            return this;
        }

        /// <summary>
        /// Set xml comments of method
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public MethodBuilder WithXmlComments(string comment)
        {
            _comment = comment;
            return this;
        }

        /// <summary>
        /// Set the method to starts 
        /// </summary>
        public MethodBuilder WithModifiers(params Modifiers[] modifiers)
        {
            _modifiers.Clear();
            _modifiers.AddRange(modifiers);
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
            if (_returnType != null)
            {
                return MethodDeclaration(IdentifierName(_returnType.Name),
                        Identifier(_name));
            }
            else
            {
                return MethodDeclaration(
                        PredefinedType(Token(SyntaxKind.VoidKeyword)),
                        Identifier(_name));
            }
        }

        private MethodDeclarationSyntax BuildModifiers(MethodDeclarationSyntax method)
        {
            if (_modifiers != null || !_modifiers.Any())
            {
                return method;
            }

            return method.WithModifiers(Helpers.Common.Modifiers.Create(_modifiers.ToArray()));
        }

        private MethodDeclarationSyntax BuildXmlComments(MethodDeclarationSyntax method)
        {
            if (string.IsNullOrEmpty(_comment))
                return method;
            var summary = new List<SyntaxToken>();
            summary.Add(XmlTextNewLine(TriviaList(), "\n", "\n", TriviaList()));
            var commentLines = _comment.Split(new[] { "\n" }, StringSplitOptions.None);
            for (int n = 0; n < commentLines.Length; n++)
            {
                var fixedCommentLine = $" {commentLines[n]}";
                if (n != commentLines.Length - 1)
                    fixedCommentLine += "\n";
                summary.Add(XmlTextLiteral(TriviaList(DocumentationCommentExterior("///")), fixedCommentLine, fixedCommentLine, TriviaList()));
            }

            summary.Add(XmlTextNewLine(TriviaList(), "\n", "\n", TriviaList()));
            summary.Add(XmlTextLiteral(TriviaList(DocumentationCommentExterior("///")), " ", " ", TriviaList()));
            var trivia = Trivia(
                DocumentationCommentTrivia(
                    SyntaxKind.SingleLineDocumentationCommentTrivia,
                    List<XmlNodeSyntax>(new XmlNodeSyntax[]
                    {
                            XmlText().WithTextTokens(TokenList(XmlTextLiteral(TriviaList(DocumentationCommentExterior("///")), " ", " ", TriviaList()))),
                            XmlElement(XmlElementStartTag(XmlName(Identifier("summary"))), XmlElementEndTag(XmlName(Identifier("summary"))))
                                .WithContent(SingletonList<XmlNodeSyntax>(XmlText().WithTextTokens(TokenList(summary)))),
                            XmlText().WithTextTokens(TokenList(XmlTextNewLine(TriviaList(), "\n", "\n", TriviaList())))
                    })));
            return method.WithLeadingTrivia(trivia);
        }

        private MethodDeclarationSyntax BuildAttributes(MethodDeclarationSyntax method)
        {
            return !_attributes.Any() ? method : method.WithAttributeLists(List(Attributes.Create(_attributes.ToArray())));
        }

        private MethodDeclarationSyntax BuildParameters(MethodDeclarationSyntax method)
        {
            return _parameters == null ? method : method.WithParameterList(Parameters.Create(_parameters.ToArray()));
        }

        private MethodDeclarationSyntax BuildBody(MethodDeclarationSyntax method)
        {
            return _body == null ? method : method.WithBody(_body);
        }
    }
}
