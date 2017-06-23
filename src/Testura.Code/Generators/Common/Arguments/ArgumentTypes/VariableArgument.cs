using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
#pragma warning disable 1591

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes
{
    /// <summary>
    /// Provides the functionality to generate variable argument. Example of generated code: <c>(myVariable)</c>
    /// </summary>
    public class VariableArgument : Argument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VariableArgument"/> class.
        /// </summary>
        /// <param name="name">Name of the variable</param>
        /// <param name="namedArgument">Specificy the argument for a partical parameter</param>
        public VariableArgument(string name, string namedArgument = null)
            : base(namedArgument)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }

        /// <summary>
        /// Gets or sets the name of variable argument
        /// </summary>
        public string Name { get; set; }

        protected override ArgumentSyntax CreateArgumentSyntax()
        {
            return SyntaxFactory.Argument(SyntaxFactory.IdentifierName(Name));
        }
    }
}
