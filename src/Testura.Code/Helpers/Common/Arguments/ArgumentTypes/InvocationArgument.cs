using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Helpers.Common.Arguments.ArgumentTypes
{
    public class InvocationArgument : IArgument
    {
        private readonly ExpressionSyntax _invocation;
        private readonly Type _castTo;

        public InvocationArgument(ExpressionSyntax invocation, Type castTo = null)
        {
            _invocation = invocation;
            _castTo = castTo ?? typeof(void);
        }

        public ArgumentSyntax GetArgumentSyntax()
        {
            if (_castTo != typeof(void))
            {
                return SyntaxFactory.Argument(SyntaxFactory.CastExpression(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.BoolKeyword)), _invocation));
            }
            return SyntaxFactory.Argument(_invocation);
        }
    }
}