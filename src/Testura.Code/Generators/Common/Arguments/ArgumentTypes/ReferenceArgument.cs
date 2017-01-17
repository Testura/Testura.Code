using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Models.References;

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes
{
    public class ReferenceArgument : IArgument
    {
        private readonly VariableReference _reference;

        public ReferenceArgument(VariableReference reference)
        {
            if (reference == null)
            {
                throw new ArgumentNullException(nameof(reference));
            }

            _reference = reference;
        }

        public ArgumentSyntax GetArgumentSyntax()
        {
            return SyntaxFactory.Argument(ReferenceGenerator.Create(_reference));
        }
    }
}