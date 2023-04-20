using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Builders.BuildMembers;

public class RegionBuildMember : IBuildMember
{
    private readonly string _regionName;
    private readonly IEnumerable<IBuildMember> _members;

    public RegionBuildMember(string regionName, IEnumerable<IBuildMember> members)
    {
        _regionName = regionName;
        _members = members;
    }

    public SyntaxList<MemberDeclarationSyntax> AddMember(SyntaxList<MemberDeclarationSyntax> members)
    {
        if (!_members.Any())
        {
            return members;
        }

        var newMembersSyntaxList = default(SyntaxList<MemberDeclarationSyntax>);

        foreach (var buildMember in _members)
        {
            newMembersSyntaxList = buildMember.AddMember(newMembersSyntaxList);
        }

        newMembersSyntaxList = AddStartRegion(newMembersSyntaxList);
        newMembersSyntaxList = AddEndRegion(newMembersSyntaxList);

        foreach (var memberDeclarationSyntax in newMembersSyntaxList)
        {
            members = members.Add(memberDeclarationSyntax);
        }

        return members;
    }

    public SyntaxList<MemberDeclarationSyntax> AddStartRegion(SyntaxList<MemberDeclarationSyntax> newMembersSyntaxList)
    {
        // A bit hackish.. see if there are a better solution.
        var modifiedFirstMember = newMembersSyntaxList
            .First()
            .WithLeadingTrivia(
                TriviaList(
                    Trivia(
                        RegionDirectiveTrivia(true)
                            .WithEndOfDirectiveToken(
                                Token(TriviaList(PreprocessingMessage($" {_regionName} {Environment.NewLine}")), SyntaxKind.EndOfDirectiveToken, TriviaList())))));

        return newMembersSyntaxList.Replace(newMembersSyntaxList.First(), modifiedFirstMember);
    }

    public SyntaxList<MemberDeclarationSyntax> AddEndRegion(SyntaxList<MemberDeclarationSyntax> newMembersSyntaxList)
    {
        var modifiedLastMember = newMembersSyntaxList
            .Last()
            .WithTrailingTrivia(TriviaList(Trivia(EndRegionDirectiveTrivia(true))));

        return newMembersSyntaxList.Replace(newMembersSyntaxList.Last(), modifiedLastMember);
    }
}
