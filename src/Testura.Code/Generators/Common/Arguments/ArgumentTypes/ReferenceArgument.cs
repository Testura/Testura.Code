using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Models.References;
#pragma warning disable 1591

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes;

/// <summary>
/// Provides the functionality to generate reference arguments. Example of generated code: <c>(i.MyProperty)</c>
/// </summary>
public class ReferenceArgument : Argument
{
    private readonly VariableReference _reference;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReferenceArgument"/> class.
    /// </summary>
    /// <param name="reference">The variable/method reference.</param>
    /// <param name="namedArgument">Specify the argument for a particular parameter.</param>
    public ReferenceArgument(VariableReference reference, string namedArgument = null)
        : base(namedArgument)
    {
        _reference = reference ?? throw new ArgumentNullException(nameof(reference));
    }

    protected override ArgumentSyntax CreateArgumentSyntax()
    {
        return SyntaxFactory.Argument(ReferenceGenerator.Create(_reference));
    }
}
