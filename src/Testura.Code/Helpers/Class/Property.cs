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
        public static PropertyDeclarationSyntax Create(string name, Type type, PropertyTypes propertyType)
        {
            return Create(name, type, propertyType, new List<Code.Modifiers>(), new List<Attribute>());
        }


        /// <summary>
        /// Create a auto property for a class
        /// </summary>
        /// <param name="name">Name of property</param>
        /// <param name="type">Type of property</param>
        /// <returns>A property declaration</returns>
        public static PropertyDeclarationSyntax Create(string name, Type type, PropertyTypes propertyType, IList<Code.Modifiers> modifiers)
        {
            return Create(name, type, propertyType, modifiers, new List<Attribute>());
        }

        /// <summary>
        /// Create a auto property for a class
        /// </summary>
        /// <param name="name">Name of property</param>
        /// <param name="type">Type of property</param>
        /// <returns>A property declaration</returns>
        public static PropertyDeclarationSyntax Create(string name, Type type, PropertyTypes propertyType, IList<Code.Modifiers> modifiers, IList<Attribute> attributes)
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

            property = property.WithModifiers(Common.Modifiers.Create(modifiers.ToArray()));
            property = property.WithAttributeLists(Attributes.Create(attributes.ToArray()));
            return property;
        }
    }
}
