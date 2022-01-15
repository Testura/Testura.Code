#pragma warning disable 1591
namespace Testura.Code.Models.References;

/// <summary>
/// Represent a null reference.
/// </summary>
public class NullReference : VariableReference
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NullReference"/> class.
    /// </summary>
    public NullReference()
        : base("null")
    {
    }

    protected NullReference(MemberReference member)
        : base("null", member)
    {
    }
}