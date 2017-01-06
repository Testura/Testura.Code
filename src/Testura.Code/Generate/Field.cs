using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Generate
{
    public static class Field
    {
        /// <summary>
        /// Create a new field for a class 
        /// </summary>
        /// <param name="name">Name of field</param>
        /// <param name="type">Type of field</param>
        /// <returns></returns>
        public static FieldDeclarationSyntax Create(string name, Type type)
        {
            var typeName = type.Name;
            if (type.IsGenericType)
            {
                typeName = NameConverters.ConvertGenericTypeName(type);
            }

            return SyntaxFactory.FieldDeclaration(SyntaxFactory.VariableDeclaration(SyntaxFactory.ParseTypeName(typeName),
                SyntaxFactory.SeparatedList(new[] { SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier(name)) }))).
                AddModifiers(SyntaxFactory.Token(SyntaxKind.PrivateKeyword));
        }
    }
}
