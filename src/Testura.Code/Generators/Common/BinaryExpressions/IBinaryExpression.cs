using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Generators.Common.BinaryExpressions
{
    public interface IBinaryExpression
    {
        ExpressionSyntax GetBinaryExpression();
    }
}
