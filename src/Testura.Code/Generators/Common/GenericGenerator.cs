using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Generators.Common
{
    public static class GenericGenerator
    {
        /// <summary>
        /// Create a new generic type for a method or class 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="genericTypes"></param>
        /// <returns></returns>
        public static GenericNameSyntax Create(string name, IEnumerable<Type> genericTypes)
        {
            if (name.Contains("`"))
                name = name.Split('`').First();
            return
                SyntaxFactory.GenericName(SyntaxFactory.Identifier(name))
                    .WithTypeArgumentList(
                        SyntaxFactory.TypeArgumentList(SyntaxFactory.SeparatedList<TypeSyntax>(GetGenericTypes(genericTypes))));
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

        /// <summary>
        /// This method is used to convert generic type names (for example list&lt;string&gt; would normally give list`1String) to
        /// a more readable name.
        /// </summary>
        /// <param name="type">The type we want to convert name from</param>
        /// <returns>Converted name</returns>
        internal static string ConvertGenericTypeName(Type type)
        {
            if (!type.IsGenericType)
                return type.Name;
            StringBuilder sb = new StringBuilder();
            sb.Append(type.Name.Substring(0, type.Name.LastIndexOf("`", StringComparison.Ordinal)));
            sb.Append(type.GetGenericArguments().Aggregate("<", (aggregate, genericType) => aggregate + (aggregate == "<" ? "" : ",") + ConvertGenericTypeName(genericType)));
            sb.Append(">");
            return sb.ToString();
        }
    }
}
