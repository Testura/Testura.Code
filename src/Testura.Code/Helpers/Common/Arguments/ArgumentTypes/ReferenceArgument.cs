using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Helpers.Common.References;

namespace Testura.Code.Helpers.Common.Arguments.ArgumentTypes
{
    public class ReferenceArgument : IArgument
    {
        private readonly VariableReference _reference;

        public ReferenceArgument(VariableReference reference)
        {
            _reference = reference;
        }

        public ArgumentSyntax GetArgumentSyntax()
        {
            return SyntaxFactory.Argument(Reference.Create(_reference));
        }
    }
}