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
    /// <returns>An object creation expression.</returns>
    public static ExpressionSyntax Create(Type type, IEnumerable<Type> genericTypes = null)
    {
        return Create(type, new List<IArgument>(), genericTypes);
    }

    /// <summary>
    /// Create the expression syntax for an object creation (for example <c>new MyClass(1, "hello")</c>)
    /// </summary>
    /// <param name="type">Type of the object.</param>
    /// <param name="arguments">Arguments to use when creating the instance of the object.</param>
    /// <param name="genericTypes">Generics of the type.</param>
    /// <returns>An object creation expression.</returns>
    public static ExpressionSyntax Create(Type type, IEnumerable<IArgument> arguments, IEnumerable<Type>? genericTypes = null)
    {
        if (genericTypes != null && genericTypes.Any())
        {
            return ObjectCreationExpression(GenericGenerator.Create(type.Name, genericTypes.ToArray())).WithArgumentList(ArgumentGenerator.Create(arguments.ToArray()));
        }

        return ObjectCreationExpression(TypeGenerator.Create(type)).WithArgumentList(ArgumentGenerator.Create(arguments.ToArray()));
    }
}
