using Microsoft.CodeAnalysis.CSharp;

namespace Testura.Code.Factories;

internal static class ConditionalFactory
{
    public static SyntaxKind GetSyntaxKind(ConditionalStatements conditional)
    {
        switch (conditional)
        {
            case ConditionalStatements.Equal:
                return SyntaxKind.EqualsExpression;
            case ConditionalStatements.NotEqual:
                return SyntaxKind.NotEqualsExpression;
            case ConditionalStatements.GreaterThan:
                return SyntaxKind.GreaterThanExpression;
            case ConditionalStatements.GreaterThanOrEqual:
                return SyntaxKind.GreaterThanOrEqualExpression;
            case ConditionalStatements.LessThan:
                return SyntaxKind.LessThanExpression;
            case ConditionalStatements.LessThanOrEqual:
                return SyntaxKind.LessThanOrEqualExpression;
            default:
                throw new ArgumentOutOfRangeException(nameof(conditional), conditional, null);
        }
    }
}