using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common;
using Testura.Code.Generators.Special;
using Testura.Code.Models.Properties;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Generators.Class;

/// <summary>
/// Provides the functionality to generate properties.
/// </summary>
public static class PropertyGenerator
{
    /// <summary>
    /// Create the syntax for a property of a class.
    /// </summary>
    /// <param name="property">The property to create.</param>
    /// <returns>The declaration syntax for a property.</returns>
    public static PropertyDeclarationSyntax Create(Property property)
    {
        if (property == null)
        {
            throw new ArgumentNullException(nameof(property));
        }

        PropertyDeclarationSyntax propertyDeclaration;
        if (property is AutoProperty autoProperty)
        {
            propertyDeclaration = CreateAutoProperty(autoProperty);
        }
        else if (property is BodyProperty bodyProperty)
        {
            propertyDeclaration = CreateBodyProperty(bodyProperty);
        }
        else if (property is ArrowExpressionProperty arrowExpressionProperty)
        {
            propertyDeclaration = CreateArrowExpressionProperty(arrowExpressionProperty);
        }
        else
        {
            throw new ArgumentException($"Unknown property type: {property.Type}, could not generate code.");
        }

        if (property.Modifiers != null)
        {
            propertyDeclaration = propertyDeclaration.WithModifiers(ModifierGenerator.Create(property.Modifiers.ToArray()));
        }

        if (property.Attributes != null)
        {
            propertyDeclaration = propertyDeclaration.WithAttributeLists(AttributeGenerator.Create(property.Attributes.ToArray()));
        }

        if (!string.IsNullOrEmpty(property.Summary))
        {
            propertyDeclaration = propertyDeclaration.WithSummary(property.Summary);
        }

        return propertyDeclaration;
    }

    private static PropertyDeclarationSyntax CreateAutoProperty(AutoProperty property)
    {
        var propertyDeclaration = PropertyDeclaration(
                TypeGenerator.Create(property.Type), Identifier(property.Name))
            .AddAccessorListAccessors(CreateAccessDeclaration(SyntaxKind.GetAccessorDeclaration, null, property.GetModifiers));

        if (property.PropertyType == PropertyTypes.GetAndSet)
        {
            propertyDeclaration = propertyDeclaration.AddAccessorListAccessors(CreateAccessDeclaration(SyntaxKind.SetAccessorDeclaration, null, property.SetModifiers));
        }

        return propertyDeclaration;
    }

    private static PropertyDeclarationSyntax CreateBodyProperty(BodyProperty property)
    {
        var propertyDeclaration = PropertyDeclaration(
                TypeGenerator.Create(property.Type), Identifier(property.Name))
            .AddAccessorListAccessors(CreateAccessDeclaration(SyntaxKind.GetAccessorDeclaration, property.GetBody, property.GetModifiers));

        if (property.SetBody != null)
        {
            propertyDeclaration =
                propertyDeclaration.AddAccessorListAccessors(CreateAccessDeclaration(SyntaxKind.SetAccessorDeclaration, property.SetBody, property.SetModifiers));
        }

        return propertyDeclaration;
    }

    private static PropertyDeclarationSyntax CreateArrowExpressionProperty(ArrowExpressionProperty property)
    {
        var propertyDeclaration = PropertyDeclaration(
                TypeGenerator.Create(property.Type), Identifier(property.Name))
            .WithExpressionBody(ArrowExpressionClause(property.ExpressionSyntax));

        if (property.AddSemicolon)
        {
            propertyDeclaration = propertyDeclaration.WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }

        return propertyDeclaration;
    }

    private static AccessorDeclarationSyntax CreateAccessDeclaration(SyntaxKind kind, BlockSyntax? body, IEnumerable<Modifiers>? modifiers)
    {
        var accessDeclaration = AccessorDeclaration(kind);

        if (modifiers != null)
        {
            accessDeclaration = accessDeclaration.WithModifiers(ModifierGenerator.Create(modifiers.ToArray()));
        }

        accessDeclaration = body != null ? accessDeclaration.WithBody(body) : accessDeclaration.WithSemicolonToken(Token(SyntaxKind.SemicolonToken));

        return accessDeclaration;
    }
}
