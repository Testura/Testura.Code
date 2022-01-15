using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Builders.Base;
using Testura.Code.Builders.BuildMembers;
using Testura.Code.Generators.Class;
using Testura.Code.Models;

namespace Testura.Code.Builders;

/// <summary>
/// Provides a builder to generate a struct.
/// </summary>
public class StructBuilder : TypeBuilderBase<StructBuilder>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StructBuilder"/> class.
    /// </summary>
    /// <param name="name">Name of the struct.</param>
    /// <param name="namespace">Name of the struct namespace.</param>
    /// <param name="namespaceType">Type of namespace</param>
    public StructBuilder(string name, string @namespace, NamespaceType namespaceType = NamespaceType.Classic)
        : base(name, @namespace, namespaceType)
    {
    }

    /// <summary>
    /// Add fields.
    /// </summary>
    /// <param name="fields">A set of wanted fields.</param>
    /// <returns>The current class builder</returns>
    public StructBuilder WithFields(params Field[] fields)
    {
        return With(new FieldBuildMember(fields.Select(FieldGenerator.Create)));
    }

    /// <summary>
    /// Add fields.
    /// </summary>
    /// <param name="fields">An array of already declared fields.</param>
    /// <returns>The current class builder</returns>
    public StructBuilder WithFields(params FieldDeclarationSyntax[] fields)
    {
        return With(new FieldBuildMember(fields));
    }

    /// <summary>
    /// Add constructor.
    /// </summary>
    /// <param name="constructor">An already generated constructor.</param>
    /// <returns>The current class builder</returns>
    public StructBuilder WithConstructor(params ConstructorDeclarationSyntax[] constructor)
    {
        return With(new ConstructorBuildMember(constructor));
    }

    protected override TypeDeclarationSyntax BuildBase()
    {
        return SyntaxFactory.StructDeclaration(Name).WithBaseList(CreateBaseList()).WithModifiers(CreateModifiers());
    }
}
