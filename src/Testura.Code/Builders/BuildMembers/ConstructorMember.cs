using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Builders.BuildMembers
{
    public class ConstructorMember : IBuildMember
    {
        private readonly IEnumerable<ConstructorDeclarationSyntax> _constructorDeclarationSyntax;

        public ConstructorMember(IEnumerable<ConstructorDeclarationSyntax> constructorDeclarationSyntax)
        {
            _constructorDeclarationSyntax = constructorDeclarationSyntax;
        }

        public SyntaxList<MemberDeclarationSyntax> AddMember(SyntaxList<MemberDeclarationSyntax> members)
        {
            foreach (var constructorDeclarationSyntax in _constructorDeclarationSyntax)
            {
                members = members.Add(constructorDeclarationSyntax);
            }

            return members;
        }
    }
}
