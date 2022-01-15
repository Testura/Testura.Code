using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes;

/// <summary>
/// Provides the functionality to generate an assign argument. Example of generated code:
/// <c>(value = 1)</c>
/// </summary>
public class AssignArgument : IArgument
{
    private readonly string _name;
    private readonly ExpressionSyntax _expressionSyntax;

    /// <summary>
    /// Initializes a new instance of the <see cref="AssignArgument"/> class.
    /// </summary>
    /// <param name="name">Name of the parameter to assign</param>
    /// <param name="value">The assign value</param>
    public AssignArgument(string name, object value)
    {
        _name = name;
        _expressionSyntax = IdentifierName(value is bool ? value.ToString().ToLower() : value.ToString());
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AssignArgument"/> class.
    /// </summary>
    /// <param name="name">Name of the parameter to assign</param>
    /// <param name="value">The assign value</param>
    /// <param name="stringType">The type of string</param>
    public AssignArgument(string name, string value, StringType stringType = StringType.Normal)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        _name = name;
        _expressionSyntax = IdentifierName(stringType == StringType.Path ? $"@\"{value}\"" : $"\"{value}\"");
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AssignArgument"/> class.
    /// </summary>
    /// <param name="name">Name of the parameter to assign</param>
    /// <param name="expressionSyntax">The assign expression syntax</param>
    public AssignArgument(string name, ExpressionSyntax expressionSyntax)
    {
        _name = name;
        _expressionSyntax = expressionSyntax;
    }

    /// <summary>
    /// Get the generated argument syntax.
    /// </summary>
    /// <returns>The generated argument syntax</returns>
    public ArgumentSyntax GetArgumentSyntax()
    {
        return Argument(
            AssignmentExpression(
                SyntaxKind.SimpleAssignmentExpression,
                IdentifierName(_name),
                _expressionSyntax));
    }
}
