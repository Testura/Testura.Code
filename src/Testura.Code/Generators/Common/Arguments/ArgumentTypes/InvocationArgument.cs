using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Models.References;
using Testura.Code.Statements;

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes
{
    public class InvocationArgument : IArgument
    {
        private readonly ExpressionSyntax _invocation;
        private readonly Type _castTo;

        public InvocationArgument(ExpressionSyntax invocation, Type castTo = null)
        {
            if (invocation == null)
            {
                throw new ArgumentNullException(nameof(invocation));
            }

            _invocation = invocation;
            _castTo = castTo ?? typeof(void);
        }

        public InvocationArgument(VariableReference reference, Type castTo = null)
        {
            if (reference == null)
            {
                throw new ArgumentNullException(nameof(reference));
            }

            _invocation = Statement.Expression.Invoke(reference).AsExpression();
            _castTo = castTo ?? typeof(void);
        }

        public ArgumentSyntax GetArgumentSyntax()
        {
            if (_castTo != typeof(void))
            {
                return SyntaxFactory.Argument(SyntaxFactory.CastExpression(TypeGenerator.Create(_castTo), _invocation));
            }

            return SyntaxFactory.Argument(_invocation);
        }
    }
}