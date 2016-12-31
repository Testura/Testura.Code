using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generate.ArgumentTypes;

namespace Testura.Code.Generate
{
    /// <summary>
    /// Generate code for conditionals (if, else, etc) 
    /// </summary>
    public static class Conditional
    {
        /// <summary>
        /// Create a new if-conditional
        /// </summary>
        /// <param name="leftArgument"></param>
        /// <param name="rightArgument"></param>
        /// <param name="conditional"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        public static StatementSyntax If(IArgument leftArgument, IArgument rightArgument, ConditionalStatement conditional, BlockSyntax block)
        {
            return
                SyntaxFactory.IfStatement(SyntaxFactory.BinaryExpression(ConditionalToSyntaxKind(conditional),
                    leftArgument.GetArgumentSyntax().Expression, rightArgument.GetArgumentSyntax().Expression), block);
        }


        private static SyntaxKind ConditionalToSyntaxKind(ConditionalStatement conditional)
        {
            switch (conditional)
            {
                case ConditionalStatement.Equal:
                    return SyntaxKind.EqualsExpression;
                default:
                    throw new ArgumentOutOfRangeException(nameof(conditional), conditional, null);
            }
        }
    }
}
