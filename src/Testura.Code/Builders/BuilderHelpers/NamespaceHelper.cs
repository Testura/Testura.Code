using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Builders.BuilderHelpers;

internal class NamespaceHelper
{
    private readonly string _name;

    public NamespaceHelper(string name)
    {
        _name = name?.Replace(" ", "_");
    }

    public CompilationUnitSyntax BuildNamespace(CompilationUnitSyntax @base, params MemberDeclarationSyntax[] members)
    {
        return @base.WithMembers(SingletonList<MemberDeclarationSyntax>(NamespaceDeclaration(IdentifierName(_name)).AddMembers(members)));
    }

    public CompilationUnitSyntax BuildNamespace(CompilationUnitSyntax @base, SyntaxList<MemberDeclarationSyntax> members)
    {
        return @base.WithMembers(SingletonList<MemberDeclarationSyntax>(NamespaceDeclaration(IdentifierName(_name)).WithMembers(members)));
    }
}