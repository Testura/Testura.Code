using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Generate
{
    public static class Generic
    {
        /// <summary>
        /// Create a new generic type for a method or class 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="genericTypes"></param>
        /// <returns></returns>
        public static GenericNameSyntax Create(string name, params Type[] genericTypes)
        {
            if (name.Contains("`"))
                name = name.Split('`').First();
            return
                SyntaxFactory.GenericName(SyntaxFactory.Identifier(name))
                    .WithTypeArgumentList(
                        SyntaxFactory.TypeArgumentList(SyntaxFactory.SeparatedList<TypeSyntax>(GetGenericTypes(genericTypes))));
        }

        private static SyntaxNodeOrToken[] GetGenericTypes(IList<Type> genericTypes)
        {
            var list = new List<SyntaxNodeOrToken>();
            foreach (var genericType in genericTypes)
            {
                list.Add(SyntaxFactory.IdentifierName(genericType.ToString()));
                list.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));
            }
            list.RemoveAt(list.Count - 1);
            return list.ToArray();
        }
    }
}
