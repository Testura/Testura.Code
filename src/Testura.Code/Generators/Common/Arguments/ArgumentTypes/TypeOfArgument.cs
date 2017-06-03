using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes
{
    public class TypeOfArgument : Argument
    {
        private readonly Type _type;

        public TypeOfArgument(Type type, string namedArgument = null)
            : base(namedArgument)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            _type = type;
        }

        protected override ArgumentSyntax CreateArgumentSyntax()
        {
            return SyntaxFactory.Argument(SyntaxFactory.TypeOfExpression(TypeGenerator.Create(_type)));
        }
    }
}