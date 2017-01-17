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
        /// <returns>The decleration syntax for a field</returns>
        public static FieldDeclarationSyntax Create(Field field)
        {
            if (field == null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            var fieldDecleration = FieldDeclaration(VariableDeclaration(TypeGenerator.Create(field.Type), SeparatedList(new[] { VariableDeclarator(Identifier(field.Name)) })));
            if (field.Modifiers != null)
            {
                fieldDecleration = fieldDecleration.WithModifiers(ModifierGenerator.Create(field.Modifiers.ToArray()));
            }

            return fieldDecleration;
        }
    }
}
