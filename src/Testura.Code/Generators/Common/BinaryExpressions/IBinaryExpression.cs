using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Generators.Common.Binaries
{
    public interface IBinaryExpression
    {
        ExpressionSyntax GetBinaryExpression();
    }
}
