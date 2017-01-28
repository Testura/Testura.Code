using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common.Binaries;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Generators.Common.BinaryExpressions
{
    public class AndBinaryExpression : IBinaryExpression
    {
        private readonly ExpressionSyntax _leftExpression;
        private readonly ExpressionSyntax _rightExpression;

        public AndBinaryExpression(ExpressionSyntax leftExpression, ExpressionSyntax rightExpression)
        {
            if (leftExpression == null)
            {
                throw new ArgumentNullException(nameof(leftExpression));
            }

            if (rightExpression == null)
            {
                throw new ArgumentNullException(nameof(rightExpression));
            }

            _leftExpression = leftExpression;
            _rightExpression = rightExpression;
        }

        public AndBinaryExpression(IBinaryExpression leftExpression, IBinaryExpression rightExpression)
            : this(leftExpression?.GetBinaryExpression(), rightExpression?.GetBinaryExpression())
        {
        }

        public ExpressionSyntax GetBinaryExpression()
        {
            return BinaryExpression(SyntaxKind.LogicalAndExpression, _leftExpression, _rightExpression);
        }
    }
}
