using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common;
using Testura.Code.Models.References;

namespace Testura.Code.Factories
{
    /// <summary>
    /// This class help us to get EqualsValueClauses for when we assign local variables
    /// </summary>
    public static class EqualsValueClauseFactory
    {
        /// <summary>
        /// Get the correct equals value clase for a specific type
        /// </summary>
        /// <param name="value">Value we want to put the variable equal as</param>
        /// <returns>The correct equals value clause</returns>
        public static EqualsValueClauseSyntax GetEqualsValueClause(object value)
        {
            if (value is int)
            {
                return
                    SyntaxFactory.EqualsValueClause(SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression,
                        SyntaxFactory.Literal(SyntaxFactory.TriviaList(), value.ToString(), (int)value, SyntaxFactory.TriviaList())));
            }

            if (value is string)
            {
                return
                    SyntaxFactory.EqualsValueClause(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression,
                        SyntaxFactory.Literal(SyntaxFactory.TriviaList(), value.ToString(), (string)value, SyntaxFactory.TriviaList())));
            }

            if (value is VariableReference)
            {
                return SyntaxFactory.EqualsValueClause(ReferenceGenerator.Create((VariableReference)value));
            }

            throw new NotSupportedException("Not a suppoerted value");
        }
    }
}
