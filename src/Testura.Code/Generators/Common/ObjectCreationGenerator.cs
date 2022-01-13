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
    /// <returns>An object creation expression.</returns>
    public static ExpressionSyntax Create(Type type)
    {
        return ObjectCreationExpression(TypeGenerator.Create(type)).WithArgumentList(ArgumentList());
    }

    /// <summary>
    /// Create the expression syntax for an object creation (for example <c>new MyClass()</c>)
    /// </summary>
    /// <param name="type">Type of the object.</param>
    /// <returns>An object creation expression.</returns>
    public static ExpressionSyntax Create(string type)
    {
        return ObjectCreationExpression(IdentifierName(type)).WithArgumentList(ArgumentList());
    }

    /// <summary>
    /// Create the expression syntax for an object creation (for example <c>new MyClass(1, "hello")</c>)
    /// </summary>
    /// <param name="type">Type of the object.</param>
    /// <param name="arguments">Arguments to use when creating the instance of the object.</param>
    /// <returns>An object creation expression.</returns>
    public static ExpressionSyntax Create(string type, IEnumerable<IArgument> arguments)
    {
        return ObjectCreationExpression(
            IdentifierName(type)).WithArgumentList(ArgumentGenerator.Create(arguments.ToArray()));
    }
}
