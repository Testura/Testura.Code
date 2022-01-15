using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Generators.Common;

/// <summary>
/// Provides the functionality to generate body/block.
/// </summary>
public static class BodyGenerator
{
    /// <summary>
    /// Create the syntax for a method/loop body with multiple statement lines.
    /// </summary>
    /// <param name="statements">Statements in the body.</param>
    /// <returns>The declared block syntax.</returns>
    public static BlockSyntax Create(params StatementSyntax[] statements)
    {
        return SyntaxFactory.Block(SyntaxFactory.List(statements));
    }
}
