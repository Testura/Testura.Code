using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common;
using Testura.Code.Models.Properties;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Generators.Class
{
    public static class PropertyGenerator
    {
        /// <summary>
        /// Create the syntax for a property of a class
        /// </summary>
        /// <param name="property">The property to create</param>
        /// <returns>The decleration syntax for a property</returns>
        public static PropertyDeclarationSyntax Create(Property property)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            PropertyDeclarationSyntax propertyDecleration;
            if (property is AutoProperty)
            {
                propertyDecleration = CreateAutoProperty((AutoProperty)property);
            }
            else if (property is BodyProperty)
            {
                propertyDecleration = CreateBodyProperty((BodyProperty)property);
            }
            else
            {
                throw new ArgumentException($"Unkown property type: {property.Type}, could not generate code.");
            }

            if (property.Modifiers != null)
            {
                propertyDecleration = propertyDecleration.WithModifiers(ModifierGenerator.Create(property.Modifiers.ToArray()));
            }

            if (property.Attributes != null)
            {
                propertyDecleration = propertyDecleration.WithAttributeLists(AttributeGenerator.Create(property.Attributes.ToArray()));
            }

            return propertyDecleration;
        }

        private static PropertyDeclarationSyntax CreateAutoProperty(AutoProperty property)
        {
            var propertyDecleration = PropertyDeclaration(
                TypeGenerator.Create(property.Type), Identifier(property.Name))
                .AddAccessorListAccessors(AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).
                    WithSemicolonToken(Token(SyntaxKind.SemicolonToken)));
            if (property.PropertyType == PropertyTypes.GetAndSet)
            {
                propertyDecleration = propertyDecleration.AddAccessorListAccessors(AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).
                     WithSemicolonToken(Token(SyntaxKind.SemicolonToken)));
            }

            return propertyDecleration;
        }

        private static PropertyDeclarationSyntax CreateBodyProperty(BodyProperty property)
        {
            var propertyDecleration = PropertyDeclaration(
                    TypeGenerator.Create(property.Type), Identifier(property.Name))
                .AddAccessorListAccessors(
                    AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithBody(property.GetBody));
            if (property.SetBody != null)
            {
                propertyDecleration =
                    propertyDecleration.AddAccessorListAccessors(
                        AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithBody(property.SetBody));
            }

            return propertyDecleration;
        }
    }
}
