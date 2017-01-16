using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Extensions;

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes
{
    public class ValueArgument : IArgument
    {
        public ValueArgument(object value)
        {
            if (!(value.IsNumeric() || value is bool))
            {
                throw new ArgumentException($"{nameof(value)} must be a number, boolean or string.");
            }

            Value = value;
            StringType = StringType.Normal;
        }

        public ValueArgument(string value, StringType stringType = StringType.Normal)
        {
            Value = stringType == StringType.Path ? $"@\"{value}\"" : $"\"{value}\"";
        }

        public object Value { get; }

        public StringType StringType { get; }

        public ArgumentSyntax GetArgumentSyntax()
        {
            if (Value is bool)
            {
                return SyntaxFactory.Argument(SyntaxFactory.IdentifierName(Value.ToString().ToLower()));
            }

            return SyntaxFactory.Argument(SyntaxFactory.IdentifierName(Value.ToString()));
        }
    }
}