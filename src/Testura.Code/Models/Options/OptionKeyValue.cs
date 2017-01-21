using Microsoft.CodeAnalysis.Options;

namespace Testura.Code.Models.Options
{
    public class OptionKeyValue
    {
        public OptionKeyValue(Option<bool> formattingOption, bool value)
        {
            FormattingOption = formattingOption;
            Value = value;
        }

        public Option<bool> FormattingOption { get; set; }

        public bool Value { get; set; }
    }
}
