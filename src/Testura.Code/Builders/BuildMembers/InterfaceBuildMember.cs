using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Builders.BuildMembers;

public class InterfaceBuildMember : IBuildMember
{
    private readonly TypeDeclarationSyntax _interface;

    public InterfaceBuildMember(InterfaceBuilder interfaceBuilder)
    {
        _interface = interfaceBuilder.BuildWithoutNamespace();
    }

    public InterfaceBuildMember(TypeDeclarationSyntax @interface)
    {
        _interface = @interface;
    }

    public SyntaxList<MemberDeclarationSyntax> AddMember(SyntaxList<MemberDeclarationSyntax> members)
    {
        return members.Add(_interface);
    }
}
