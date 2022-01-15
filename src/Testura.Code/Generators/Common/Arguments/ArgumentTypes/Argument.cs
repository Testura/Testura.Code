using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
#pragma warning disable 1591

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes;

/// <summary>
/// Provides the base class from which the classes that represent argument derived.
/// </summary>
public abstract class Argument : IArgument
{
    private readonly string _namedArgument;

    /// <summary>
    /// Initializes a new instance of the <see cref="Argument"/> class.
    /// </summary>
    /// <param name="namedArgument">Specify the argument for a particular parameter.</param>
    protected Argument(string namedArgument = null)
    {
        _namedArgument = namedArgument;
    }

    /// <summary>
    /// Get the generated argument syntax.
    /// </summary>
    /// <returns>The generated argument syntax</returns>
    public ArgumentSyntax GetArgumentSyntax()
    {
        var argumentSyntax = CreateArgumentSyntax();
        if (_namedArgument != null)
        {
            return argumentSyntax.WithNameColon(NameColon(IdentifierName(_namedArgument)));
        }

        return argumentSyntax;
    }

    protected abstract ArgumentSyntax CreateArgumentSyntax();
}
