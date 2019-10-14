using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Builders
{
    /// <summary>
    /// Provides a builder to generate an interface
    /// </summary>
    public class InterfaceBuilder : TypeBuilderBase<InterfaceBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceBuilder"/> class.
        /// </summary>
        /// <param name="name">Name of the interface.</param>
        /// <param name="namespace">Name of the interface namespace.</param>
        public InterfaceBuilder(string name, string @namespace)
            : base(name, @namespace)
        {
        }

        protected override TypeDeclarationSyntax BuildBase()
        {
            return SyntaxFactory.InterfaceDeclaration(Name).WithBaseList(CreateBaseList()).WithModifiers(CreateModifiers());
        }
    }
}
