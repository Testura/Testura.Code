namespace Testura.Code.Models.References;

/// <summary>
/// Represent the member reference on another reference.
/// </summary>
public class MemberReference : VariableReference
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MemberReference"/> class.
    /// </summary>
    /// <param name="name">Name of the member.</param>
    public MemberReference(string name)
        : base(name)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MemberReference"/> class.
    /// </summary>
    /// <param name="name">Name of the member reference.</param>
    /// <param name="memberReference">Member to reference on the member.</param>
    public MemberReference(string name, MemberReference memberReference)
        : this(name)
    {
        Member = memberReference;
    }
}
