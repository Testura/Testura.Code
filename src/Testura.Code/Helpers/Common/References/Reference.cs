﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Helpers.Common.Arguments;
using Testura.Code.Helpers.Common.Arguments.ArgumentTypes;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Helpers.Common.References
{
    public class Reference
    {
        /// <summary>
        /// Generate the code for a variable reference chain, for example: 
        /// 
        /// myVariable.SomeMethod().MyProperty
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        public static ExpressionSyntax Create(VariableReference reference)
        {
            ExpressionSyntax baseExpression;

            if (reference is MethodReference)
            {
                var methodReference = reference as MethodReference;
                if (methodReference.GenericTypes.Any())
                {
                    baseExpression = InvocationExpression(Generic.Create(methodReference.Name, methodReference.GenericTypes.ToArray()))
                        .WithArgumentList(Arguments.Argument.Create(methodReference.Arguments.ToArray()));
                }
                else
                {
                    baseExpression = InvocationExpression(IdentifierName(methodReference.Name))
                        .WithArgumentList(Arguments.Argument.Create(methodReference.Arguments.ToArray()));
                }
            }
            else
            {
                baseExpression = IdentifierName(reference.Name);
                if (reference.Member == null)
                {
                    return baseExpression;
                }
            }

            return Generate(baseExpression, reference.Member);
        }

        /// <summary>
        /// Generate the code for a member chain. This method is used if you already have a variable, member or method invocation and want to 
        /// extend it with more references calls. 
        /// </summary>
        /// <param name="invocation"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public static ExpressionSyntax Create(ExpressionSyntax invocation, MemberReference reference)
        {
           return Generate(invocation, reference);
        }

        /// <summary>
        /// Generate the code for a member reference
        /// </summary>
        /// <param name="expressionSyntax"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        private static ExpressionSyntax Generate(ExpressionSyntax expressionSyntax, MemberReference current)
        {
            if (current == null)
                return expressionSyntax;
            if (current is MethodReference)
            {
                var methodReference = current as MethodReference;
                if (methodReference.GenericTypes.Any())
                {
                    expressionSyntax = InvocationExpression(
                            expression: MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                                expressionSyntax,
                                Generic.Create(current.Name, methodReference.GenericTypes.ToArray())))
                        .WithArgumentList(Arguments.Argument.Create(methodReference.Arguments.ToArray()));
                }
                else
                {
                    expressionSyntax = InvocationExpression(
                            MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expressionSyntax,
                                IdentifierName(current.Name)))
                        .WithArgumentList(Arguments.Argument.Create(methodReference.Arguments.ToArray()));
                }
            }
            else if (current is MemberReference)
            {
                expressionSyntax = SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expressionSyntax, IdentifierName(current.Name));
            }

            return Generate(expressionSyntax, current.Member);
        }

    }
}
