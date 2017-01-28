using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common.BinaryExpressions;

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes
{
    public class BinaryExpressionArgument : IArgument
    {
        private readonly IBinaryExpression _binaryExpression;

        public BinaryExpressionArgument(IBinaryExpression binaryExpression)
        {
            if (binaryExpression == null)
            {
                throw new ArgumentNullException(nameof(binaryExpression));
            }

            _binaryExpression = binaryExpression;
        }

        public ArgumentSyntax GetArgumentSyntax()
        {
            return SyntaxFactory.Argument(_binaryExpression.GetBinaryExpression());
        }
    }
}
