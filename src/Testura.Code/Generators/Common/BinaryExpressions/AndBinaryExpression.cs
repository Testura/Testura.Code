using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Generators.Common.BinaryExpressions;

/// <summary>
/// Provides the functionality to generate a binary expression with an and (<c>&amp;&amp;</c>).
/// </summary>
public class AndBinaryExpression : IBinaryExpression
{
    private readonly ExpressionSyntax _leftExpression;
    private readonly ExpressionSyntax _rightExpression;

    /// <summary>
    /// Initializes a new instance of the <see cref="AndBinaryExpression"/> class.
    /// </summary>
    /// <param name="leftExpression">The left expression.</param>
    /// <param name="rightExpression">The right expression.</param>
    public AndBinaryExpression(ExpressionSyntax leftExpression, ExpressionSyntax rightExpression)
    {
        _leftExpression = leftExpression ?? throw new ArgumentNullException(nameof(leftExpression));
        _rightExpression = rightExpression ?? throw new ArgumentNullException(nameof(rightExpression));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AndBinaryExpression"/> class.
    /// </summary>
    /// <param name="leftExpression">The left binary expression.</param>
    /// <param name="rightExpression">The right binary expression.</param>
    public AndBinaryExpression(IBinaryExpression leftExpression, IBinaryExpression rightExpression)
        : this(leftExpression?.GetBinaryExpression(), rightExpression?.GetBinaryExpression())
    {
    }

    /// <summary>
    /// Get the generated binary expression.
    /// </summary>
    /// <returns>The generated binary expression.</returns>
    public ExpressionSyntax GetBinaryExpression()
    {
        return BinaryExpression(SyntaxKind.LogicalAndExpression, _leftExpression, _rightExpression);
    }
}
