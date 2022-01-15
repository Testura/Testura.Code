using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable 1591

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes;

/// <summary>
/// Provides the functionality to generate creation of an object using it's initializer. Example of generated code: <c>(new MyClass { Hello = 10 })</c>
/// </summary>
public class ObjectInitializationArgument : Argument
{
    private readonly Type _type;
    private readonly Dictionary<string, IArgument> _dictionary;

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectInitializationArgument"/> class.
    /// </summary>
    /// <param name="type">Type of the object</param>
    /// <param name="dictionary">Properties used for object initialization</param>
    public ObjectInitializationArgument(Type type, IDictionary<string, IArgument> dictionary)
    {
        _type = type;
        _dictionary = new Dictionary<string, IArgument>(dictionary);
    }

    protected override ArgumentSyntax CreateArgumentSyntax()
    {
        var syntaxNodeOrTokens = new List<SyntaxNodeOrToken>();
        foreach (var (key, value) in _dictionary)
        {
            syntaxNodeOrTokens.Add(
                AssignmentExpression(
                    SyntaxKind.SimpleAssignmentExpression,
                    IdentifierName(key),
                    value.GetArgumentSyntax().Expression));

            syntaxNodeOrTokens.Add(Token(SyntaxKind.CommaToken));
        }

        if (syntaxNodeOrTokens.Any())
        {
            syntaxNodeOrTokens.RemoveAt(syntaxNodeOrTokens.Count - 1);
        }

        return Argument(
            ObjectCreationExpression(TypeGenerator.Create(_type)).WithInitializer(
                InitializerExpression(
                    SyntaxKind.ObjectInitializerExpression,
                    SeparatedList<ExpressionSyntax>(syntaxNodeOrTokens.ToArray()))));
    }
}
