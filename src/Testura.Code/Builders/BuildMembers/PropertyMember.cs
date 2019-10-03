using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Builders.BuildMembers
{
    public class PropertyMember : IBuildMember
    {
        private readonly IEnumerable<PropertyDeclarationSyntax> _propertyDeclarationSyntaxs;

        public PropertyMember(IEnumerable<PropertyDeclarationSyntax> propertyDeclarationSyntaxs)
        {
            _propertyDeclarationSyntaxs = propertyDeclarationSyntaxs;
        }

        public SyntaxList<MemberDeclarationSyntax> AddMember(SyntaxList<MemberDeclarationSyntax> members)
        {
            foreach (var propertyDeclarationSyntax in _propertyDeclarationSyntaxs)
            {
                members = members.Add(propertyDeclarationSyntax);
            }

            return members;
        }
    }
}
