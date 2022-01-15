using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;

namespace Testura.Code.Generators.Common;

/// <summary>
/// Provides functionality to generate argument lists.
/// </summary>
public static class ArgumentGenerator
{
    /// <summary>
    /// Create the syntax for a list of arguments to a method/constructor.
    /// </summary>
    /// <param name="arguments">Arguments to create.</param>
    /// <returns>The argument list syntax for arguments.</returns>
    public static ArgumentListSyntax Create(params IArgument[] arguments)
    {
        var convertedArguments = ConvertArgumentsToSyntaxNodesOrTokens(arguments);
        return SyntaxFactory.ArgumentList(SyntaxFactory.SeparatedList<ArgumentSyntax>(convertedArguments.ToArray()));
    }

    /// <summary>
    /// Convert arguments to syntax nodes or tokens.
    /// </summary>
    /// <param name="arguments">Arguments to convert.</param>
    /// <returns>A list with SyntaxNodeOrToken.</returns>
    internal static List<SyntaxNodeOrToken> ConvertArgumentsToSyntaxNodesOrTokens(params IArgument[] arguments)
    {
        if (!arguments.Any())
        {
            return new List<SyntaxNodeOrToken>();
        }

        var list = new List<SyntaxNodeOrToken>();
        foreach (var argument in arguments)
        {
            list.Add(argument != null ? argument.GetArgumentSyntax() : new ValueArgument("null").GetArgumentSyntax());
            list.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));
        }

        list.RemoveAt(list.Count - 1);
        return list;
    }
}
