using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common;
using Testura.Code.Models.References;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Statements;

/// <summary>
/// Used to generate jump statements (for example return).
/// </summary>
public class JumpStatement
{
    /// <summary>
    /// Create the return statement syntax to return true.
    /// </summary>
    /// <returns>The declared return statement syntax.</returns>
    public ReturnStatementSyntax ReturnTrue()
    {
        return ReturnStatement(
            LiteralExpression(SyntaxKind.TrueLiteralExpression).WithToken(Token(SyntaxKind.TrueKeyword)));
    }

    /// <summary>
    /// Create the return statement syntax to return false.
    /// </summary>
    /// <returns>The declared return statement syntax.</returns>
    public ReturnStatementSyntax ReturnFalse()
    {
        return ReturnStatement(
            LiteralExpression(SyntaxKind.TrueLiteralExpression).WithToken(Token(SyntaxKind.FalseKeyword)));
    }

    /// <summary>
    /// Create the return statement syntax to return of a reference.
    /// </summary>
    /// <param name="variableReference">Reference that we should return.</param>
    /// <returns>The declared return statement syntax.</returns>
    public ReturnStatementSyntax Return(VariableReference variableReference)
    {
        if (variableReference == null)
        {
            throw new ArgumentNullException(nameof(variableReference));
        }

        return ReturnStatement(ReferenceGenerator.Create(variableReference));
    }

    /// <summary>
    /// Create the return statement syntax to return another expression.
    /// </summary>
    /// <param name="expression">The expression syntax to return.</param>
    /// <returns>The declared return statement syntax.</returns>
    public ReturnStatementSyntax Return(ExpressionSyntax expression)
    {
        if (expression == null)
        {
            throw new ArgumentNullException(nameof(expression));
        }

        return ReturnStatement(expression);
    }

    /// <summary>
    /// Create the return statement syntax to return this.
    /// </summary>
    /// <returns>The declared return statement syntax.</returns>
    public ReturnStatementSyntax ReturnThis()
    {
        return ReturnStatement(ThisExpression());
    }
}