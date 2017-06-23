using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Models.References;
#pragma warning disable 1591

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes
{
    /// <summary>
    /// Provides the functionallity to generate reference arguments. Example of generated code: <c>(i.MyProperty)</c>
    /// </summary>
    public class ReferenceArgument : Argument
    {
        private readonly VariableReference _reference;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceArgument"/> class.
        /// </summary>
        /// <param name="reference">The variable/method reference.</param>
        /// <param name="namedArgument">Specificy the argument for a partical parameter.</param>
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