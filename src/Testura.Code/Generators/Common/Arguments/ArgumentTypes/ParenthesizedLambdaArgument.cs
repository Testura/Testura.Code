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

        public ParenthesizedLambdaArgument(ExpressionSyntax expressionSyntax, IEnumerable<Parameter> parameters = null)
        {
            if (expressionSyntax == null)
            {
                throw new ArgumentNullException(nameof(expressionSyntax));
            }

            _parameters = parameters ?? new List<Parameter>();
            _expressionSyntax = expressionSyntax;
        }

        public ArgumentSyntax GetArgumentSyntax()
        {
            var expression = ParenthesizedLambdaExpression(_expressionSyntax);
            if (_parameters.Any())
            {
                expression = expression.WithParameterList(ParameterGenerator.Create(_parameters.ToArray()));
            }

            return Argument(expression);
        }
    }
}
