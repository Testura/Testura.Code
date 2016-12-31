using System;

namespace Testura.Code.Reference
{
    public class NullReference : VariableReference
    {
        public NullReference(Type variableType) : base("null", variableType)
        {
        }

        protected NullReference(Type variableType, MemberReference member) : base("null", variableType, member)
        {
        }
    }
}
