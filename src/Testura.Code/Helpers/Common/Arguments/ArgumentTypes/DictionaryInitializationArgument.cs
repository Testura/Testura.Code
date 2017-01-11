using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Helpers.Common.Arguments.ArgumentTypes
{
    public class DictionaryInitializationArgument<T, T2> : IArgument
    {
        private readonly Dictionary<T, IArgument> _dictionary;

        public DictionaryInitializationArgument(IDictionary<T, IArgument> dictionary)
        {
            _dictionary = new Dictionary<T, IArgument>(dictionary);
        }

        public ArgumentSyntax GetArgumentSyntax()
        {
            var syntaxNodeOrTokens = new List<SyntaxNodeOrToken>();
            foreach (var dictionaryValue in _dictionary)
            {
                syntaxNodeOrTokens.Add(SyntaxFactory.AssignmentExpression(SyntaxKind.SimpleAssignmentExpression,
                    SyntaxFactory.ImplicitElementAccess()
                        .WithArgumentList(
                            SyntaxFactory.BracketedArgumentList(
                                SyntaxFactory.SingletonSeparatedList<ArgumentSyntax>(
                                    SyntaxFactory.Argument(
                                        SyntaxFactory.IdentifierName(typeof(T) == typeof(string)
                                            ? $"\"{dictionaryValue.Key}\""
                                            : dictionaryValue.Key.ToString()))))),
                    dictionaryValue.Value.GetArgumentSyntax().Expression));
                syntaxNodeOrTokens.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));
            }
            if (syntaxNodeOrTokens.Any())
                syntaxNodeOrTokens.RemoveAt(syntaxNodeOrTokens.Count - 1);
            return SyntaxFactory.Argument(SyntaxFactory.ObjectCreationExpression(SyntaxFactory.GenericName(
                    SyntaxFactory.Identifier("Dictionary"))
                .WithTypeArgumentList(
                    SyntaxFactory.TypeArgumentList(
                        SyntaxFactory.SeparatedList<TypeSyntax>(
                            new SyntaxNodeOrToken[]
                            {
                                SyntaxFactory.IdentifierName(typeof(T).ToString()),
                                SyntaxFactory.Token(SyntaxKind.CommaToken),
                                SyntaxFactory.IdentifierName(typeof(T2).ToString())
                            })))).WithInitializer(SyntaxFactory.InitializerExpression(
                SyntaxKind.ObjectInitializerExpression, SyntaxFactory.SeparatedList<ExpressionSyntax>(syntaxNodeOrTokens.ToArray()))));
        }
    }
}