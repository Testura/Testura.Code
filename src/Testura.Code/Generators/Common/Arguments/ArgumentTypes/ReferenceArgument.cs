using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Models.References;

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes
{
    public class ReferenceArgument : Argument
    {
        private readonly VariableReference _reference;

        public ReferenceArgument(VariableReference reference, string namedArgument = null)
            : base(namedArgument)
        {
            if (reference == null)
            {
                throw new ArgumentNullException(nameof(reference));
            }

            _reference = reference;
        }

        protected override ArgumentSyntax CreateArgumentSyntax()
        {
            return SyntaxFactory.Argument(ReferenceGenerator.Create(_reference));
        }
    }
}