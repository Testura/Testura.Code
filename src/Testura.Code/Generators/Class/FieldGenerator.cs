using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common;
using Testura.Code.Models;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Generators.Class
{
    public static class FieldGenerator
    {
        /// <summary>
        /// Create a new field for a class 
        /// </summary>
        /// <param name="name">Name of field</param>
        /// <param name="type">Type of field</param>
        /// <returns></returns>
        public static FieldDeclarationSyntax Create(Field field)
        {
            var fieldDecleration = FieldDeclaration(VariableDeclaration(TypeGenerator.Create(field.Type), SeparatedList(new[] { VariableDeclarator(Identifier(field.Name)) })));
            if (field.Modifiers != null)
            {
                fieldDecleration = fieldDecleration.WithModifiers(ModifierGenerator.Create(field.Modifiers.ToArray()));
            }

            return fieldDecleration;
        }
    }
}
