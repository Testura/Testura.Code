using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
#pragma warning disable 1591

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes;

/// <summary>
/// Provides the functionality to generate creation of an objection using it's constructor. Example of generated code: <c>(new MyClass())</c>
/// </summary>
public class ObjectCreationArgument : Argument
{
    private readonly Type _type;
    private readonly IList<IArgument> _arguments;
    private readonly IList<Type> _genericTypes;

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectCreationArgument"/> class.
    /// </summary>
    /// <param name="type">The object type to initialize.</param>
    /// <param name="arguments">Arguments used when initializing the class.</param>
    /// <param name="genericTypes">Generics of the type.</param>
    /// <param name="namedArgument">Specify the argument for a parameter.</param>
    public ObjectCreationArgument(
        Type type,
        IEnumerable<IArgument>? arguments = null,
        IEnumerable<Type>? genericTypes = null,
        string namedArgument = null)
        : base(namedArgument)
    {
        _type = type ?? throw new ArgumentNullException(nameof(type));
        _arguments = arguments == null ? new List<IArgument>() : new List<IArgument>(arguments);
        _genericTypes = genericTypes == null ? new List<Type>() : new List<Type>(genericTypes);
    }

    protected override ArgumentSyntax CreateArgumentSyntax()
    {
        return SyntaxFactory.Argument(ObjectCreationGenerator.Create(_type, _arguments, _genericTypes));
    }
}
