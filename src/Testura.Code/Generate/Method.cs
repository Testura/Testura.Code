using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Generate
{
    /// <summary>
    /// Generate code for a method
    /// </summary>
    public static class Method
    {
        /// <summary>
        /// Create code to invoke a method on class
        /// </summary>
        /// <param name="variableName">Variable to invoke</param>
        /// <param name="methodName">The method we want to call</param>
        /// <param name="arguments">Arguments in the method</param>
        /// <param name="genericTypes">Optional list of types if the method is generic</param>
        /// <returns>A statement syntax</returns>
        public static Invocation Invoke(string variableName, string methodName, ArgumentListSyntax arguments, IList<Type> genericTypes = null)
        {
            InvocationExpressionSyntax invocationExpressionSyntax;
            if (genericTypes != null && genericTypes.Any())
            {
                invocationExpressionSyntax = SyntaxFactory.InvocationExpression(
                    SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression, 
                        SyntaxFactory.IdentifierName(variableName),
                        Generic.Create(methodName, genericTypes)));
            }
            else
            {
                invocationExpressionSyntax = SyntaxFactory.InvocationExpression(
                    SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression, 
                        SyntaxFactory.IdentifierName(variableName),
                        SyntaxFactory.IdentifierName(methodName)));
            }

            return arguments != null ? new Invocation(invocationExpressionSyntax.WithArgumentList(arguments)) : new Invocation(invocationExpressionSyntax);
        }

        /// <summary>
        /// Create code to invoke a method on class
        /// </summary>
        /// <param name="methodInvocation">Variable to invoke</param>
        /// <param name="methodName">The method we want to call</param>
        /// <param name="arguments">Arguments in the method</param>
        /// <param name="genericTypes">Optional list of types if the method is generic</param>
        /// <returns>A statement syntax</returns>
        public static Invocation Invoke(Invocation methodInvocation, string methodName, ArgumentListSyntax arguments, IList<Type> genericTypes = null)
        {
            InvocationExpressionSyntax invocationExpressionSyntax;
            if (genericTypes != null && genericTypes.Any())
            {
                invocationExpressionSyntax = SyntaxFactory.InvocationExpression(
                    SyntaxFactory.MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression, 
                            methodInvocation.AsInvocationStatment(),
                            Generic.Create(methodName, genericTypes)));
            }
            else
            {
                invocationExpressionSyntax = SyntaxFactory.InvocationExpression(
                    SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        methodInvocation.AsInvocationStatment(), 
                        SyntaxFactory.IdentifierName(methodName)));
            }

            return arguments != null ? new Invocation(invocationExpressionSyntax.WithArgumentList(arguments)) : new Invocation(invocationExpressionSyntax);
        }
    }

    public class Invocation
    {
        private readonly InvocationExpressionSyntax invocation;

        public Invocation(InvocationExpressionSyntax invocation)
        {
            this.invocation = invocation;
        }

        public ExpressionStatementSyntax AsExpressionStatement()
        {
            return SyntaxFactory.ExpressionStatement(invocation);
        }

        public InvocationExpressionSyntax AsInvocationStatment()
        {
            return invocation;
        }

    }
}
