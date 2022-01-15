using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Factories;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;
using Testura.Code.Generators.Common.BinaryExpressions;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Statements;

/// <summary>
/// Provides the functionality to generate selection statements.
/// </summary>
public class SelectionStatement
{
    /// <summary>
    /// Create the statement syntax for a if-conditional.
    /// </summary>
    /// <param name="leftArgument">The left argument of the if-statement.</param>
    /// <param name="rightArgument">The right argument of the if-statement.</param>
    /// <param name="conditional">The conditional.</param>
    /// <param name="block">The block containing all statements.</param>
    /// <returns>The declared statement syntax.</returns>
    public StatementSyntax If(IArgument leftArgument, IArgument rightArgument, ConditionalStatements conditional, BlockSyntax block)
    {
        if (leftArgument == null)
        {
            throw new ArgumentNullException(nameof(leftArgument));
        }

        if (rightArgument == null)
        {
            throw new ArgumentNullException(nameof(rightArgument));
        }

        return
            IfStatement(
                BinaryExpression(
                    ConditionalFactory.GetSyntaxKind(conditional),
                    leftArgument.GetArgumentSyntax().Expression,
                    rightArgument.GetArgumentSyntax().Expression),
                block);
    }

    /// <summary>
    /// Create the statement syntax for a if-conditional with a single statement.
    /// </summary>
    /// <param name="leftArgument">The left argument of the if-statement.</param>
    /// <param name="rightArgument">The right argument of the if-statement.</param>
    /// <param name="conditional">The conditional.</param>
    /// <param name="expressionStatement">Statement inside the if.</param>
    /// <returns>The declared statement syntax.</returns>
    public StatementSyntax If(IArgument leftArgument, IArgument rightArgument, ConditionalStatements conditional, ExpressionStatementSyntax expressionStatement)
    {
        if (leftArgument == null)
        {
            throw new ArgumentNullException(nameof(leftArgument));
        }

        if (rightArgument == null)
        {
            throw new ArgumentNullException(nameof(rightArgument));
        }

        return
            IfStatement(
                BinaryExpression(
                    ConditionalFactory.GetSyntaxKind(conditional),
                    leftArgument.GetArgumentSyntax().Expression,
                    rightArgument.GetArgumentSyntax().Expression),
                expressionStatement);
    }

    /// <summary>
    /// Create the statement syntax for a if-conditional.
    /// </summary>
    /// <param name="binaryExpression">The binary expression to generate.</param>
    /// <param name="block">The block containing all statements.</param>
    /// <returns>The declared statement syntax.</returns>
    public StatementSyntax If(IBinaryExpression binaryExpression, BlockSyntax block)
    {
        if (binaryExpression == null)
        {
            throw new ArgumentNullException(nameof(binaryExpression));
        }

        return IfStatement(binaryExpression.GetBinaryExpression(),  block);
    }

    /// <summary>
    /// Create the statement syntax for a if-conditional with a single statement.
    /// </summary>
    /// <param name="binaryExpression">The binary expression to generate.</param>
    /// <param name="expressionStatement">Statement inside the if.</param>
    /// <returns>The declared statement syntax.</returns>
    public StatementSyntax If(IBinaryExpression binaryExpression, ExpressionStatementSyntax expressionStatement)
    {
        if (binaryExpression == null)
        {
            throw new ArgumentNullException(nameof(binaryExpression));
        }

        return IfStatement(binaryExpression.GetBinaryExpression(), expressionStatement);
    }
}