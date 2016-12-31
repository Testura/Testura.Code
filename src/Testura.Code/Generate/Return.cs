using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Generate
{
    /// <summary>
    /// Generate return statements
    /// </summary>
    public static class Return
    {
        /// <summary>
        /// Create a true return statement
        /// </summary>
        /// <returns></returns>
        public static ReturnStatementSyntax True()
        {
            return SyntaxFactory.ReturnStatement(
                SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression).WithToken(SyntaxFactory.Token(SyntaxKind.TrueKeyword)));
        }

        /// <summary>
        /// Create a false return statement
        /// </summary>
        /// <returns></returns>
        public static ReturnStatementSyntax False()
        {
            return SyntaxFactory.ReturnStatement(
                SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression).WithToken(SyntaxFactory.Token(SyntaxKind.FalseKeyword)));
        }
    }
}
