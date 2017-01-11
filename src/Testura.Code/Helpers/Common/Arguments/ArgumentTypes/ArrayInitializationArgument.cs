using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Helpers.Common.Arguments.ArgumentTypes
{
    public class ArrayInitializationArgument : IArgument
    {
        private readonly Type _type;
        private readonly IList<IArgument> _arguments;

        public ArrayInitializationArgument(Type type, IEnumerable<IArgument> arguments)
        {
            _type = type;
            _arguments = new List<IArgument>(arguments);
        }

        public ArgumentSyntax GetArgumentSyntax()
        {
            SyntaxNodeOrToken[] m = new SyntaxNodeOrToken[0];
            if (_arguments.Any())
            {
                m = new SyntaxNodeOrToken[_arguments.Count * 2 - 1];
                int argumentIndex = 0;
                for (int n = 0; n < m.Length; n += 2)
                {
                    m[n] = _arguments[argumentIndex].GetArgumentSyntax().Expression;
                    if ((n + 1) < m.Length)
                        m[n + 1] = SyntaxFactory.Token(SyntaxKind.CommaToken);
                    argumentIndex++;
                }

            }

            return
                SyntaxFactory.Argument(
                    SyntaxFactory.ArrayCreationExpression(
                            SyntaxFactory.ArrayType(SyntaxFactory.IdentifierName(_type.Name))
                                .WithRankSpecifiers(
                                    SyntaxFactory.SingletonList<ArrayRankSpecifierSyntax>(
                                        SyntaxFactory.ArrayRankSpecifier(
                                            SyntaxFactory.SingletonSeparatedList<ExpressionSyntax>(SyntaxFactory.OmittedArraySizeExpression())))))
                        .WithInitializer(SyntaxFactory.InitializerExpression(SyntaxKind.ArrayInitializerExpression,
                            SyntaxFactory.SeparatedList<ExpressionSyntax>(m))));
        }
    }
}