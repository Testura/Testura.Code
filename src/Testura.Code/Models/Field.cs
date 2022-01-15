using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Models;

/// <summary>
/// Represent a class field.
/// </summary>
public class Field
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Field"/> class.
    /// </summary>
    /// <param name="name">Name of the field.</param>
    /// <param name="type">Type of the field.</param>
    /// <param name="modifiers">The fields modifiers.</param>
    /// <param name="attributes">The fields attributes.</param>
    /// <param name="initializeWith">Expression used to initialize field</param>
    /// <param name="summary">XML documentation summary</param>
    public Field(
        string name,
        Type type,
        IEnumerable<Modifiers> modifiers = null,
        IEnumerable<Attribute> attributes = null,
        ExpressionSyntax initializeWith = null,
        string summary = null)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Type = type ?? throw new ArgumentNullException(nameof(type));
        Modifiers = modifiers;
        Attributes = attributes;
        InitializeWith = initializeWith;
        Summary = summary;
    }

    /// <summary>
    /// Gets or sets the name of the field.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the type of the field.
    /// </summary>
    public Type Type { get; set; }

    /// <summary>
    /// Gets or sets the modifier(s) of the field.
    /// </summary>
    public IEnumerable<Modifiers>? Modifiers { get; set; }

    /// <summary>
    /// Gets or sets the attributes of the field.
    /// </summary>
    public IEnumerable<Attribute>? Attributes { get; set; }

    /// <summary>
    /// Gets or sets expression used to assign field.
    /// </summary>
    public ExpressionSyntax? InitializeWith { get; set; }

    /// <summary>
    /// Gets the xml documentation summary of the field.
    /// </summary>
    public string? Summary { get; }
}
