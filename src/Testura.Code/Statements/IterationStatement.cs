using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common;
using Testura.Code.Models.References;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Statements
{
    public class IterationStatement
    {
        /// <summary>
        /// Create the for-statement syntax for a for-loop with fixed start and stop
        /// </summary>
        /// <param name="start">Start number</param>
        /// <param name="end">End number</param>
        /// <param name="variableName">Variable name in loop</param>
        /// <param name="body">Body inside loop</param>
        /// <returns>The declared for statement syntax</returns>
        public ForStatementSyntax For(int start, int end, string variableName, BlockSyntax body)
        {
            if (string.IsNullOrEmpty(variableName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(variableName));
            }

            return For(new ConstantReference(start), new ConstantReference(end), variableName, body);
        }

        /// <summary>
        /// Create the for-statement syntax for a for-loop with a reference for start and stop
        /// </summary>
        /// <param name="start">Reference for start</param>
        /// <param name="end">Reference for end</param>
        /// <param name="variableName">Variable name in loop</param>
        /// <param name="body">Body inside loop</param>
        /// <returns>The declared for statement syntax</returns>
        public ForStatementSyntax For(VariableReference start, VariableReference end, string variableName, BlockSyntax body)
        {
            if (start == null)
            {
                throw new ArgumentNullException(nameof(start));
            }

            if (end == null)
            {
                throw new ArgumentNullException(nameof(end));
            }

            if (string.IsNullOrEmpty(variableName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(variableName));
            }

            return ForStatement(
                VariableDeclaration(
                    PredefinedType(Token(SyntaxKind.IntKeyword)), SeparatedList(new[]
                    {
                        VariableDeclarator(
                            Identifier(variableName),
                            null,
                            EqualsValueClause(ReferenceGenerator.Create(start)))
                    })),
                SeparatedList<ExpressionSyntax>(),
                BinaryExpression(
                        SyntaxKind.LessThanExpression,
                        IdentifierName(variableName),
                        ReferenceGenerator.Create(end)),
                SeparatedList<ExpressionSyntax>(new[]
                {
                    PostfixUnaryExpression(SyntaxKind.PostIncrementExpression, IdentifierName(variableName))
                }), body);
        }
    }
}
