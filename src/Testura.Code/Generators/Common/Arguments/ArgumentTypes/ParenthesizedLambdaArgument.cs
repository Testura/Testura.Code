using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Models;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes
{
    public class ParenthesizedLambdaArgument : IArgument
    {
        private readonly IEnumerable<Parameter> _parameters;
        private readonly ExpressionSyntax _expressionSyntax;
        private readonly BlockSyntax _blockSyntax;

        public ParenthesizedLambdaArgument(ExpressionSyntax expressionSyntax, IEnumerable<Parameter> parameters = null)
        {
            if (expressionSyntax == null)
            {
                throw new ArgumentNullException(nameof(expressionSyntax));
            }

            _parameters = parameters ?? new List<Parameter>();
            _expressionSyntax = expressionSyntax;
        }

        public ParenthesizedLambdaArgument(BlockSyntax blockSyntax, IEnumerable<Parameter> parameters = null)
        {
            if (blockSyntax == null)
            {
                throw new ArgumentNullException(nameof(blockSyntax));
            }

            _parameters = parameters ?? new List<Parameter>();
            _blockSyntax = blockSyntax;
        }

        public ArgumentSyntax GetArgumentSyntax()
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
