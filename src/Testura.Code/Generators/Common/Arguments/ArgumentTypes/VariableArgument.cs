using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes
{
    public class VariableArgument : Argument
    {
        public VariableArgument(string name, string namedArgument = null)
            : base(namedArgument)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }

        public string Name { get; set; }

        protected override ArgumentSyntax CreateArgumentSyntax()
        {
            return SyntaxFactory.Argument(SyntaxFactory.IdentifierName(Name));
        }
    }
}
