using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Extensions;
#pragma warning disable 1591

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes;

/// <summary>
/// Provides the functionality to generate simple value arguments.
/// </summary>
public class ValueArgument : Argument
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValueArgument"/> class.
    /// </summary>
    /// <param name="value">Value to send in as an argument.</param>
    /// <param name="namedArgument">Specify the argument for a particular parameter.</param>
    public ValueArgument(object value, string namedArgument = null)
        : base(namedArgument)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (!(value.IsNumeric() || value is bool or string))
        {
            throw new ArgumentException($"{nameof(value)} must be a number or boolean");
        }

        Value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueArgument"/> class.
    /// </summary>
    /// <param name="value">String value to send in as an argument.</param>
    /// <param name="stringType">The type of string.</param>
    /// <param name="namedArgument">Specify the argument for a particular parameter.</param>
    public ValueArgument(string value, StringType stringType = StringType.Normal, string namedArgument = null)
        : base(namedArgument)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        Value = stringType == StringType.Path ? $"@\"{value}\"" : $"\"{value}\"";
    }

    /// <summary>
    /// Gets the value sent in as an argument.
    /// </summary>
    public object Value { get; }

    protected override ArgumentSyntax CreateArgumentSyntax()
    {
        if (Value is bool)
        {
            return SyntaxFactory.Argument(SyntaxFactory.IdentifierName(Value.ToString().ToLower()));
        }

        return SyntaxFactory.Argument(SyntaxFactory.IdentifierName(Value.ToString()));
    }
}
