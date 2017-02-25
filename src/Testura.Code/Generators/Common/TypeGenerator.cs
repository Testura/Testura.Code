using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Models.Types;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Generators.Common
{
    public static class TypeGenerator
    {
        /// <summary>
        /// Create the syntax for a type
        /// </summary>
        /// <param name="type">The type to create</param>
        /// <returns>The declared type syntax</returns>
        public static TypeSyntax Create(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (type is CustomTypeProxy)
            {
                return ParseTypeName(((CustomTypeProxy)type).TypeName);
            }

            if (type.IsGenericType)
            {
                return CreateGenericType(type);
            }

            var typeSyntax = CheckPredefinedTypes(type);
            return typeSyntax ?? ParseTypeName(type.Name);
        }

        private static TypeSyntax CheckPredefinedTypes(Type type)
        {
            if (type == typeof(int))
            {
                return PredefinedType(Token(SyntaxKind.IntKeyword));
            }

            if (type == typeof(double))
            {
                return PredefinedType(Token(SyntaxKind.DoubleKeyword));
            }

            if (type == typeof(long))
            {
                return PredefinedType(Token(SyntaxKind.LongKeyword));
            }

            if (type == typeof(ulong))
            {
                return PredefinedType(Token(SyntaxKind.ULongKeyword));
            }

            if (type == typeof(float))
            {
                return PredefinedType(Token(SyntaxKind.FloatKeyword));
            }

            if (type == typeof(byte))
            {
                return PredefinedType(Token(SyntaxKind.ByteKeyword));
            }

            if (type == typeof(string))
            {
                return PredefinedType(Token(SyntaxKind.StringKeyword));
            }

            if (type == typeof(sbyte))
            {
                return PredefinedType(Token(SyntaxKind.SByteKeyword));
            }

            if (type == typeof(ushort))
            {
                return PredefinedType(Token(SyntaxKind.UShortKeyword));
            }

            if (type == typeof(uint))
            {
                return PredefinedType(Token(SyntaxKind.UIntKeyword));
            }

            if (type == typeof(bool))
            {
                return PredefinedType(Token(SyntaxKind.BoolKeyword));
            }

            if (type == typeof(char))
            {
                return PredefinedType(Token(SyntaxKind.CharKeyword));
            }

            if (type == typeof(decimal))
            {
                return PredefinedType(Token(SyntaxKind.DecimalKeyword));
            }

            return null;
        }

        private static TypeSyntax CreateGenericType(Type type)
        {
            return
                GenericName(Identifier(type.Name.Substring(0, type.Name.LastIndexOf("`", StringComparison.Ordinal)))).WithTypeArgumentList(TypeArgumentList(
                    SeparatedList<TypeSyntax>(GetGenericTypes(type.GetGenericArguments()))));
        }

        private static SyntaxNodeOrToken[] GetGenericTypes(IEnumerable<Type> genericTypes)
        {
            var list = new List<SyntaxNodeOrToken>();
            foreach (var genericType in genericTypes)
            {
                list.Add(Create(genericType));
                list.Add(Token(SyntaxKind.CommaToken));
            }

            list.RemoveAt(list.Count - 1);
            return list.ToArray();
        }
    }
}