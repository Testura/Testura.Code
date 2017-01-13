using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Generators.Common
{
    /// <summary>
    /// Generate code for method bodies.
    /// </summary>
    public static class BodyGenerator
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
    }
}
