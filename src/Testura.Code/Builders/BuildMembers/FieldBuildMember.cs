using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Builders.BuildMembers;

public class FieldBuildMember : IBuildMember
{
    private readonly IEnumerable<FieldDeclarationSyntax> _fieldDeclarationSyntaxs;

    public FieldBuildMember(IEnumerable<FieldDeclarationSyntax> fieldDeclarationSyntaxs)
    {
        _fieldDeclarationSyntaxs = fieldDeclarationSyntaxs;
    }

    public SyntaxList<MemberDeclarationSyntax> AddMember(SyntaxList<MemberDeclarationSyntax> members)
    {
        foreach (var fieldDeclarationSyntax in _fieldDeclarationSyntaxs)
        {
            members = members.Add(fieldDeclarationSyntax);
        }

        return members;
    }
}