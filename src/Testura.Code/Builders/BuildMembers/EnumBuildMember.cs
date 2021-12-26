using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common;
using Testura.Code.Models;
using Attribute = Testura.Code.Models.Attribute;

namespace Testura.Code.Builders.BuildMembers
{
    public class EnumBuildMember : IBuildMember
    {
        private readonly string _name;
        private readonly IEnumerable<EnumMember> _members;
        private readonly IEnumerable<Modifiers> _modifiers;
        private readonly IEnumerable<Attribute> _attributes;

        public EnumBuildMember(
            string name,
            IEnumerable<EnumMember> members,
            IEnumerable<Modifiers> modifiers = null,
            IEnumerable<Attribute> attributes = null)
        {
            _name = name;
            _members = members;
            _modifiers = modifiers;
            _attributes = attributes;
        }

        public SyntaxList<MemberDeclarationSyntax> AddMember(SyntaxList<MemberDeclarationSyntax> members)
        {
            return members.Add(EnumGenerator.Create(_name, _members, _modifiers, _attributes));
        }
    }
}
