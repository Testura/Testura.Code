using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Attribute = Testura.Code.Models.Attribute;

namespace Testura.Code.Generators.Common
{
    public static class EnumGenerator
    {
        public static MemberDeclarationSyntax Create(
            string name,
            IEnumerable<string> values,
            IEnumerable<Modifiers> modifiers = null,
            IEnumerable<Attribute> attributes = null)
        {
            var enumDeclaration = EnumDeclaration(name);

            if (values.Any())
            {
                var enumMemberDeclarations = new List<SyntaxNodeOrToken>();
                foreach (var value in values)
                {
                    enumMemberDeclarations.Add(EnumMemberDeclaration(Identifier(value)));
                    enumMemberDeclarations.Add(Token(SyntaxKind.CommaToken));
                }

                enumMemberDeclarations.RemoveAt(enumMemberDeclarations.Count - 1);

                enumDeclaration = enumDeclaration.WithMembers(SeparatedList<EnumMemberDeclarationSyntax>(enumMemberDeclarations));
            }

            if (modifiers != null)
            {
                enumDeclaration = enumDeclaration.WithModifiers(ModifierGenerator.Create(modifiers.ToArray()));
            }

            if (attributes != null)
            {
                enumDeclaration = enumDeclaration.WithAttributeLists(AttributeGenerator.Create(attributes.ToArray()));
            }

            return enumDeclaration;
        }
    }
}
