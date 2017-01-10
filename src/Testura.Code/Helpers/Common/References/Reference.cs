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
            var baseExpression = SyntaxFactory.IdentifierName(reference.Name);
            if (reference.Member == null)
            {
                return baseExpression;
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
                IList<IArgument> arguments = new List<IArgument>();
                if (current is MethodReference)
                {
                    arguments = ((MethodReference)current).Arguments;
                }

                expressionSyntax = SyntaxFactory.InvocationExpression(
                       SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expressionSyntax,
                           SyntaxFactory.IdentifierName(current.Name))).WithArgumentList(Arguments.Argument.Create(arguments.ToArray()));
            }
            else if (current is MemberReference)
            {
                expressionSyntax = SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expressionSyntax, SyntaxFactory.IdentifierName(current.Name));
            }

            return Generate(expressionSyntax, current.Member);
        }

    }
}
