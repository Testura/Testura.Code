using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common;
using Testura.Code.Generators.Common.Binaries;
using Testura.Code.Models.References;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Statements
{
    public class IterationStatement
    {
        /// <summary>
        /// Create the for statement syntax for a for loop with fixed start and stop
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
        /// Create the for statement syntax for a for loop with a reference for start and stop
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

        /// <summary>
        /// Create the foreach statement syntax for a foreach loop with a reference as enumerable
        /// </summary>
        /// <param name="variableName">Name of the variable</param>
        /// <param name="varialeType">The variable type</param>
        /// <param name="enumerableName">Name of the enumerable variable</param>
        /// <param name="body">Body of the foreach loop</param>
        /// <param name="useVar">If we should use the var keyword instead of the type</param>
        /// <returns>The declared foreach statement syntax</returns>
        public ForEachStatementSyntax ForEach(string variableName, Type varialeType, string enumerableName, BlockSyntax body, bool useVar = true)
        {
            return ForEach(variableName, varialeType, new VariableReference(enumerableName), body, useVar);
        }

        /// <summary>
        /// Create the foreach statement syntax for a foreach loop with a reference as enumerable
        /// </summary>
        /// <param name="variableName">Name of the variable</param>
        /// <param name="varialeType">The variable type</param>
        /// <param name="enumerableReference">Reference to the enumerable</param>
        /// <param name="body">Body of the foreach loop</param>
        /// <param name="useVar">If we should use the var keyword instead of the type</param>
        /// <returns>The declared foreach statement syntax</returns>
        public ForEachStatementSyntax ForEach(string variableName, Type varialeType, VariableReference enumerableReference, BlockSyntax body, bool useVar = true)
        {
            if (string.IsNullOrEmpty(variableName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(variableName));
            }

            if (varialeType == null)
            {
                throw new ArgumentNullException(nameof(varialeType));
            }

            if (enumerableReference == null)
            {
                throw new ArgumentNullException(nameof(enumerableReference));
            }

            return ForEachStatement(
                useVar ? IdentifierName("var") : TypeGenerator.Create(varialeType),
                Identifier(variableName),
                ReferenceGenerator.Create(enumerableReference),
                body);
        }

        /// <summary>
        /// Create the while statement syntax for a while loop with the boolean true
        /// </summary>
        /// <param name="body">Body of the while loop</param>
        /// <returns>The declared while statement syntax<</returns>
        public WhileStatementSyntax WhileTrue(BlockSyntax body)
        {
            return WhileStatement(LiteralExpression(SyntaxKind.TrueLiteralExpression), body);
        }

        /// <summary>
        /// Create the while statement syntax for a while loop with a binary expression
        /// </summary>
        /// <param name="binaryExpression">The binary expression</param>
        /// <param name="body">Body of the while loop</param>
        /// <returns>The declared while statement</returns>
        public WhileStatementSyntax While(IBinaryExpression binaryExpression, BlockSyntax body)
        {
            if (binaryExpression == null)
            {
                throw new ArgumentNullException(nameof(binaryExpression));
            }

            return WhileStatement(binaryExpression.GetBinaryExpression(), body);
        }
    }
}
