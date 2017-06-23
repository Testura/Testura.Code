using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Models;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
#pragma warning disable 1591

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes
{
    /// <summary>
    /// Provides the functionallity to generate parenthesized lambda arguments. Example of generated code: <c>(() => Do()</c>
    /// </summary>
    public class ParenthesizedLambdaArgument : Argument
    {
        private readonly IEnumerable<Parameter> _parameters;
        private readonly ExpressionSyntax _expressionSyntax;
        private readonly BlockSyntax _blockSyntax;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParenthesizedLambdaArgument"/> class.
        /// </summary>
        /// <param name="expressionSyntax">The expression to execute inside the lambda.</param>
        /// <param name="parameters">Parameters in the lambda.</param>
        /// <param name="namedArgument">Specificy the argument for a partical parameter.</param>
        public ParenthesizedLambdaArgument(ExpressionSyntax expressionSyntax, IEnumerable<Parameter> parameters = null, string namedArgument = null)
            : base(namedArgument)
        {
            if (expressionSyntax == null)
            {
                throw new ArgumentNullException(nameof(expressionSyntax));
            }

            _parameters = parameters ?? new List<Parameter>();
            _expressionSyntax = expressionSyntax;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParenthesizedLambdaArgument"/> class.
        /// </summary>
        /// <param name="blockSyntax">The block/body inside the lambda.</param>
        /// <param name="parameters">Parameters in the lambda.</param>
        /// <param name="namedArgument">Specificy the argument for a partical parameter.</param>
        public ParenthesizedLambdaArgument(BlockSyntax blockSyntax, IEnumerable<Parameter> parameters = null, string namedArgument = null)
            : base(namedArgument)
        {
            if (blockSyntax == null)
            {
                throw new ArgumentNullException(nameof(blockSyntax));
            }

            _parameters = parameters ?? new List<Parameter>();
            _blockSyntax = blockSyntax;
        }

        protected override ArgumentSyntax CreateArgumentSyntax()
        {
            ParenthesizedLambdaExpressionSyntax expression = null;

            if (_blockSyntax != null)
            {
                expression = ParenthesizedLambdaExpression(_blockSyntax);
            }
            else
            {
                expression = ParenthesizedLambdaExpression(_expressionSyntax);
            }

            if (_parameters.Any())
            {
                expression = expression.WithParameterList(ParameterGenerator.ConvertParameterSyntaxToList(_parameters.Select(p => Parameter(Identifier(p.Name))).ToArray()));
            }

            return Argument(expression);
        }
    }
}
