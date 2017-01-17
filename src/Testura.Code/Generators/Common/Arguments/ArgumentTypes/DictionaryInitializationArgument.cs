using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes
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
                syntaxNodeOrTokens.Add(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        ImplicitElementAccess()
                            .WithArgumentList(
                                BracketedArgumentList(
                                    SingletonSeparatedList<ArgumentSyntax>(
                                        Argument(
                                            IdentifierName(
                                                typeof(T) == typeof(string) ? $"\"{dictionaryValue.Key}\"" : dictionaryValue.Key.ToString()))))),
                        dictionaryValue.Value.GetArgumentSyntax().Expression));
                syntaxNodeOrTokens.Add(Token(SyntaxKind.CommaToken));
            }

            if (syntaxNodeOrTokens.Any())
            {
                syntaxNodeOrTokens.RemoveAt(syntaxNodeOrTokens.Count - 1);
            }

            return Argument(ObjectCreationExpression(GenericName(Identifier("Dictionary"))
                    .WithTypeArgumentList(TypeArgumentList(
                        SeparatedList<TypeSyntax>(
                            new SyntaxNodeOrToken[]
                            {
                                IdentifierName(typeof(T).ToString()),
                                Token(SyntaxKind.CommaToken),
                                IdentifierName(typeof(T2).ToString())
                            })))).WithInitializer(InitializerExpression(
                SyntaxKind.ObjectInitializerExpression, SeparatedList<ExpressionSyntax>(syntaxNodeOrTokens.ToArray()))));
        }
    }
}