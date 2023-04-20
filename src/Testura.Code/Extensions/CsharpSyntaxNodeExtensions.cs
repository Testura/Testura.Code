using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Models;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Generators.Special;

/// <summary>
/// Extension methods to CsharpSyntaxNode.
/// </summary>
public static class CsharpSyntaxNodeExtensions
{
    /// <summary>
    /// Create summary to syntax node.
    /// </summary>
    /// <typeparam name="T">Syntax type.</typeparam>
    /// <param name="syntax">The syntax.</param>
    /// <param name="summary">Summary text.</param>
    /// <param name="parameters">Parameters in the summary</param>
    /// <returns>Return syntax node with summary.</returns>
    public static T WithSummary<T>(this T syntax, string summary, IEnumerable<Parameter>? parameters = null)
        where T : CSharpSyntaxNode
    {
        parameters = parameters ?? new List<Parameter>();

        if (string.IsNullOrEmpty(summary))
        {
            return syntax;
        }

        var content = List<XmlNodeSyntax>();

        content = CreateSummaryDocumentation(content, summary);

        foreach (var parameter in parameters.Where(p => p.XmlDocumentation != null))
        {
            content = CreateParameterDocumentation(content, parameter);
        }

        content = content.Add(XmlText().WithTextTokens(TokenList(XmlTextNewLine(TriviaList(), Environment.NewLine, Environment.NewLine, TriviaList()))));

        var trivia = Trivia(
            DocumentationCommentTrivia(
                SyntaxKind.SingleLineDocumentationCommentTrivia,
                content));
        return syntax.WithLeadingTrivia(trivia);
    }

    private static SyntaxList<XmlNodeSyntax> CreateSummaryDocumentation(SyntaxList<XmlNodeSyntax> content, string text)
    {
        var summary = new List<SyntaxToken>();
        summary.Add(XmlTextNewLine(TriviaList(), Environment.NewLine, Environment.NewLine, TriviaList()));
        var commentLines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        for (int n = 0; n < commentLines.Length; n++)
        {
            var fixedCommentLine = $" {commentLines[n]}";
            if (n != commentLines.Length - 1)
            {
                fixedCommentLine += Environment.NewLine;
            }

            summary.Add(XmlTextLiteral(TriviaList(DocumentationCommentExterior("///")), fixedCommentLine, fixedCommentLine, TriviaList()));
        }

        summary.Add(XmlTextNewLine(TriviaList(), Environment.NewLine, Environment.NewLine, TriviaList()));
        summary.Add(XmlTextLiteral(TriviaList(DocumentationCommentExterior("///")), " ", " ", TriviaList()));

        return content.AddRange(new List<XmlNodeSyntax>
        {
            XmlText().WithTextTokens(TokenList(XmlTextLiteral(TriviaList(DocumentationCommentExterior("///")), " ", " ", TriviaList()))),
            XmlElement(XmlElementStartTag(XmlName(Identifier("summary"))), XmlElementEndTag(XmlName(Identifier("summary"))))
                .WithContent(SingletonList<XmlNodeSyntax>(XmlText().WithTextTokens(TokenList(summary)))),
        });
    }

    private static SyntaxList<XmlNodeSyntax> CreateParameterDocumentation(SyntaxList<XmlNodeSyntax> content, Parameter parameter)
    {
        return content.AddRange(new List<XmlNodeSyntax>
        {
            XmlText().WithTextTokens(
                TokenList(
                    new[]
                    {
                        XmlTextNewLine(
                            TriviaList(),
                            Environment.NewLine,
                            Environment.NewLine,
                            TriviaList()),
                        XmlTextLiteral(
                            TriviaList(
                                DocumentationCommentExterior("///")),
                            " ",
                            " ",
                            TriviaList())
                    })),
            XmlExampleElement(SingletonList<XmlNodeSyntax>(
                    XmlText().WithTextTokens(
                        TokenList(
                            XmlTextLiteral(
                                TriviaList(),
                                parameter.XmlDocumentation,
                                parameter.XmlDocumentation,
                                TriviaList())))))
                .WithStartTag(XmlElementStartTag(
                        XmlName(
                            Identifier("param")))
                    .WithAttributes(
                        SingletonList<XmlAttributeSyntax>(
                            XmlNameAttribute(
                                XmlName(
                                    Identifier(" name")),
                                Token(SyntaxKind.DoubleQuoteToken),
                                IdentifierName(parameter.Name),
                                Token(SyntaxKind.DoubleQuoteToken)))))
                .WithEndTag(
                    XmlElementEndTag(
                        XmlName(
                            Identifier("param"))))
        });
    }
}
