using System.Collections.Generic;
using Testura.Code.Helpers.Arguments.ArgumentTypes;

namespace Testura.Code.Helpers.References
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
