using System;
using Testura.Code.Extensions;
using Testura.Code.Generators.Common;

namespace Testura.Code.Models.References
{
    public class ConstantReference : VariableReference
    {
        public ConstantReference(object value)
            : base(value.ToString())
        {
            if (!(value.IsNumeric() || value is bool))
            {
                throw new ArgumentException($"{nameof(value)} must be a number, boolean or string.");
            }
        }

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
}
