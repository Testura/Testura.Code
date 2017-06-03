using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common.BinaryExpressions;

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes
{
    public class BinaryExpressionArgument : Argument
    {
        private readonly IBinaryExpression _binaryExpression;

        public BinaryExpressionArgument(IBinaryExpression binaryExpression, string namedArgument = null)
            : base(namedArgument)
        {
            if (binaryExpression == null)
            {
                throw new ArgumentNullException(nameof(binaryExpression));
            }

            _binaryExpression = binaryExpression;
        }

        protected override ArgumentSyntax CreateArgumentSyntax()
        {
            return SyntaxFactory.Argument(_binaryExpression.GetBinaryExpression());
        }
    }
}
