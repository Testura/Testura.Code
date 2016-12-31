using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Reference;

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
        /// <param name="block"></param>
        /// <returns></returns>
        public static ForStatementSyntax For(int start, int end, string variableName, BlockSyntax block)
        {
            return For(new ConstantReference(start), new ConstantReference(end), variableName, block);
        }

        /// <summary>
        /// Create a mew for-loop with references for start and stop
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="variableName"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        public static ForStatementSyntax For(VariableReference start, VariableReference end, string variableName, BlockSyntax block)
        {
           return SyntaxFactory.ForStatement(
               SyntaxFactory.VariableDeclaration(
                   SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)), SyntaxFactory.SeparatedList(new[]
                   {
                        SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier(variableName), null,
                            SyntaxFactory.EqualsValueClause(SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(0))))
                   })), SyntaxFactory.SeparatedList<ExpressionSyntax>(), SyntaxFactory.BinaryExpression(
                       SyntaxKind.LessThanExpression,
                       References.GenerateReferenceChain(start),
                       References.GenerateReferenceChain(end)),
               SyntaxFactory.SeparatedList<ExpressionSyntax>(new[]
               {SyntaxFactory.PostfixUnaryExpression(SyntaxKind.PostIncrementExpression, SyntaxFactory.IdentifierName(variableName))}), block);
        }
    }
}
