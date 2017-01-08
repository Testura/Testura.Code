namespace Testura.Code
{
    public enum AssertType
    {
        AreEqual,
        AreNotEqual,
        IsTrue,
        IsFalse,
        Contains
    }

    public enum ConditionalStatement
    {
        Equal,
        NotEqual,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual
    }


    public enum MemberReferenceTypes
    {
        Property,
        Field,
        Method
    }

    public enum Modifiers
    {
        Public,
        Private,
        Static,
        Abstract,
        Virtual
    }
}
