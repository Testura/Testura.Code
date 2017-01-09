using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Helpers
{
    /// <summary>
    /// Generate code for method bodies.
    /// </summary>
    public static class Body
    {
        /// <summary>
        /// Create a method body with multiple statement lines
        /// </summary>
        /// <param name="statements"></param>
        /// <returns></returns>
        public static BlockSyntax Create(params StatementSyntax[] statements)
        {
            return SyntaxFactory.Block(SyntaxFactory.List<StatementSyntax>(statements));
        }

        ///// <summary>
        ///// Create a method body with a single statement line
        ///// </summary>
        ///// <param name="statement"></param>
        ///// <returns></returns>
        //public static BlockSyntax Create(StatementSyntax statement)
        //{
        //    return SyntaxFactory.Block(SyntaxFactory.SingletonList<StatementSyntax>(statement));
        //}

    }
}
