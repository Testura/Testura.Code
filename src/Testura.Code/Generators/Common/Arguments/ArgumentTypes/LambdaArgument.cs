using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
#pragma warning disable 1591

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes
{
    /// <summary>
    /// Provides the functionallity to generate lambda arguments. Example of generated code: <c>(n=>MyMethod())</c>
    /// </summary>
    public class LambdaArgument : Argument
    {
        private readonly ExpressionSyntax _expressionSyntax;
        private readonly string _parameterName;
        private readonly BlockSyntax _blockSyntax;

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaArgument"/> class.
        /// </summary>
        /// <param name="expressionSyntax">Generated expression inside the lambda.</param>
        /// <param name="parameterName">Paramters in the lambda.</param>
        /// <param name="namedArgument">Specificy the argument for a partical parameter.</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaArgument"/> class.
        /// </summary>
        /// <param name="blockSyntax">Generated block/body inside the lambda.</param>
        /// <param name="parameterName">Paramters in the lambda.</param>
        /// <param name="namedArgument">Specificy the argument for a partical parameter.</param>
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
