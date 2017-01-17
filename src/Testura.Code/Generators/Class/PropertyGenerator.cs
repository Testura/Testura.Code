using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common;
using Testura.Code.Models;

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
            var propertyDecleration = SyntaxFactory.PropertyDeclaration(
                TypeGenerator.Create(property.Type), SyntaxFactory.Identifier(property.Name))
                .AddAccessorListAccessors(SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).
                    WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));
            if (property.PropertyType == PropertyTypes.GetAndSet)
            {
                propertyDecleration = propertyDecleration.AddAccessorListAccessors(SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).
                     WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));
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
    }
}
