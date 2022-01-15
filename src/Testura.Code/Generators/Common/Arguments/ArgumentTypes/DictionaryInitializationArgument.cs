using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
#pragma warning disable 1591

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes;

/// <summary>
/// Provides the functionality to generate dictionary initialization argument. Example of generated code: <c>new Dictionary&lt;int,int>{ [1] = 2}</c>
/// </summary>
/// <typeparam name="TKey">The dictionary key type</typeparam>
/// <typeparam name="TValue">The dictionary value type</typeparam>
public class DictionaryInitializationArgument<TKey, TValue> : Argument
    where TKey : notnull
{
    private readonly Dictionary<TKey, IArgument> _dictionary;

    /// <summary>
    /// Initializes a new instance of the <see cref="DictionaryInitializationArgument{TKey, TValue}"/> class.
    /// </summary>
    /// <param name="dictionary">The dictionary to generate.</param>
    /// <param name="namedArgument">Specify the argument for a particular parameter.</param>
    public DictionaryInitializationArgument(IDictionary<TKey, IArgument> dictionary, string namedArgument = null)
        : base(namedArgument)
    {
        _dictionary = new Dictionary<TKey, IArgument>(dictionary);
    }

    protected override ArgumentSyntax CreateArgumentSyntax()
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
                                            typeof(TKey) == typeof(string) ? $"\"{dictionaryValue.Key}\"" : dictionaryValue.Key.ToString()))))),
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
                        TypeGenerator.Create(typeof(TKey)),
                        Token(SyntaxKind.CommaToken),
                        TypeGenerator.Create(typeof(TValue))
                    })))).WithInitializer(InitializerExpression(
            SyntaxKind.ObjectInitializerExpression, SeparatedList<ExpressionSyntax>(syntaxNodeOrTokens.ToArray()))));
    }
}
