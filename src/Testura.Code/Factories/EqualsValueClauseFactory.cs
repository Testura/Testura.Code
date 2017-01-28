using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common;
using Testura.Code.Models.References;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Factories
{
    internal static class EqualsValueClauseFactory
    {
        /// <summary>
        /// Get the correct equals value clause for a specific type
        /// </summary>
        /// <param name="value">Value we want to put the variable equal as</param>
        /// <returns>The correct equals value clause</returns>
        internal static EqualsValueClauseSyntax GetEqualsValueClause(object value)
        {
            if (value is int)
            {
                return EqualsValueClause(
                        LiteralExpression(
                                SyntaxKind.NumericLiteralExpression,
                                Literal(TriviaList(), value.ToString(), (int)value, TriviaList())));
            }

            if (value is string)
            {
                return EqualsValueClause(
                        LiteralExpression(
                            SyntaxKind.StringLiteralExpression,
                            Literal(TriviaList(), value.ToString(), (string)value, TriviaList())));
            }

            if (value is VariableReference)
            {
                return EqualsValueClause(ReferenceGenerator.Create((VariableReference)value));
            }

            throw new NotSupportedException("Not a supported value");
        }
    }
}
