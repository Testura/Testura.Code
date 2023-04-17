namespace Testura.Code.Models;

/// <summary>
/// Represent a parameter.
/// </summary>
public class Parameter
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Parameter"/> class.
    /// </summary>
    /// <param name="name">Name of the parameter.</param>
    /// <param name="type">Type of the parameter.</param>
    /// <param name="modifier">The parameter modifiers.</param>
    /// <param name="xmlDocumentation">The parameters xml documentation.</param>
    /// <param name="attributes">The parameters attributes </param>
    public Parameter(
        string name,
        Type type,
        ParameterModifiers modifier = ParameterModifiers.None,
        string xmlDocumentation = null,
        IEnumerable<Attribute> attributes = null)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Type = type ?? throw new ArgumentNullException(nameof(type));
        Modifier = modifier;
        XmlDocumentation = xmlDocumentation;
        Attributes = attributes;
    }

    /// <summary>
    /// Gets or sets the name of the parameter.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the type of the parameters.
    /// </summary>
    public Type Type { get; set; }

    /// <summary>
    /// Gets or sets the parameter modifier.
    /// </summary>
    public ParameterModifiers Modifier { get; set; }

    /// <summary>
    /// Gets or sets the xml documentation
    /// </summary>
    public string XmlDocumentation { get; set; }

    /// <summary>
    /// Gets or sets the attributes of the parameter.
    /// </summary>
    public IEnumerable<Attribute> Attributes { get; set; }
}
