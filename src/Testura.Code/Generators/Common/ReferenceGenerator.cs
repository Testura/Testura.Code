using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Models.References;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Generators.Common;

/// <summary>
/// Provides the functionality to generate references and reference chains. Example of generated code: "<c>test</c>", "<c>test.MyMethod()</c>".
/// </summary>
public static class ReferenceGenerator
{
    /// <summary>
    /// Create the expression syntax for a variable, method or a chain of member/method(s).
    /// </summary>
    /// <param name="reference">The start reference.</param>
    /// <returns>The declared expression syntax.</returns>
    public static ExpressionSyntax Create(VariableReference reference)
    {
        if (reference == null)
        {
            throw new ArgumentNullException(nameof(reference));
        }

        ExpressionSyntax baseExpression;

        if (reference is MethodReference methodReference)
        {
            if (methodReference.GenericTypes.Any())
            {
                baseExpression = InvocationExpression(GenericGenerator.Create(methodReference.Name, methodReference.GenericTypes.ToArray()))
                    .WithArgumentList(ArgumentGenerator.Create(methodReference.Arguments.ToArray()));
            }
            else
            {
                baseExpression = InvocationExpression(IdentifierName(methodReference.Name))
                    .WithArgumentList(ArgumentGenerator.Create(methodReference.Arguments.ToArray()));
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
    /// Create the expression syntax for a chain of member/method(s).
    /// </summary>
    /// <param name="expression">The expression to build upon.</param>
    /// <param name="reference">Next member reference in chain.</param>
    /// <returns>The declared expression syntax.</returns>
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
        while (true)
        {
            switch (reference)
            {
                case null:
                    return expressionSyntax;

                case MethodReference memberReference:
                {
                    if (memberReference.GenericTypes.Any())
                    {
                        expressionSyntax = InvocationExpression(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expressionSyntax, GenericGenerator.Create(memberReference.Name, memberReference.GenericTypes.ToArray())))
                            .WithArgumentList(ArgumentGenerator.Create(memberReference.Arguments.ToArray()));
                    }
                    else
                    {
                        expressionSyntax = InvocationExpression(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expressionSyntax, IdentifierName(memberReference.Name)))
                            .WithArgumentList(ArgumentGenerator.Create(memberReference.Arguments.ToArray()));
                    }

                    break;
                }

                case MemberReference:
                    expressionSyntax = MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expressionSyntax, IdentifierName(reference.Name));
                    break;
            }

            reference = reference.Member;
        }
    }
}
