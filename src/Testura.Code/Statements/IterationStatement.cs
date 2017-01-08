using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Helper.References;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Statements
{
    public class IterationStatement
    {
        /// <summary>
        /// Create a new for-loop with fixed start and stop
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="variableName"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public ForStatementSyntax For(int start, int end, string variableName, BlockSyntax body)
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
        public ForStatementSyntax For(VariableReference start, VariableReference end, string variableName, BlockSyntax body)
        {
            return ForStatement(
                VariableDeclaration(
                    PredefinedType(Token(SyntaxKind.IntKeyword)), SeparatedList(new[]
                    {
                        VariableDeclarator(Identifier(variableName), null,
                            EqualsValueClause(Reference.Create(start)))
                    })), SeparatedList<ExpressionSyntax>(), BinaryExpression(
                        SyntaxKind.LessThanExpression,
                        IdentifierName(variableName),
                        Reference.Create(end)),
                SeparatedList<ExpressionSyntax>(new[]
                {PostfixUnaryExpression(SyntaxKind.PostIncrementExpression, IdentifierName(variableName))}), body);
        }
    }
}
