using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common;
using Testura.Code.Models;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Attribute = Testura.Code.Models.Attribute;

namespace Testura.Code.Builders
{
    /// <summary>
    /// Provides a builder to generate a method
    /// </summary>
    public class MethodBuilder
    {
        private readonly string _name;
        private readonly List<ParameterSyntax> _parameters;
        private readonly List<Modifiers> _modifiers;
        private Type _returnType;
        private BlockSyntax _body;
        private string _summary;

        private SyntaxList<AttributeListSyntax> _attributes;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodBuilder"/> class.
        /// </summary>
        /// <param name="name">Name of the method</param>
        public MethodBuilder(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            _name = name.Replace(" ", "_");
            _parameters = new List<ParameterSyntax>();
            _modifiers = new List<Modifiers>();
            _body = BodyGenerator.Create();
        }

        /// <summary>
        /// Set method parameters.
        /// </summary>
        /// <param name="parameters">A set of wanted parameters.</param>
        /// <returns>The current method builder</returns>
        public MethodBuilder WithParameters(params Parameter[] parameters)
        {
            _parameters.Clear();
            foreach (var parameter in parameters)
            {
                _parameters.Add(ParameterGenerator.Create(parameter));
            }

            return this;
        }

        /// <summary>
        /// Set method parameters.
        /// </summary>
        /// <param name="parameters">A set of already generated parameters.</param>
        /// <returns>The current method builder</returns>
        public MethodBuilder WithParameters(params ParameterSyntax[] parameters)
        {
            _parameters.Clear();
            _parameters.AddRange(parameters);
            return this;
        }

        /// <summary>
        /// Set method return type
        /// </summary>
        /// <param name="type">The wanted return type</param>
        /// <returns>The current method builder</returns>
        public MethodBuilder WithReturnType(Type type)
        {
            _returnType = type;
            return this;
        }

        /// <summary>
        /// Set method attributs.
        /// </summary>
        /// <param name="attributes">A set of wanted attributes</param>
        /// <returns>The current method builder</returns>
        public MethodBuilder WithAttributes(params Attribute[] attributes)
        {
            _attributes = AttributeGenerator.Create(attributes);
            return this;
        }

        /// <summary>
        /// Set method attributes.
        /// </summary>
        /// <param name="attributes">A set of already generated attributes </param>
        /// <returns>The current method builder</returns>
        public MethodBuilder WithAttributes(SyntaxList<AttributeListSyntax> attributes)
        {
            _attributes = attributes;
            return this;
        }

        /// <summary>
        /// Set method body.
        /// </summary>
        /// <param name="body">The method body.</param>
        /// <returns>The current method builder</returns>
        public MethodBuilder WithBody(BlockSyntax body)
        {
            _body = body;
            return this;
        }

        /// <summary>
        /// Set method xml summary.
        /// </summary>
        /// <param name="summary">The method summary.</param>
        /// <returns>The current method builder</returns>
        public MethodBuilder WithSummary(string summary)
        {
            _summary = summary;
            return this;
        }

        /// <summary>
        /// Set method modifiers.
        /// </summary>
        /// <param name="modifiers">A set of wanted modifiers.</param>
        /// <returns>The current method builder</returns>
        public MethodBuilder WithModifiers(params Modifiers[] modifiers)
        {
            _modifiers.Clear();
            _modifiers.AddRange(modifiers);
            return this;
        }

        /// <summary>
        /// Build method and return the generated code.
        /// </summary>
        /// <returns>The generated method.</returns>
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
                return MethodDeclaration(TypeGenerator.Create(_returnType), Identifier(_name));
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
            if (_modifiers == null || !_modifiers.Any())
            {
                return method;
            }

            return method.WithModifiers(ModifierGenerator.Create(_modifiers.ToArray()));
        }

        private MethodDeclarationSyntax BuildXmlComments(MethodDeclarationSyntax method)
        {
            if (string.IsNullOrEmpty(_summary))
            {
                return method;
            }

            var summary = new List<SyntaxToken>();
            summary.Add(XmlTextNewLine(TriviaList(), "\n", "\n", TriviaList()));
            var commentLines = _summary.Split(new[] { "\n" }, StringSplitOptions.None);
            for (int n = 0; n < commentLines.Length; n++)
            {
                var fixedCommentLine = $" {commentLines[n]}";
                if (n != commentLines.Length - 1)
                {
                    fixedCommentLine += "\n";
                }

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
            return !_attributes.Any() ? method : method.WithAttributeLists(_attributes);
        }

        private MethodDeclarationSyntax BuildParameters(MethodDeclarationSyntax method)
        {
            return !_parameters.Any() ? method : method.WithParameterList(ParameterGenerator.ConvertParameterSyntaxToList(_parameters.ToArray()));
        }

        private MethodDeclarationSyntax BuildBody(MethodDeclarationSyntax method)
        {
            if (_body == null)
            {
                return method.WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
            }

            return method.WithBody(_body);
        }
    }
}
