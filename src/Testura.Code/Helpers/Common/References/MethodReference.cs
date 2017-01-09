using System.Collections.Generic;
using Testura.Code.Helpers.Common.Arguments.ArgumentTypes;

namespace Testura.Code.Helpers.Common.References
{
    public class MethodReference : MemberReference
    {
        public IList<IArgument> Arguments { get; private set; }

        public MethodReference(string methodName, IList<IArgument> arguments)
            : base(methodName)
        {
            Arguments = arguments;
        }

        public MethodReference(string methodName, IList<IArgument> arguments, MemberReference member) 
            : base(methodName, member)
        {
            Arguments = arguments;
        }
    }
}
