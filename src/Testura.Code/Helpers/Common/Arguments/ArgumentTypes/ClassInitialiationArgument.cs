using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Helpers.Common.Arguments.ArgumentTypes
{
    public class ClassInitialiationArgument : IArgument
    {
        private readonly Type type;
        private readonly List<IArgument> arguments;
        private readonly IList<Type> genericTypes;

        public ClassInitialiationArgument(Type type, List<IArgument> arguments, IList<Type> genericTypes = null)
        {
            this.type = type;
            this.arguments = arguments;
            this.genericTypes = genericTypes;
        }

        public ClassInitialiationArgument(Type type, IList<Type> genericTypes = null)
        {
            this.type = type;
            this.arguments = new List<IArgument>();
            this.genericTypes = genericTypes;
        }

        public ArgumentSyntax GetArgumentSyntax()
        {
            if (genericTypes != null && genericTypes.Any())
            {
                return SyntaxFactory.Argument(SyntaxFactory.ObjectCreationExpression(Generic.Create(type.Name, genericTypes.ToArray())).WithArgumentList(Argument.Create(arguments.ToArray())));
            }
            return SyntaxFactory.Argument(SyntaxFactory.ObjectCreationExpression(SyntaxFactory.IdentifierName(type.Name)).WithArgumentList(Argument.Create(arguments.ToArray())));
        }
    }
}