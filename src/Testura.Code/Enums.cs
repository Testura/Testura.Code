namespace Testura.Code
{
    public enum AssertType
    {
        AreEqual,
        AreNotEqual,
        IsTrue,
        IsFalse,
        Contains,
        AreSame,
        AreNotSame
    }

    public enum ConditionalStatements
    {
        Equal,
        NotEqual,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual
    }

    public enum Modifiers
    {
        Public,
        Private,
        Static,
        Abstract,
        Virtual
    }

    public enum ParameterModifiers
    {
        None,
        Out,
        Ref,
        This
    }

    public enum PropertyTypes
    {
        Get,
        GetAndSet
    }

    public enum StringType
    {
        Normal,
        Path
    }

    public enum MathOperators
    {
        Add,
        Subtract,
        Divide,
        Multiply
    }
}
