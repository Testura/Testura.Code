using Microsoft.CodeAnalysis.CSharp;

namespace Testura.Code.Factories;

internal static class OperatorFactory
{
    public static SyntaxKind GetSyntaxKind(Operators @operator)
    {
        switch (@operator)
        {
            case Operators.Multiplication:
                return SyntaxKind.AsteriskToken;
            case Operators.Division:
                return SyntaxKind.SlashToken;
            case Operators.Reminder:
                return SyntaxKind.PercentToken;
            case Operators.Addition:
                return SyntaxKind.PlusToken;
            case Operators.Subtraction:
                return SyntaxKind.MinusToken;
            case Operators.Equal:
                return SyntaxKind.EqualsEqualsToken;
            case Operators.NotEqual:
                return SyntaxKind.ExclamationEqualsToken;
            case Operators.GreaterThan:
                return SyntaxKind.GreaterThanToken;
            case Operators.GreaterThanOrEqual:
                return SyntaxKind.GreaterThanEqualsToken;
            case Operators.LessThan:
                return SyntaxKind.LessThanToken;
            case Operators.LessThanOrEqual:
                return SyntaxKind.LessThanEqualsToken;
            case Operators.Increment:
                return SyntaxKind.PlusPlusToken;
            case Operators.Decrement:
                return SyntaxKind.MinusMinusToken;
            default:
                throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null);
        }
    }
}