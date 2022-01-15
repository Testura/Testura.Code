using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Models;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Attribute = Testura.Code.Models.Attribute;

namespace Testura.Code.Generators.Common;

public static class EnumGenerator
{
    public static MemberDeclarationSyntax Create(
        string name,
        IEnumerable<EnumMember> enumMembers,
        IEnumerable<Modifiers>? modifiers = null,
        IEnumerable<Attribute>? attributes = null)
    {
        var enumDeclaration = EnumDeclaration(name);

        if (enumMembers.Any())
        {
            var enumMemberDeclarations = new List<SyntaxNodeOrToken>();
            foreach (var enumMember in enumMembers)
            {
                enumMemberDeclarations.Add(CreateEnumMemberDeclarationSyntax(enumMember));
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

    private static EnumMemberDeclarationSyntax CreateEnumMemberDeclarationSyntax(EnumMember enumMember)
    {
        var enumMemberDeclarationSyntax = EnumMemberDeclaration(Identifier(enumMember.Name));

        if (enumMember.Value.HasValue)
        {
            enumMemberDeclarationSyntax = enumMemberDeclarationSyntax.WithEqualsValue(
                EqualsValueClause(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(enumMember.Value.Value))));
        }

        if (enumMember.Attributes != null)
        {
            enumMemberDeclarationSyntax = enumMemberDeclarationSyntax.WithAttributeLists(AttributeGenerator.Create(enumMember.Attributes.ToArray()));
        }

        return enumMemberDeclarationSyntax;
    }
}
