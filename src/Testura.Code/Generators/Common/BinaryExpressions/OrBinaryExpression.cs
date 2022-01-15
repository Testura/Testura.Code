using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Generators.Common.BinaryExpressions;

/// <summary>
/// Provides functionality to generate a binary expression with a or (<c>||</c>).
/// </summary>
public class OrBinaryExpression : IBinaryExpression
{
    private readonly ExpressionSyntax _leftExpression;
    private readonly ExpressionSyntax _rightExpression;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrBinaryExpression"/> class.
    /// </summary>
    /// <param name="leftExpression">The left expression.</param>
    /// <param name="rightExpression">The right expression.</param>
    public OrBinaryExpression(ExpressionSyntax leftExpression, ExpressionSyntax rightExpression)
    {
        _leftExpression = leftExpression ?? throw new ArgumentNullException(nameof(leftExpression));
        _rightExpression = rightExpression ?? throw new ArgumentNullException(nameof(rightExpression));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OrBinaryExpression"/> class.
    /// </summary>
    /// <param name="leftExpression">The left binary expression.</param>
    /// <param name="rightExpression">The right binary expression.</param>
    public OrBinaryExpression(IBinaryExpression leftExpression, IBinaryExpression rightExpression)
        : this(leftExpression?.GetBinaryExpression(), rightExpression?.GetBinaryExpression())
    {
    }

    /// <summary>
    /// Get the generated binary expression.
    /// </summary>
    /// <returns>The generated binary expression.</returns>
    public ExpressionSyntax GetBinaryExpression()
    {
        return BinaryExpression(SyntaxKind.LogicalOrExpression, _leftExpression, _rightExpression);
    }
}
