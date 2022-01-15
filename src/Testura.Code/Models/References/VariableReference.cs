namespace Testura.Code.Models.References;

/// <summary>
/// The reference classes are used to simplify call to methods, fields, properties etc.
///
/// <example>
/// An example of this could be this:
///
/// <c>myVariable.MyMethod().AProperty;</c>
///
/// This could be represented like this:
/// <c>new VariableReference("myVariable", new MethodReference("MyMethod",new MemberReference("AProperty");</c>
/// </example>
/// </summary>
public class VariableReference
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VariableReference"/> class.
    /// </summary>
    /// <param name="variableName">Name of the variable,</param>
    public VariableReference(string variableName)
    {
        Name = variableName ?? throw new ArgumentNullException(nameof(variableName));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="VariableReference"/> class.
    /// </summary>
    /// <param name="variableName">Name of the variable</param>
    /// <param name="member">Member to reference on the variable</param>
    public VariableReference(string variableName, MemberReference member)
        : this(variableName)
    {
        Member = member;
    }

    /// <summary>
    /// Gets or sets the name of the reference.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the member references.
    /// </summary>
    public MemberReference? Member { get; set; }

    /// <summary>
    /// Go through the chain of members and return last.
    /// </summary>
    /// <returns>Returns last member in the reference chain.</returns>
    public VariableReference GetLastMember()
    {
        var child = Member;
        while (child?.Member != null)
        {
            child = child.Member;
        }

        return child;
    }
}
