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
        /// Create a auto property for a class
        /// </summary>
        /// <param name="name">Name of property</param>
        /// <param name="type">Type of property</param>
        /// <returns>A property declaration</returns>
        public static PropertyDeclarationSyntax Create(Property property)
        {
            var propertyDecleration = SyntaxFactory.PropertyDeclaration(
                SyntaxFactory.ParseTypeName(property.Type.Name), SyntaxFactory.Identifier(property.Name))
                .AddAccessorListAccessors(SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).
                    WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));
            if (property.PropertyType == PropertyTypes.GetAndSet)
            {
                propertyDecleration = propertyDecleration.AddAccessorListAccessors(SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).
                     WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                 );
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
