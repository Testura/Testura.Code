using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
#pragma warning disable 1591

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes;

/// <summary>
/// Provides the functionality to generate a <c>typeOf(..)</c> argument
/// </summary>
public class TypeOfArgument : Argument
{
    private readonly Type _type;

    /// <summary>
    /// Initializes a new instance of the <see cref="TypeOfArgument"/> class.
    /// </summary>
    /// <param name="type">The type inside the <c>typeOf</c>.</param>
    /// <param name="namedArgument">Specify the argument for a particular parameter.</param>
    public TypeOfArgument(Type type, string namedArgument = null)
        : base(namedArgument)
    {
        _type = type ?? throw new ArgumentNullException(nameof(type));
    }

    protected override ArgumentSyntax CreateArgumentSyntax()
    {
        return SyntaxFactory.Argument(SyntaxFactory.TypeOfExpression(TypeGenerator.Create(_type)));
    }
}
