using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Reference;

namespace Testura.Code.Generate
{
    public class References
    {
        /// <summary>
        /// Generate the code for a variable reference chain, for example: 
        /// 
        /// myVariable.SomeMethod().MyProperty
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        public static ExpressionSyntax GenerateReferenceChain(VariableReference reference)
        {
            var i = SyntaxFactory.IdentifierName(reference.Name);
            if (reference.Member == null)
                return i;
            var baseExpression = SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName(reference.Name), SyntaxFactory.IdentifierName(reference.Member.Name));
            return Generate(baseExpression, reference.Member.Member);
        }

        /// <summary>
        /// Generate the code for a member chain. This method is used if you already have a variable, member or method invocation and want to 
        /// extend it with more references calls. 
        /// </summary>
        /// <param name="invocation"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public static ExpressionSyntax GenerateReferenceChain(ExpressionSyntax invocation, MemberReference reference)
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
            if (current.ReferenceType == MemberReferenceTypes.Field || current.ReferenceType == MemberReferenceTypes.Property)
            {
                expressionSyntax = SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expressionSyntax, SyntaxFactory.IdentifierName(current.Name));
            }
            else if (current.ReferenceType == MemberReferenceTypes.Method)
            {
                expressionSyntax = SyntaxFactory.InvocationExpression(SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expressionSyntax, SyntaxFactory.IdentifierName(current.Name)));
            }
            return Generate(expressionSyntax, current.Member);
        }

    }
}
