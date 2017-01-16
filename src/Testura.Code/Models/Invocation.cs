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

        public ExpressionStatementSyntax AsStatement()
        {
            return ExpressionStatement(_invocation);
        }

        public InvocationExpressionSyntax AsExpression()
        {
            return _invocation;
        }
    }
}
