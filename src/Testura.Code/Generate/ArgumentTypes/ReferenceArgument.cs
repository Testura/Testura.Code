using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Reference;

namespace Testura.Code.Generate.ArgumentTypes
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
            return SyntaxFactory.Argument(References.GenerateReferenceChain(reference));
        }
    }
}