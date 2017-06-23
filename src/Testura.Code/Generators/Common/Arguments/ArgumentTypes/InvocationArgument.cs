using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Models.References;
using Testura.Code.Statements;
#pragma warning disable 1591

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes
{
    /// <summary>
    /// Provides the functionality to generate invocation arguments. Example of generated code: <c>(Do())</c>
    /// </summary>
    public class InvocationArgument : Argument
    {
        private readonly ExpressionSyntax _invocation;
        private readonly Type _castTo;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvocationArgument"/> class.
        /// </summary>
        /// <param name="invocation">The invoction express.</param>
        /// <param name="castTo">Cast to this type (no casting if null).</param>
        /// <param name="namedArgument">Specificy the argument for a partical parameter.</param>
        public InvocationArgument(ExpressionSyntax invocation, Type castTo = null, string namedArgument = null)
            : base(namedArgument)
        {
            if (invocation == null)
            {
                throw new ArgumentNullException(nameof(invocation));
            }

            _invocation = invocation;
            _castTo = castTo ?? typeof(void);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvocationArgument"/> class.
        /// </summary>
        /// <param name="reference">A reference to invoke</param>
        /// <param name="castTo">Cast to this type (no cast if null).</param>
        /// <param name="namedArgument">Specificy the argument for a partical parameter.</param>
        public InvocationArgument(VariableReference reference, Type castTo = null, string namedArgument = null)
            : base(namedArgument)
        {
            if (reference == null)
            {
                throw new ArgumentNullException(nameof(reference));
            }

            _invocation = Statement.Expression.Invoke(reference).AsExpression();
            _castTo = castTo ?? typeof(void);
        }

        protected override ArgumentSyntax CreateArgumentSyntax()
        {
            if (_castTo != typeof(void))
            {
                return SyntaxFactory.Argument(SyntaxFactory.CastExpression(TypeGenerator.Create(_castTo), _invocation));
            }

            return SyntaxFactory.Argument(_invocation);
        }
    }
}