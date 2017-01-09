using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Helpers.Arguments.ArgumentTypes
{
    public class ArrayInitializationArgument : IArgument
    {
        private readonly Type type;
        private readonly IList<IArgument> arguments;

        public ArrayInitializationArgument(Type type, IList<IArgument> arguments)
        {
            this.type = type;
            this.arguments = arguments;
        }

        public ArgumentSyntax GetArgumentSyntax()
        {
            SyntaxNodeOrToken[] m = new SyntaxNodeOrToken[0];
            if (arguments.Any())
            {
                m = new SyntaxNodeOrToken[arguments.Count * 2 - 1];
                for (int n = 0; n < arguments.Count; n += 2)
                {
                    m[n] = arguments[n].GetArgumentSyntax().Expression;
                    if ((n + 1) < arguments.Count)
                        m[n + 1] = SyntaxFactory.Token(SyntaxKind.CommaToken);
                }

            }
            return
                SyntaxFactory.Argument(
                    SyntaxFactory.ArrayCreationExpression(
                            SyntaxFactory.ArrayType(SyntaxFactory.IdentifierName(type.Name))
                                .WithRankSpecifiers(
                                    SyntaxFactory.SingletonList<ArrayRankSpecifierSyntax>(
                                        SyntaxFactory.ArrayRankSpecifier(
                                            SyntaxFactory.SingletonSeparatedList<ExpressionSyntax>(SyntaxFactory.OmittedArraySizeExpression())))))
                        .WithInitializer(SyntaxFactory.InitializerExpression(SyntaxKind.ArrayInitializerExpression,
                            SyntaxFactory.SeparatedList<ExpressionSyntax>(m))));
        }
    }
}