using System;
using System.Collections.Generic;
using Testura.Code.Helpers.Common.Arguments.ArgumentTypes;

namespace Testura.Code.Helpers.Common.References
{
    public class MethodReference : MemberReference
    {
        public IList<IArgument> Arguments { get; private set; }
        public IList<Type> GenericTypes { get; private set; }

        public MethodReference(
            string methodName,
            IList<IArgument> arguments = null,
            IList<Type> genericTypes = null)
            : base(methodName)
        {
            Arguments = arguments ?? new List<IArgument>();
            GenericTypes = genericTypes ?? new List<Type>();
        }

        public MethodReference(
            string methodName,
            MemberReference member,
            IList<IArgument> arguments = null,
            IList<Type> genericTypes = null)
            : base(methodName, member)
        {
            Arguments = arguments ?? new List<IArgument>();
            GenericTypes = genericTypes ?? new List<Type>();
        }
    }
}
