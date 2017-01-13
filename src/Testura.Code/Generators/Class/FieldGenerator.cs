using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common;
using Testura.Code.Models;

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
            var typeName = field.Type.Name;
            if (field.Type.IsGenericType)
            {
                typeName = GenericGenerator.ConvertGenericTypeName(field.Type);
            }

            var fieldDecleration = SyntaxFactory.FieldDeclaration(SyntaxFactory.VariableDeclaration(SyntaxFactory.ParseTypeName(typeName),
                SyntaxFactory.SeparatedList(new[] {SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier(field.Name))})));
            if (field.Modifiers != null)
            {
                fieldDecleration = fieldDecleration.WithModifiers(Common.ModifierGenerator.Create(field.Modifiers.ToArray()));
            }

            return fieldDecleration;
        }
    }
}
