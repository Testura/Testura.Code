using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Helper.Arguments.ArgumentTypes
{
    public class TypeOfArgument : IArgument
    {
        private readonly Type type;

        public TypeOfArgument(Type type)
        {
            this.type = type;
        }

        public ArgumentSyntax GetArgumentSyntax()
        {
            return SyntaxFactory.Argument(SyntaxFactory.TypeOfExpression(SyntaxFactory.IdentifierName(type.Name)));
        }
    }
}