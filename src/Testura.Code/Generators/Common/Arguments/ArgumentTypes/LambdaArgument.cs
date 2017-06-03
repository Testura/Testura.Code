using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes
{
    public class LambdaArgument : Argument
    {
        private readonly ExpressionSyntax _expressionSyntax;
        private readonly string _parameterName;
        private readonly BlockSyntax _blockSyntax;

        public LambdaArgument(ExpressionSyntax expressionSyntax, string parameterName, string namedArgument = null)
            : base(namedArgument)
        {
            if (expressionSyntax == null)
            {
                throw new ArgumentNullException(nameof(expressionSyntax));
            }

            if (parameterName == null)
            {
                throw new ArgumentNullException(nameof(parameterName));
            }

            _expressionSyntax = expressionSyntax;
            _parameterName = parameterName;
        }

        public LambdaArgument(BlockSyntax blockSyntax, string parameterName, string namedArgument = null)
            : base(namedArgument)
        {
            if (blockSyntax == null)
            {
                throw new ArgumentNullException(nameof(blockSyntax));
            }

            if (parameterName == null)
            {
                throw new ArgumentNullException(nameof(parameterName));
            }

            _blockSyntax = blockSyntax;
            _parameterName = parameterName;
        }

        protected override ArgumentSyntax CreateArgumentSyntax()
        {
            SimpleLambdaExpressionSyntax expression = null;

            if (_blockSyntax != null)
            {
                expression = SimpleLambdaExpression(Parameter(Identifier(_parameterName)), _blockSyntax);
            }
            else
            {
                expression = SimpleLambdaExpression(Parameter(Identifier(_parameterName)), _expressionSyntax);
            }

            return Argument(expression);
        }
    }
}
