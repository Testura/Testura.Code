using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generate.ArgumentTypes;

namespace Testura.Code.Generate
{
    /// <summary>
    /// Generate code for attributes
    /// </summary>
    public static class Attributes
    {
        /// <summary>
        /// Create a new attribute
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static SyntaxList<AttributeListSyntax> Create(params Attribute[] attributes)
        {
            var attributesSyntax = new AttributeListSyntax[attributes.Length];
            for(int n = 0; n < attributes.Length; n++)
            {
                var attribute = attributes[n];
                var attributeSyntax = SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(attribute.Name));
                if (attribute.Arguments.Any())
                {
                    attributeSyntax = attributeSyntax.WithArgumentList(GetArguments(attribute.Arguments));
                }
                attributesSyntax[n] =
                    SyntaxFactory.AttributeList(SyntaxFactory.SingletonSeparatedList(attributeSyntax));
            } 
            return
                SyntaxFactory.List<AttributeListSyntax>(attributesSyntax);
        }

        private static AttributeArgumentListSyntax GetArguments(List<IArgument> arguments)
        {
            var list = ConvertArgumentsToSyntaxNodesOrTokens(arguments);
            return SyntaxFactory.AttributeArgumentList(SyntaxFactory.SeparatedList<AttributeArgumentSyntax>(list));
        }

        private static List<SyntaxNodeOrToken> ConvertArgumentsToSyntaxNodesOrTokens(List<IArgument> arguments)
        {
            if (!arguments.Any())
                return new List<SyntaxNodeOrToken>();
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

    public class Attribute
    {
        /// <summary>
        /// Gets or sets the name of the attribute
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the argument to the attribute
        /// </summary>
        public List<IArgument> Arguments { get; set; }

        public Attribute(string name)
        {
            Name = name;
            Arguments = new List<IArgument>();
        }

        public Attribute(string name, List<IArgument> arguments)
        {
            Name = name;
            Arguments = arguments;
        } 
    }
}
