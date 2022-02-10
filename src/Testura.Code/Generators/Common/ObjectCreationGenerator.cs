using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Generators.Common;

public static class ObjectCreationGenerator
{
    /// <summary>
    /// Create the expression syntax for an object creation (for example <c>new MyClass()</c>)
    /// </summary>
    /// <param name="type">Type of the object.</param>
    /// <param name="genericTypes">Generics of the type.</param>
    /// <param name="initialization">Expressions used at initialization</param>
    /// <returns>An object creation expression.</returns>
    public static ExpressionSyntax Create(Type type, IEnumerable<Type> genericTypes = null, IEnumerable<ExpressionSyntax> initialization = null)
    {
        return Create(type, new List<IArgument>(), genericTypes, initialization);
    }

    /// <summary>
    /// Create the expression syntax for an object creation (for example <c>new MyClass(1, "hello")</c>)
    /// </summary>
    /// <param name="type">Type of the object.</param>
    /// <param name="arguments">Arguments to use when creating the instance of the object.</param>
    /// <param name="genericTypes">Generics of the type.</param>
    /// <param name="initialization">Expressions used at initialization</param>
    /// <returns>An object creation expression.</returns>
    public static ExpressionSyntax Create(Type type, IEnumerable<IArgument> arguments, IEnumerable<Type>? genericTypes = null, IEnumerable<ExpressionSyntax> initialization = null)
    {
        ObjectCreationExpressionSyntax objectCreationExpressionSyntax;

        if (genericTypes != null && genericTypes.Any())
        {
            objectCreationExpressionSyntax = ObjectCreationExpression(GenericGenerator.Create(type.Name, genericTypes.ToArray()));
        }
        else
        {
            objectCreationExpressionSyntax = ObjectCreationExpression(TypeGenerator.Create(type));
        }

        if ((arguments != null && arguments.Any()) || (initialization == null || !initialization.Any()))
        {
            objectCreationExpressionSyntax = objectCreationExpressionSyntax.WithArgumentList(ArgumentGenerator.Create(arguments.ToArray()));
        }

        if(initialization != null && initialization.Any())
        {
            var syntaxNodeOrToken = new List<SyntaxNodeOrToken>();

            foreach (var expressionSyntax in initialization)
            {
                syntaxNodeOrToken.Add(expressionSyntax);
                syntaxNodeOrToken.Add(Token(SyntaxKind.CommaToken));
            }

            syntaxNodeOrToken.RemoveAt(syntaxNodeOrToken.Count - 1);

            objectCreationExpressionSyntax = objectCreationExpressionSyntax.WithInitializer(
                InitializerExpression(SyntaxKind.ObjectInitializerExpression, SeparatedList<ExpressionSyntax>(syntaxNodeOrToken)));
        }

        return objectCreationExpressionSyntax;
    }
}
