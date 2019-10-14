using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common;
using Attribute = Testura.Code.Models.Attribute;

namespace Testura.Code.Builders.BuildMembers
{
    public class EnumMember : IBuildMember
    {
        private readonly string _name;
        private readonly IEnumerable<string> _values;
        private readonly IEnumerable<Modifiers> _modifiers;
        private readonly IEnumerable<Attribute> _attributes;

        public EnumMember(
            string name,
            IEnumerable<string> values,
            IEnumerable<Modifiers> modifiers = null,
            IEnumerable<Attribute> attributes = null)
        {
            _name = name;
            _values = values;
            _modifiers = modifiers;
            _attributes = attributes;
        }

        public SyntaxList<MemberDeclarationSyntax> AddMember(SyntaxList<MemberDeclarationSyntax> members)
        {
            return members.Add(EnumGenerator.Create(_name, _values, _modifiers, _attributes));
        }
    }
}
