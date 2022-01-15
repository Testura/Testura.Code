namespace Testura.Code.Models;

/// <summary>
/// Represent a enum member.
/// </summary>
public class EnumMember
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EnumMember"/> class.
    /// </summary>
    /// <param name="name">Name of the enum member</param>
    /// <param name="value">Value of the enum member</param>
    /// <param name="attributes">Attributes of the enum member</param>
    public EnumMember(string name, int? value = null, IEnumerable<Attribute>? attributes = null)
    {
        Name = name;
        Value = value;
        Attributes = attributes;
    }

    /// <summary>
    /// Gets or sets the name of the enum member.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the value of the enum member.
    /// </summary>
    public int? Value { get; set; }

    /// <summary>
    /// Gets or sets the attributes of the enum member.
    /// </summary>
    public IEnumerable<Attribute>? Attributes { get; set; }
}
