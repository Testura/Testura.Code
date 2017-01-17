using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;
using Testura.Code.Models;

namespace Testura.Code.Generators.Common
{
    public static class AttributeGenerator
    {
        /// <summary>
        /// Create the syntax for an attribute
        /// </summary>
        /// <param name="attributes">Attribute(s) to create</param>
        /// <returns>The declared syntax list</returns>
        public static SyntaxList<AttributeListSyntax> Create(params Attribute[] attributes)
        {
            var attributesSyntax = new AttributeListSyntax[attributes.Length];
            for (int n = 0; n < attributes.Length; n++)
            {
                var attributeSyntax = Create(attributes[n]);
                attributesSyntax[n] =
                    SyntaxFactory.AttributeList(SyntaxFactory.SingletonSeparatedList(attributeSyntax));
            }

            return
                SyntaxFactory.List<AttributeListSyntax>(attributesSyntax);
        }

        private static AttributeSyntax Create(Attribute attribute)
        {
            var attributeSyntax = SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(attribute.Name));
            if (attribute.Arguments.Any())
            {
                attributeSyntax = attributeSyntax.WithArgumentList(GetArguments(attribute.Arguments));
            }

            return attributeSyntax;
        }

        private static AttributeArgumentListSyntax GetArguments(List<IArgument> arguments)
        {
            var list = ConvertArgumentsToSyntaxNodesOrTokens(arguments);
            return SyntaxFactory.AttributeArgumentList(SyntaxFactory.SeparatedList<AttributeArgumentSyntax>(list));
        }

        private static List<SyntaxNodeOrToken> ConvertArgumentsToSyntaxNodesOrTokens(List<IArgument> arguments)
        {
            if (!arguments.Any())
            {
                return new List<SyntaxNodeOrToken>();
            }

            var list = new List<SyntaxNodeOrToken>();

            foreach (var argument in arguments)
            {
                list.Add(SyntaxFactory.AttributeArgument(argument.GetArgumentSyntax().Expression));
                list.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));
            }

            list.RemoveAt(list.Count - 1);
            return list;
        }
    }
}
