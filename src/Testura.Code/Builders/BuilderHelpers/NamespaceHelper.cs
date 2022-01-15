using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Builders.BuilderHelpers;

internal class NamespaceHelper
{
    private readonly NamespaceType _namespaceType;
    private readonly string _name;

    public NamespaceHelper(string name, NamespaceType namespaceType)
    {
        _name = name?.Replace(" ", "_");
        _namespaceType = namespaceType;
    }

    public CompilationUnitSyntax BuildNamespace(CompilationUnitSyntax @base, params MemberDeclarationSyntax[] members)
    {
        switch (_namespaceType)
        {
            case NamespaceType.Classic:
                return @base.WithMembers(SingletonList<MemberDeclarationSyntax>(NamespaceDeclaration(IdentifierName(_name)).AddMembers(members)));

            case NamespaceType.FileScoped:
                return @base.WithMembers(SingletonList<MemberDeclarationSyntax>(FileScopedNamespaceDeclaration(IdentifierName(_name)).AddMembers(members)));

            default:
                throw new ArgumentOutOfRangeException("NameSpaceType", "Not supported namespace type");
        }
    }

    public CompilationUnitSyntax BuildNamespace(CompilationUnitSyntax @base, SyntaxList<MemberDeclarationSyntax> members)
    {
        switch (_namespaceType)
        {
            case NamespaceType.Classic:
                return @base.WithMembers(SingletonList<MemberDeclarationSyntax>(NamespaceDeclaration(IdentifierName(_name)).WithMembers(members)));

            case NamespaceType.FileScoped:
                return @base.WithMembers(SingletonList<MemberDeclarationSyntax>(FileScopedNamespaceDeclaration(IdentifierName(_name)).WithMembers(members)));

            default:
                throw new ArgumentOutOfRangeException("NameSpaceType", "Not supported namespace type");
        }
    }
}
