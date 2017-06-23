using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
#pragma warning disable 1591

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes
{
    /// <summary>
    /// Provides the functionality to generate a class initialization argument. Example of generated code: <c>(new MyClass())</c>
    /// </summary>
    public class ClassInitializationArgument : Argument
    {
        private readonly Type _type;
        private readonly IList<IArgument> _arguments;
        private readonly IList<Type> _genericTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassInitializationArgument"/> class.
        /// </summary>
        /// <param name="type">The class type to initialize.</param>
        /// <param name="arguments">Arguments used when initializing the class.</param>
        /// <param name="genericTypes">Generics of the class.</param>
        /// <param name="namedArgument">Specificy the argument for a partical parameter.</param>
        public ClassInitializationArgument(
            Type type,
            IEnumerable<IArgument> arguments = null,
            IEnumerable<Type> genericTypes = null,
            string namedArgument = null)
            : base(namedArgument)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            _type = type;
            _arguments = arguments == null ? new List<IArgument>() : new List<IArgument>(arguments);
            _genericTypes = genericTypes == null ? new List<Type>() : new List<Type>(genericTypes);
        }

        protected override ArgumentSyntax CreateArgumentSyntax()
        {
            if (_genericTypes != null && _genericTypes.Any())
            {
                return SyntaxFactory.Argument(SyntaxFactory.ObjectCreationExpression(GenericGenerator.Create(_type.Name, _genericTypes.ToArray())).WithArgumentList(ArgumentGenerator.Create(_arguments.ToArray())));
            }

            return SyntaxFactory.Argument(SyntaxFactory.ObjectCreationExpression(TypeGenerator.Create(_type)).WithArgumentList(ArgumentGenerator.Create(_arguments.ToArray())));
        }
    }
}