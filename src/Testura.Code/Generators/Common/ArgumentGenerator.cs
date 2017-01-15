using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;

namespace Testura.Code.Generators.Common
{
    /// <summary>
    /// Generate code for arguments
    /// </summary>
    public static class ArgumentGenerator
    {
        /// <summary>
        /// Create arguments for a instance call
        /// </summary>
        /// <param name="arguments">Arguments</param>
        /// <returns>A argument list syntax</returns>
        public static ArgumentListSyntax Create(params IArgument[] arguments)
        {
            var convertedArguments = ConvertArgumentsToSyntaxNodesOrTokens(arguments);
            return SyntaxFactory.ArgumentList(SyntaxFactory.SeparatedList<ArgumentSyntax>(convertedArguments.ToArray()));
        }

        /// <summary>
        /// Convert argument to syntax nodes or tokens. It used for special case when Create do too much
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public static List<SyntaxNodeOrToken> ConvertArgumentsToSyntaxNodesOrTokens(params IArgument[] arguments)
        {
            if (!arguments.Any())
                return new List<SyntaxNodeOrToken>();
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

    public enum StringType
    {
        Normal,
        Path
    }
}
