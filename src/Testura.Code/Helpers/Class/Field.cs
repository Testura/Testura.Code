using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Helpers.Common;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Helpers.Class
{
    public static class Field
    {
        /// <summary>
        /// Create a new field for a class 
        /// </summary>
        /// <param name="name">Name of field</param>
        /// <param name="type">Type of field</param>
        /// <returns></returns>
        public static FieldDeclarationSyntax Create(
            string name,
            Type type,
            IList<Modifiers> modifiers = null)
        {
            var typeName = type.Name;
            if (type.IsGenericType)
            {
                typeName = Generic.ConvertGenericTypeName(type);
            }

            var field = FieldDeclaration(VariableDeclaration(ParseTypeName(typeName),
                SeparatedList(new[] {VariableDeclarator(Identifier(name))})));
            if (modifiers != null)
            {
                field = field.WithModifiers(Common.Modifiers.Create(modifiers.ToArray()));
            }

            return field;
        }
    }
}
