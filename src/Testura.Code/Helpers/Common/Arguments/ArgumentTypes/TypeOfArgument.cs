using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Helpers.Common.Arguments.ArgumentTypes
{
    public class TypeOfArgument : IArgument
    {
        private readonly string typeName;

        public TypeOfArgument(Type type)
        {
            typeName = type.Name;
        }

        public TypeOfArgument(string type)
        {
            typeName = type;
        }

        public ArgumentSyntax GetArgumentSyntax()
        {
            return SyntaxFactory.Argument(SyntaxFactory.TypeOfExpression(SyntaxFactory.IdentifierName(typeName)));
        }
    }
}