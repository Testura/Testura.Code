using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Helpers.Arguments.ArgumentTypes
{
    public class InvocationArgument : IArgument
    {
        private readonly ExpressionSyntax invocation;
        private readonly Type castTo;

        public InvocationArgument(ExpressionSyntax invocation, Type castTo = null)
        {
            this.invocation = invocation;
            this.castTo = castTo ?? typeof(void);
        }

        public ArgumentSyntax GetArgumentSyntax()
        {
            if (castTo != typeof(void))
            {
                return SyntaxFactory.Argument(SyntaxFactory.CastExpression(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.BoolKeyword)), invocation));
            }
            return SyntaxFactory.Argument(invocation);
        }
    }
}