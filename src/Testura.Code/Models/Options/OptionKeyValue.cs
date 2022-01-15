using Microsoft.CodeAnalysis.Options;

namespace Testura.Code.Models.Options;

/// <summary>
/// Represent the formation options when saving generated code.
/// <example>
/// <c>>new OptionKeyValue(CSharpFormattingOptions.NewLinesForBracesInMethods, false) </c>
/// </example>
/// </summary>
public class OptionKeyValue
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OptionKeyValue"/> class.
    /// </summary>
    /// <param name="formattingOption">The CSharpFormattingOptions.</param>
    /// <param name="value">If the formatting option should be on or off.</param>
    public OptionKeyValue(Option<bool> formattingOption, bool value)
    {
        FormattingOption = formattingOption;
        Value = value;
    }

    /// <summary>
    /// Gets or sets the formatting option.
    /// </summary>
    public Option<bool> FormattingOption { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the formatting option should be on or off.
    /// </summary>
    public bool Value { get; set; }
}
