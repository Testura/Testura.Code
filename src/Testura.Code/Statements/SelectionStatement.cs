using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Statements
{
    public class SelectionStatement
    {
        /// <summary>
        /// Create the statement syntax for a if-conditional
        /// </summary>
        /// <param name="leftArgument">The left argument of the if-statement</param>
        /// <param name="rightArgument">The right argument of the if-statement</param>
        /// <param name="conditional">The conditional</param>
        /// <param name="block">The if block</param>
        /// <returns>The declared statement syntax</returns>
        public StatementSyntax If(IArgument leftArgument, IArgument rightArgument, ConditionalStatements conditional, BlockSyntax block)
        {
            return
                IfStatement(BinaryExpression(ConditionalToSyntaxKind(conditional),
                    leftArgument.GetArgumentSyntax().Expression, rightArgument.GetArgumentSyntax().Expression), block);
        }

        private SyntaxKind ConditionalToSyntaxKind(ConditionalStatements conditional)
        {
            switch (conditional)
            {
                case ConditionalStatements.Equal:
                    return SyntaxKind.EqualsExpression;
                case ConditionalStatements.NotEqual:
                    return SyntaxKind.NotEqualsExpression;
                case ConditionalStatements.GreaterThan:
                    return SyntaxKind.GreaterThanExpression;
                case ConditionalStatements.GreaterThanOrEqual:
                    return SyntaxKind.GreaterThanOrEqualExpression;
                case ConditionalStatements.LessThan:
                    return SyntaxKind.LessThanExpression;
                case ConditionalStatements.LessThanOrEqual:
                    return SyntaxKind.LessThanOrEqualExpression;
                default:
                    throw new ArgumentOutOfRangeException(nameof(conditional), conditional, null);
            }
        }
    }
}
