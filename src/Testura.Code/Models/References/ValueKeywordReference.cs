namespace Testura.Code.Models.References
{
    public class ValueKeywordReference : VariableReference
    {
        public ValueKeywordReference()
            : base("value")
        {
        }

        protected ValueKeywordReference(MemberReference member)
            : base("value", member)
        {
        }
    }
}
