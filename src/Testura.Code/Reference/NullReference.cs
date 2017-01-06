using System;

namespace Testura.Code.Reference
{
    public class NullReference : VariableReference
    {
        public NullReference() : base("null")
        {
        }

        protected NullReference(MemberReference member) : base("null", member)
        {
        }
    }
}
