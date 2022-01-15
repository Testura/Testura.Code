using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Builders.BuildMembers;

namespace Testura.Code.Builders.BuilderHelpers;

internal class MemberHelper
{
    public MemberHelper()
    {
        Members = new List<IBuildMember>();
    }

    public List<IBuildMember> Members { get; }

    public MemberHelper AddMembers(params IBuildMember[] members)
    {
        Members.AddRange(members);
        return this;
    }

    public TypeDeclarationSyntax BuildMembers(TypeDeclarationSyntax type)
    {
        return type.WithMembers(BuildSyntaxList());
    }

    public CompilationUnitSyntax BuildMembers(CompilationUnitSyntax compilationUnitSyntax)
    {
        return compilationUnitSyntax.WithMembers(BuildSyntaxList());
    }

    public SyntaxList<MemberDeclarationSyntax> BuildSyntaxList()
    {
        var members = default(SyntaxList<MemberDeclarationSyntax>);

        foreach (var member in Members)
        {
            members = member.AddMember(members);
        }

        return members;
    }
}