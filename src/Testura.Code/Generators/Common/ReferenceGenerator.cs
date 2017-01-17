using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Models.References;

namespace Testura.Code.Generators.Common
{
    public class ReferenceGenerator
    {
        /// <summary>
        /// Create the expression syntax for a variable, nethod or a chain of member/method(s).
        /// </summary>
        /// <param name="reference">The start reference</param>
        /// <returns>The declared expression syntax</returns>
        public static ExpressionSyntax Create(VariableReference reference)
        {
            if (reference == null)
            {
                throw new ArgumentNullException(nameof(reference));
            }

            ExpressionSyntax baseExpression;

            if (reference is MethodReference)
            {
                var methodReference = reference as MethodReference;
                if (methodReference.GenericTypes.Any())
                {
                    baseExpression = SyntaxFactory.InvocationExpression(GenericGenerator.Create(methodReference.Name, methodReference.GenericTypes.ToArray()))
                        .WithArgumentList(ArgumentGenerator.Create(methodReference.Arguments.ToArray()));
                }
                else
                {
                    baseExpression = SyntaxFactory.InvocationExpression(SyntaxFactory.IdentifierName(methodReference.Name))
                        .WithArgumentList(ArgumentGenerator.Create(methodReference.Arguments.ToArray()));
                }
            }
            else
            {
                baseExpression = SyntaxFactory.IdentifierName(reference.Name);
                if (reference.Member == null)
                {
                    return baseExpression;
                }
            }

            return Generate(baseExpression, reference.Member);
        }

        /// <summary>
        /// Create the expression syntax for a chain of member/method(s).
        /// </summary>
        /// <param name="expression">The expression to build upon</param>
        /// <param name="reference">Next member reference in chain</param>
        /// <returns>The declared expression syntax</returns>
        public static ExpressionSyntax Create(ExpressionSyntax expression, MemberReference reference)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            if (reference == null)
            {
                throw new ArgumentNullException(nameof(reference));
            }

            return Generate(expression, reference);
        }

        private static ExpressionSyntax Generate(ExpressionSyntax expressionSyntax, MemberReference reference)
        {
            if (reference == null)
            {
                return expressionSyntax;
            }

            if (reference is MethodReference)
            {
                var methodReference = reference as MethodReference;
                if (methodReference.GenericTypes.Any())
                {
                    expressionSyntax = SyntaxFactory.InvocationExpression(
                            expression: SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                                expressionSyntax,
                                GenericGenerator.Create(reference.Name, methodReference.GenericTypes.ToArray())))
                        .WithArgumentList(ArgumentGenerator.Create(methodReference.Arguments.ToArray()));
                }
                else
                {
                    expressionSyntax = SyntaxFactory.InvocationExpression(
                            SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expressionSyntax,
                                SyntaxFactory.IdentifierName(reference.Name)))
                        .WithArgumentList(ArgumentGenerator.Create(methodReference.Arguments.ToArray()));
                }
            }
            else if (reference is MemberReference)
            {
                expressionSyntax = SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expressionSyntax, SyntaxFactory.IdentifierName(reference.Name));
            }

            return Generate(expressionSyntax, reference.Member);
        }
    }
}
