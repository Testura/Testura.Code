using Testura.Code.Extensions;
#pragma warning disable 1591

namespace Testura.Code.Models.References;

/// <summary>
/// Represent the reference to a constant value.
/// </summary>
public class ConstantReference : VariableReference
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstantReference"/> class.
    /// </summary>
    /// <param name="value">Value of the constant.</param>
    public ConstantReference(object value)
        : base(value?.ToString())
    {
        if (!(value.IsNumeric() || value is bool))
        {
            throw new ArgumentException($"{nameof(value)} must be a number or boolean.");
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConstantReference"/> class.
    /// </summary>
    /// <param name="value">Value of the constant</param>
    /// <param name="stringType">The type of string to generate.</param>
    public ConstantReference(string value, StringType stringType = StringType.Normal)
        : base(value)
    {
        Name = stringType == StringType.Path ? $"@\"{value}\"" : $"\"{value}\"";
    }

    protected ConstantReference(string variableName, MemberReference member)
        : base(variableName, member)
    {
    }
}
