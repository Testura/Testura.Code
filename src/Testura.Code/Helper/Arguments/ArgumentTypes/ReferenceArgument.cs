using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Helper.References;

namespace Testura.Code.Helper.Arguments.ArgumentTypes
{
    public class ReferenceArgument : IArgument
    {
        private readonly VariableReference reference;

        public ReferenceArgument(VariableReference reference)
        {
            this.reference = reference;
        }

        public ArgumentSyntax GetArgumentSyntax()
        {
            return SyntaxFactory.Argument(Reference.Create(reference));
        }
    }
}