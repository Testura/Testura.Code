using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Reference;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Generate
{
    /// <summary>
    /// Generate code for for, while and for each loops
    /// </summary>
    public static class Control
    {
        /// <summary>
        /// Create a new for-loop with fixed start and stop
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="variableName"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static ForStatementSyntax For(int start, int end, string variableName, BlockSyntax body)
        {
            return For(new ConstantReference(start), new ConstantReference(end), variableName, body);
        }

        /// <summary>
        /// Create a mew for-loop with references for start and stop
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="variableName"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static ForStatementSyntax For(VariableReference start, VariableReference end, string variableName, BlockSyntax body)
        {
           return ForStatement(
               VariableDeclaration(
                   PredefinedType(Token(SyntaxKind.IntKeyword)), SeparatedList(new[]
                   {
                        VariableDeclarator(Identifier(variableName), null,
                            EqualsValueClause(References.GenerateReferenceChain(start)))
                   })), SeparatedList<ExpressionSyntax>(), BinaryExpression(
                       SyntaxKind.LessThanExpression,
                       IdentifierName(variableName),
                       References.GenerateReferenceChain(end)),
               SeparatedList<ExpressionSyntax>(new[]
               {PostfixUnaryExpression(SyntaxKind.PostIncrementExpression, IdentifierName(variableName))}), body);
        }
    }
}
