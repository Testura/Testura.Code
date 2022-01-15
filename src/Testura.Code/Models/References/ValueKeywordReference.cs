#pragma warning disable 1591
namespace Testura.Code.Models.References;

/// <summary>
/// Represent a <c>value</c> keyword reference.
/// </summary>
public class ValueKeywordReference : VariableReference
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValueKeywordReference"/> class.
    /// </summary>
    public ValueKeywordReference()
        : base("value")
    {
    }

    protected ValueKeywordReference(MemberReference member)
        : base("value", member)
    {
    }
}