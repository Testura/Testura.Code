using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes
{
    public class ValueArgument : IArgument
    {
        public ValueArgument(object value)
        {
            if (!(value is sbyte
                || value is byte
                || value is short
                || value is ushort
                || value is int
                || value is uint
                || value is long
                || value is ulong
                || value is float
                || value is double
                || value is decimal
                || value is bool))
            {
                throw new ArgumentException($"{nameof(value)} must be a number, boolean or string.");
            }
            Value = value;
            ArgumentType = ArgumentType.Normal;
        }

        public ValueArgument(string value, ArgumentType argumentType = ArgumentType.Normal)
        {
            if (argumentType == ArgumentType.Path)
            {
                Value = $"@\"{value}\"";
            }
            else
            {
                Value = $"\"{value}\"";
            }
        }

        public object Value { get; }

        public ArgumentType ArgumentType { get; set; }


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