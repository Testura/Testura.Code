using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Models
{
    public class Invocation
    {
        private readonly InvocationExpressionSyntax invocation;

        public Invocation(InvocationExpressionSyntax invocation)
        {
            this.invocation = invocation;
        }

        public ExpressionStatementSyntax AsExpressionStatement()
        {
            return ExpressionStatement(invocation);
        }

        public InvocationExpressionSyntax AsInvocationStatment()
        {
            return invocation;
        }
    }
}
