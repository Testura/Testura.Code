using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Helper;
using Testura.Code.Helper.References;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Statements
{
    public class ExpressionStatement
    {
        /// <summary>
        /// Create code to invoke a method on class
        /// </summary>
        /// <param name="variableName">Variable to invoke</param>
        /// <param name="methodName">The method we want to call</param>
        /// <param name="arguments">Arguments in the method</param>
        /// <param name="genericTypes">Optional list of types if the method is generic</param>
        /// <returns>A statement syntax</returns>
        public Invocation Invoke(string variableName, string methodName, ArgumentListSyntax arguments, params Type[] genericTypes)
        {
            InvocationExpressionSyntax invocationExpressionSyntax;
            if (genericTypes != null && genericTypes.Any())
            {
                invocationExpressionSyntax = InvocationExpression(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName(variableName),
                        Generic.Create(methodName, genericTypes)));
            }
            else
            {
                invocationExpressionSyntax = InvocationExpression(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName(variableName),
                        IdentifierName(methodName)));
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
        public Invocation Invoke(Invocation methodInvocation, string methodName, ArgumentListSyntax arguments, params Type[] genericTypes)
        {
            InvocationExpressionSyntax invocationExpressionSyntax;
            if (genericTypes != null && genericTypes.Any())
            {
                invocationExpressionSyntax = InvocationExpression(
                    MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            methodInvocation.AsInvocationStatment(),
                            Generic.Create(methodName, genericTypes)));
            }
            else
            {
                invocationExpressionSyntax = InvocationExpression(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        methodInvocation.AsInvocationStatment(),
                        IdentifierName(methodName)));
            }

            return arguments != null ? new Invocation(invocationExpressionSyntax.WithArgumentList(arguments)) : new Invocation(invocationExpressionSyntax);
        }

        public Invocation Invoke(VariableReference reference)
        {
            VariableReference child = reference.Member;
            while (child.Member != null)
            {
                child = child.Member;
            }

            if (!(child is MethodReference))
            {
                throw new ArgumentException(nameof(reference), "Must be a method reference");
            }

            return new Invocation((InvocationExpressionSyntax)Reference.Create(reference));
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
