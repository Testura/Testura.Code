using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes
{
    public class ClassInitialiationArgument : IArgument
    {
        private readonly Type _type;
        private readonly IList<IArgument> _arguments;
        private readonly IList<Type> _genericTypes;

        public ClassInitialiationArgument(Type type, IEnumerable<IArgument> arguments, IEnumerable<Type> genericTypes = null)
        {
            _type = type;
            _arguments = new List<IArgument>(arguments);
            _genericTypes = new List<Type>(genericTypes);
        }

        public ClassInitialiationArgument(Type type, IEnumerable<Type> genericTypes = null)
        {
            _type = type;
            _arguments = new List<IArgument>();
            _genericTypes = new List<Type>(genericTypes);
        }

        public ArgumentSyntax GetArgumentSyntax()
        {
            if (_genericTypes != null && _genericTypes.Any())
            {
                return SyntaxFactory.Argument(SyntaxFactory.ObjectCreationExpression(GenericGenerator.Create(_type.Name, _genericTypes.ToArray())).WithArgumentList(ArgumentGenerator.Create(_arguments.ToArray())));
            }

            return SyntaxFactory.Argument(SyntaxFactory.ObjectCreationExpression(SyntaxFactory.IdentifierName(_type.Name)).WithArgumentList(ArgumentGenerator.Create(_arguments.ToArray())));
        }
    }
}