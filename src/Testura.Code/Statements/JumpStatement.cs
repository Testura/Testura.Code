using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Statements
{
    public class JumpStatement
    {
        /// <summary>
        /// Create a true return statement
        /// </summary>
        /// <returns></returns>
        public ReturnStatementSyntax True()
        {
            return ReturnStatement(
                LiteralExpression(SyntaxKind.TrueLiteralExpression).WithToken(Token(SyntaxKind.TrueKeyword)));
        }

        /// <summary>
        /// Create a false return statement
        /// </summary>
        /// <returns></returns>
        public ReturnStatementSyntax False()
        {
            return ReturnStatement(
                LiteralExpression(SyntaxKind.TrueLiteralExpression).WithToken(Token(SyntaxKind.FalseKeyword)));
        }
    }
}
