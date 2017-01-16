using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes
{
    public class VariableArgument : IArgument
    {
        public VariableArgument(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public ArgumentSyntax GetArgumentSyntax()
        {
            return SyntaxFactory.Argument(SyntaxFactory.IdentifierName(Name));
        }
    }
}
