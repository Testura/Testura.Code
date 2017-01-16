using System;
using System.Collections.Generic;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;

namespace Testura.Code.Models.References
{
    public class MethodReference : MemberReference
    {
        public MethodReference(
            string methodName,
            IEnumerable<IArgument> arguments = null,
            IEnumerable<Type> genericTypes = null)
            : this(methodName, null, arguments, genericTypes)
        {
        }

        public MethodReference(
            string methodName,
            MemberReference member,
            IEnumerable<IArgument> arguments = null,
            IEnumerable<Type> genericTypes = null)
            : base(methodName, member)
        {
            Arguments = arguments == null ? new List<IArgument>() : new List<IArgument>(arguments);
            GenericTypes = genericTypes == null ? new List<Type>() : new List<Type>(genericTypes);
        }

        public IList<IArgument> Arguments { get; private set; }

        public IList<Type> GenericTypes { get; private set; }
    }
}
