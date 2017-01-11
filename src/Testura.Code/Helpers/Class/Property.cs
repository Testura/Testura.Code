using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Helpers.Common;
using Attribute = Testura.Code.Models.Attribute;

namespace Testura.Code.Helpers.Class
{
    public enum PropertyTypes
    {
        Get,
        GetAndSet
    }

    public static class Property
    {
        /// <summary>
        /// Create a auto property for a class
        /// </summary>
        /// <param name="name">Name of property</param>
        /// <param name="type">Type of property</param>
        /// <returns>A property declaration</returns>
        public static PropertyDeclarationSyntax Create(
            string name,
            Type type,
            PropertyTypes propertyType,
            IEnumerable<Modifiers> modifiers = null,
            IEnumerable<Attribute> attributes = null)
        {
            var property = SyntaxFactory.PropertyDeclaration(
                SyntaxFactory.ParseTypeName(type.Name), SyntaxFactory.Identifier(name))
                .AddAccessorListAccessors(SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).
                    WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));
            if (propertyType == PropertyTypes.GetAndSet)
            {
                property = property.AddAccessorListAccessors(SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).
                     WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                 );
            }

            if (modifiers != null)
            {
                property = property.WithModifiers(Common.Modifiers.Create(modifiers.ToArray()));
            }

            if (attributes != null)
            {
                property = property.WithAttributeLists(Attributes.Create(attributes.ToArray()));
            }

            return property;
        }
    }
}
