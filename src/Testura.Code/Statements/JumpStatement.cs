using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common;
using Testura.Code.Models.References;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Statements
{
    public class JumpStatement
    {
        /// <summary>
        /// Create a true return statement
        /// </summary>
        /// <returns></returns>
        public ReturnStatementSyntax ReturnTrue()
        {
            return ReturnStatement(
                LiteralExpression(SyntaxKind.TrueLiteralExpression).WithToken(Token(SyntaxKind.TrueKeyword)));
        }

        /// <summary>
        /// Create a false return statement
        /// </summary>
        /// <returns></returns>
        public ReturnStatementSyntax ReturnFalse()
        {
            return ReturnStatement(
                LiteralExpression(SyntaxKind.TrueLiteralExpression).WithToken(Token(SyntaxKind.FalseKeyword)));
        }

        /// <summary>
        /// Create a false return statement
        /// </summary>
        /// <returns></returns>
        public ReturnStatementSyntax Return(VariableReference variableReference)
        {
            return ReturnStatement(ReferenceGenerator.Create(variableReference));
        }

        /// <summary>
        /// Create a false return statement
        /// </summary>
        /// <returns></returns>
        public ReturnStatementSyntax Return(ExpressionSyntax expression)
        {
            return ReturnStatement(expression);
        }
    }
}
