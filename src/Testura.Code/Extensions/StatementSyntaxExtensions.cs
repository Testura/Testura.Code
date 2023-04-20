using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Extensions;

public static class StatementSyntaxExtensions
{
    /// <summary>
    /// Add comment to a statement.
    /// </summary>
    /// <param name="statementSyntax">Statement</param>
    /// <param name="comment">Comment text</param>
    /// <param name="commentPosition">The position of the comment.</param>
    /// <returns>Statement with comment.</returns>
    public static StatementSyntax WithComment(this StatementSyntax statementSyntax, string comment, CommentPosition commentPosition = CommentPosition.Above)
    {
        switch (commentPosition)
        {
            case CommentPosition.Above:
                return statementSyntax.WithLeadingTrivia(SyntaxFactory.TriviaList(SyntaxFactory.Comment($"//{comment}{Environment.NewLine}")));
            case CommentPosition.Right:
                return statementSyntax.WithTrailingTrivia(SyntaxFactory.TriviaList(SyntaxFactory.Comment($" //{comment}{Environment.NewLine}")));
            default:
                throw new ArgumentOutOfRangeException(nameof(commentPosition), commentPosition, null);
        }
    }
}
