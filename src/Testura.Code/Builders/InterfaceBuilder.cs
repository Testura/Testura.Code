using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Builders.Base;

namespace Testura.Code.Builders;

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
    /// <param name="namespaceType">Type of namespace</param>
    public InterfaceBuilder(string name, string @namespace, NamespaceType namespaceType = NamespaceType.Classic)
        : base(name, @namespace, namespaceType)
    {
    }

    protected override TypeDeclarationSyntax BuildBase()
    {
        return SyntaxFactory.InterfaceDeclaration(Name).WithBaseList(CreateBaseList()).WithModifiers(CreateModifiers());
    }
}
