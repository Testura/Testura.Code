namespace Testura.Code.Helper.References
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
