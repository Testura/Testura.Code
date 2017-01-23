using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Models
{
    public class Invocation
    {
        private readonly InvocationExpressionSyntax _invocation;

        public Invocation(InvocationExpressionSyntax invocation)
        {
            _invocation = invocation;
        }

        /// <summary>
        /// Convert the invocation expression to a statement
        /// </summary>
        /// <returns>A statement</returns>
        public ExpressionStatementSyntax AsStatement()
        {
            return ExpressionStatement(_invocation);
        }

        /// <summary>
        /// Convert the invocation expression to an expression
        /// </summary>
        /// <returns>An expression</returns>
        public InvocationExpressionSyntax AsExpression()
        {
            return _invocation;
        }
    }
}
