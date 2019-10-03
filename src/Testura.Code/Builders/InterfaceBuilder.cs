using Microsoft.CodeAnalysis;
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

        /// <summary>
        /// Build the interface and return the generated code.
        /// </summary>
        /// <returns>The generated class.</returns>
        public CompilationUnitSyntax Build()
        {
            var members = default(SyntaxList<MemberDeclarationSyntax>);

            foreach (var member in Members)
            {
                members = member.AddMember(members);
            }

            var @interface = BuildInterfaceBase();
            @interface = BuildAttributes(@interface);
            @interface = @interface.WithMembers(members);
            var @base = SyntaxFactory.CompilationUnit();
            @base = BuildUsings(@base);
            @base = BuildNamespace(@base, @interface);
            return @base;
        }

        private TypeDeclarationSyntax BuildInterfaceBase()
        {
            return SyntaxFactory.InterfaceDeclaration(Name).WithBaseList(CreateBaseList()).WithModifiers(CreateModifiers());
        }
    }
}
