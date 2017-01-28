using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Factories;
using Testura.Code.Models.References;

namespace Testura.Code.Generators.Common.BinaryExpressions
{
    public class ConditionalBinaryExpression : IBinaryExpression
    {
        private readonly ExpressionSyntax _leftExpression;
        private readonly ExpressionSyntax _rightExpression;
        private readonly ConditionalStatements _conditional;

        public ConditionalBinaryExpression(
            ExpressionSyntax leftExpression,
            ExpressionSyntax rigthExpression,
            ConditionalStatements conditional)
        {
            if (leftExpression == null)
            {
                throw new ArgumentNullException(nameof(leftExpression));
            }

            if (rigthExpression == null)
            {
                throw new ArgumentNullException(nameof(rigthExpression));
            }

            _leftExpression = leftExpression;
            _rightExpression = rigthExpression;
            _conditional = conditional;
        }

        public ConditionalBinaryExpression(
            VariableReference leftReference,
            VariableReference rightReference,
            ConditionalStatements conditional)
        {
            if (leftReference == null)
            {
                throw new ArgumentNullException(nameof(leftReference));
            }

            if (rightReference == null)
            {
                throw new ArgumentNullException(nameof(rightReference));
            }

            _leftExpression = ReferenceGenerator.Create(leftReference);
            _rightExpression = ReferenceGenerator.Create(rightReference);
            _conditional = conditional;
        }

        public ExpressionSyntax GetBinaryExpression()
        {
            return SyntaxFactory.BinaryExpression(ConditionalFactory.GetSyntaxKind(_conditional), _leftExpression, _rightExpression);
        }
    }
}
