using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common;
using Testura.Code.Models.Properties;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Generators.Class
{
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
            if (property is AutoProperty)
            {
                propertyDeclaration = CreateAutoProperty((AutoProperty)property);
            }
            else if (property is BodyProperty)
            {
                propertyDeclaration = CreateBodyProperty((BodyProperty)property);
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

            return propertyDeclaration;
        }

        private static PropertyDeclarationSyntax CreateAutoProperty(AutoProperty property)
        {
            var propertyDeclaration = PropertyDeclaration(
                TypeGenerator.Create(property.Type), Identifier(property.Name))
                .AddAccessorListAccessors(AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).
                    WithSemicolonToken(Token(SyntaxKind.SemicolonToken)));
            if (property.PropertyType == PropertyTypes.GetAndSet)
            {
                propertyDeclaration = propertyDeclaration.AddAccessorListAccessors(AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).
                     WithSemicolonToken(Token(SyntaxKind.SemicolonToken)));
            }

            return propertyDeclaration;
        }

        private static PropertyDeclarationSyntax CreateBodyProperty(BodyProperty property)
        {
            var propertyDeclaration = PropertyDeclaration(
                    TypeGenerator.Create(property.Type), Identifier(property.Name))
                .AddAccessorListAccessors(
                    AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithBody(property.GetBody));
            if (property.SetBody != null)
            {
                propertyDeclaration =
                    propertyDeclaration.AddAccessorListAccessors(
                        AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithBody(property.SetBody));
            }

            return propertyDeclaration;
        }
    }
}
