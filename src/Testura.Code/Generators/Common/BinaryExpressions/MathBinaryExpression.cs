using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Factories;
using Testura.Code.Models.References;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Generators.Common.BinaryExpressions
{
    public class MathBinaryExpression : IBinaryExpression
    {
        private readonly MathOperators _mathOperator;
        private readonly bool _useParenthes;
        private readonly ExpressionSyntax _leftExpression;
        private readonly ExpressionSyntax _rightExpression;

        public MathBinaryExpression(
            ExpressionSyntax leftExpression,
            ExpressionSyntax rigthExpression,
            MathOperators mathOperator,
            bool useParenthes = false)
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
            _mathOperator = mathOperator;
            _useParenthes = useParenthes;
        }

        public MathBinaryExpression(
            VariableReference leftReference,
            VariableReference rightReference,
            MathOperators mathOperator,
            bool useParenthes = false)
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
            _mathOperator = mathOperator;
            _useParenthes = useParenthes;
        }

        public MathBinaryExpression(
            VariableReference leftReference,
            MathBinaryExpression rightBinaryExpression,
            MathOperators mathOperator,
            bool useParenthes = false)
        {
            if (leftReference == null)
            {
                throw new ArgumentNullException(nameof(leftReference));
            }

            if (rightBinaryExpression == null)
            {
                throw new ArgumentNullException(nameof(rightBinaryExpression));
            }

            _leftExpression = ReferenceGenerator.Create(leftReference);
            _rightExpression = rightBinaryExpression.GetBinaryExpression();
            _mathOperator = mathOperator;
            _useParenthes = useParenthes;
        }

        public ExpressionSyntax GetBinaryExpression()
        {
            var binaryExpression = BinaryExpression(MathOperatorFactory.GetSyntaxKind(_mathOperator), _leftExpression, _rightExpression);
            if (_useParenthes)
            {
                return ParenthesizedExpression(binaryExpression);
            }

            return binaryExpression;
        }
    }
}
