using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Generate.ArgumentTypes
{
    public class ValueArgument : IArgument
    {
        public object NameOrvalue { get; set; }
        public ArgumentType ArgumentType { get; set; }

        public ValueArgument(object nameOrValue, ArgumentType argumentType = ArgumentType.Other)
        {
            if (argumentType == ArgumentType.String)
                nameOrValue = $"\"{nameOrValue}\"";
            if (argumentType == ArgumentType.Path)
                nameOrValue = $"@\"{nameOrValue}\"";
            NameOrvalue = nameOrValue;
            ArgumentType = argumentType;
        }

        public ArgumentSyntax GetArgumentSyntax()
        {
            if (NameOrvalue is bool)
            {
                return SyntaxFactory.Argument(SyntaxFactory.IdentifierName(NameOrvalue.ToString().ToLower()));
            }
            return SyntaxFactory.Argument(SyntaxFactory.IdentifierName(NameOrvalue.ToString()));
        }
    }
}