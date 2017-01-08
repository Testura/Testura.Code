using System.Collections.Generic;
using Testura.Code.Helper.Arguments.ArgumentTypes;

namespace Testura.Code.Helper.References
{
    public class MethodReference : MemberReference
    {
        public IList<IArgument> Arguments { get; private set; }

        public MethodReference(string variableName, IList<IArgument> arguments)
            : base(variableName, MemberReferenceTypes.Method)
        {
            Arguments = arguments;
        }

        public MethodReference(string variableName, IList<IArgument> arguments, MemberReference member) 
            : base(variableName, MemberReferenceTypes.Method, member)
        {
            Arguments = arguments;
        }
    }
}
