using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Helpers.Common.Arguments.ArgumentTypes
{
    public class ValueArgument : IArgument
    {
        public ValueArgument(object value, ArgumentType argumentType = ArgumentType.Normal)
        {
            Value = value;
            if (value is string)
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

            ArgumentType = argumentType;
        }

        public object Value { get; set; }

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