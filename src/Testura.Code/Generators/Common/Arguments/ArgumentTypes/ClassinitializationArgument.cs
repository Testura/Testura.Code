using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes
{
    public class ClassInitializationArgument : IArgument
    {
        private readonly Type _type;
        private readonly IList<IArgument> _arguments;
        private readonly IList<Type> _genericTypes;

        public ClassInitializationArgument(
            Type type,
            IEnumerable<IArgument> arguments = null,
            IEnumerable<Type> genericTypes = null)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            _type = type;
            _arguments = arguments == null ? new List<IArgument>() : new List<IArgument>(arguments);
            _genericTypes = genericTypes == null ? new List<Type>() : new List<Type>(genericTypes);
        }

        public ArgumentSyntax GetArgumentSyntax()
        {
            if (_genericTypes != null && _genericTypes.Any())
            {
                return SyntaxFactory.Argument(SyntaxFactory.ObjectCreationExpression(GenericGenerator.Create(_type.Name, _genericTypes.ToArray())).WithArgumentList(ArgumentGenerator.Create(_arguments.ToArray())));
            }

            return SyntaxFactory.Argument(SyntaxFactory.ObjectCreationExpression(TypeGenerator.Create(_type)).WithArgumentList(ArgumentGenerator.Create(_arguments.ToArray())));
        }
    }
}