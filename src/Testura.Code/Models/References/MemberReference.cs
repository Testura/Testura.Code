namespace Testura.Code.Models.References
{
    public class MemberReference : VariableReference
    {
        public MemberReference(string name)
            : base(name)
        {
        }

        public MemberReference(string name, MemberReference memberReference)
            : this(name)
        {
            Member = memberReference;
        }
    }
}
