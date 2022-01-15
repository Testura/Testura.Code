using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Factories;
using Testura.Code.Models.References;

namespace Testura.Code.Generators.Common.BinaryExpressions;

/// <summary>
/// Provides the functionality to generate a conditional binary expression. Example of generated code: <c>test()+test.MyProp</c>
/// </summary>
public class ConditionalBinaryExpression : IBinaryExpression
{
    private readonly ExpressionSyntax _leftExpression;
    private readonly ExpressionSyntax _rightExpression;
    private readonly ConditionalStatements _conditional;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConditionalBinaryExpression"/> class.
    /// </summary>
    /// <param name="leftExpression">The left expression.</param>
    /// <param name="rightExpression">The right expression.</param>
    /// <param name="conditional">The conditional statement between two expressions.</param>
    public ConditionalBinaryExpression(
        ExpressionSyntax leftExpression,
        ExpressionSyntax rightExpression,
        ConditionalStatements conditional)
    {
        _leftExpression = leftExpression ?? throw new ArgumentNullException(nameof(leftExpression));
        _rightExpression = rightExpression ?? throw new ArgumentNullException(nameof(rightExpression));
        _conditional = conditional;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConditionalBinaryExpression"/> class.
    /// </summary>
    /// <param name="leftReference">The left reference.</param>
    /// <param name="rightReference">The right reference.</param>
    /// <param name="conditional">The conditional statement between two expressions.</param>
    public ConditionalBinaryExpression(
        VariableReference leftReference,
        VariableReference rightReference,
        ConditionalStatements conditional)
    {
        if (leftReference == null)
        {
            throw new ArgumentNullException(nameof(leftReference));
        }

        if (rightReference == null)
        {
            throw new ArgumentNullException(nameof(rightReference));
        }

        _leftExpression = ReferenceGenerator.Create(leftReference);
        _rightExpression = ReferenceGenerator.Create(rightReference);
        _conditional = conditional;
    }

    /// <summary>
    /// Get the generated binary expression.
    /// </summary>
    /// <returns>The generated binary expression.</returns>
    public ExpressionSyntax GetBinaryExpression()
    {
        return SyntaxFactory.BinaryExpression(ConditionalFactory.GetSyntaxKind(_conditional), _leftExpression, _rightExpression);
    }
}
