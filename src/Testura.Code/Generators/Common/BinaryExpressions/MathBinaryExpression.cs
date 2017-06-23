using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Factories;
using Testura.Code.Models.References;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Generators.Common.BinaryExpressions
{
    /// <summary>
    /// Provides the functionallity to generate binary expressions with math operators
    /// </summary>
    public class MathBinaryExpression : IBinaryExpression
    {
        private readonly MathOperators _mathOperator;
        private readonly bool _useParenthes;
        private readonly ExpressionSyntax _leftExpression;
        private readonly ExpressionSyntax _rightExpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="MathBinaryExpression"/> class.
        /// </summary>
        /// <param name="leftExpression">The left expression.</param>
        /// <param name="rigthExpression">The right expression.</param>
        /// <param name="mathOperator">The math operator to generate.</param>
        /// <param name="useParenthes">If we should generate with paranthes surounding the the binary expression.</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="MathBinaryExpression"/> class.
        /// </summary>
        /// <param name="leftReference">The left reference</param>
        /// <param name="rightReference">The right reference</param>
        /// <param name="mathOperator">The math operator to generate</param>
        /// <param name="useParenthes">If we should generate with paranthes surounding the the binary expression</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="MathBinaryExpression"/> class.
        /// </summary>
        /// <param name="leftReference">The left reference.</param>
        /// <param name="rightBinaryExpression">The right other binary expression.</param>
        /// <param name="mathOperator">The math operator to generate.</param>
        /// <param name="useParenthes">If we should generate with paranthes surounding the the binary expression.</param>
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

        /// <summary>
        /// Get the generated binary expression.
        /// </summary>
        /// <returns>The generated binary expression.</returns>
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
