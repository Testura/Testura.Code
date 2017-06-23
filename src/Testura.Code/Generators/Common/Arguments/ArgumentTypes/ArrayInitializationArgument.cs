using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
#pragma warning disable 1591

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes
{
    /// <summary>
    /// Provices the functionality to generate a array initialization argument. Example of generated code:
    /// <c>(new int[] { 1, 2, test.MyInt })</c>
    /// </summary>
    public class ArrayInitializationArgument : Argument
    {
        private readonly Type _type;
        private readonly IList<IArgument> _arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayInitializationArgument"/> class.
        /// </summary>
        /// <param name="type">Base type of the array.</param>
        /// <param name="arguments">Values or references used in the array initialization.</param>
        /// <param name="namedArgument">Specificy the argument for a partical parameter.</param>
        public ArrayInitializationArgument(Type type, IEnumerable<IArgument> arguments, string namedArgument = null)
            : base(namedArgument)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            _type = type;
            _arguments = arguments == null ? new List<IArgument>() : new List<IArgument>(arguments);
        }

        protected override ArgumentSyntax CreateArgumentSyntax()
        {
            SyntaxNodeOrToken[] syntaxNodeOrTokens = new SyntaxNodeOrToken[0];
            if (_arguments.Any())
            {
                syntaxNodeOrTokens = new SyntaxNodeOrToken[(_arguments.Count * 2) - 1];
                var argumentIndex = 0;
                for (int i = 0; i < syntaxNodeOrTokens.Length; i += 2)
                {
                    syntaxNodeOrTokens[i] = _arguments[argumentIndex].GetArgumentSyntax().Expression;
                    if ((i + 1) < syntaxNodeOrTokens.Length)
                    {
                        syntaxNodeOrTokens[i + 1] = Token(SyntaxKind.CommaToken);
                    }

                    argumentIndex++;
                }
            }

            return Argument(ArrayCreationExpression(ArrayType(TypeGenerator.Create(_type))
                                .WithRankSpecifiers(SingletonList<ArrayRankSpecifierSyntax>(ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(OmittedArraySizeExpression())))))
                                .WithInitializer(InitializerExpression(SyntaxKind.ArrayInitializerExpression, SeparatedList<ExpressionSyntax>(syntaxNodeOrTokens))));
        }
    }
}