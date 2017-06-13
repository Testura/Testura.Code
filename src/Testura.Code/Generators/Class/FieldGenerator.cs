using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common;
using Testura.Code.Models;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Generators.Class
{
    public static class FieldGenerator
    {
        /// <summary>
        /// Create the syntax for a field of a class
        /// </summary>
        /// <param name="field">Field to create</param>
        /// <returns>The declaration syntax for a field</returns>
        public static FieldDeclarationSyntax Create(Field field)
        {
            if (field == null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            var fieldDeclaration = FieldDeclaration(VariableDeclaration(TypeGenerator.Create(field.Type), SeparatedList(new[] { VariableDeclarator(Identifier(field.Name)) })));

            if (field.Modifiers != null)
            {
                fieldDeclaration = fieldDeclaration.WithModifiers(ModifierGenerator.Create(field.Modifiers.ToArray()));
            }

            if (field.Attributes != null)
            {
                fieldDeclaration = fieldDeclaration.WithAttributeLists(AttributeGenerator.Create(field.Attributes.ToArray()));
            }

            return fieldDeclaration;
        }
    }
}
