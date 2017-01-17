using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Generators.Common
{
    internal static class GenericGenerator
    {
        /// <summary>
        /// Create the syntax for a generic type
        /// </summary>
        /// <param name="name">Name the base (for example List)</param>
        /// <param name="genericTypes">The generic types</param>
        /// <returns>The declared generic name syntax</returns>
        internal static GenericNameSyntax Create(string name, IEnumerable<Type> genericTypes)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (name.Contains("`"))
            {
                name = name.Split('`').First();
            }

            return
                SyntaxFactory.GenericName(SyntaxFactory.Identifier(name))
                    .WithTypeArgumentList(
                        SyntaxFactory.TypeArgumentList(SyntaxFactory.SeparatedList<TypeSyntax>(GetGenericTypes(genericTypes))));
        }

        internal static string ConvertGenericTypeName(Type type)
        {
            if (!type.IsGenericType)
            {
                return type.Name;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(type.Name.Substring(0, type.Name.LastIndexOf("`", StringComparison.Ordinal)));
            sb.Append(type.GetGenericArguments().Aggregate("<", (aggregate, genericType) => aggregate + (aggregate == "<" ? string.Empty : ",") + ConvertGenericTypeName(genericType)));
            sb.Append(">");
            return sb.ToString();
        }

        private static SyntaxNodeOrToken[] GetGenericTypes(IEnumerable<Type> genericTypes)
        {
            var list = new List<SyntaxNodeOrToken>();
            foreach (var genericType in genericTypes)
            {
                list.Add(TypeGenerator.Create(genericType));
                list.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));
            }

            list.RemoveAt(list.Count - 1);
            return list.ToArray();
        }
    }
}
