using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common.BinaryExpressions;
#pragma warning disable 1591

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes;

/// <summary>
/// Provides the functionality to generate binary expression argument. Example of generated code: <c>(1+2)</c>
/// </summary>
public class BinaryExpressionArgument : Argument
{
    private readonly IBinaryExpression _binaryExpression;

    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryExpressionArgument"/> class.
    /// </summary>
    /// <param name="binaryExpression">The binary expression.</param>
    /// <param name="namedArgument">Specify the argument for a particular parameter.</param>
    public BinaryExpressionArgument(IBinaryExpression binaryExpression, string namedArgument = null)
        : base(namedArgument)
    {
        _binaryExpression = binaryExpression ?? throw new ArgumentNullException(nameof(binaryExpression));
    }

    protected override ArgumentSyntax CreateArgumentSyntax()
    {
        return SyntaxFactory.Argument(_binaryExpression.GetBinaryExpression());
    }
}
