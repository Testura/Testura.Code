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
    public Parameter(string name, Type type, ParameterModifiers modifier = ParameterModifiers.None, string xmlDocumentation = null)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Type = type ?? throw new ArgumentNullException(nameof(type));
        Modifier = modifier;
        XmlDocumentation = xmlDocumentation;
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
    /// Gets he xml documentation
    /// </summary>
    public string XmlDocumentation { get; }
}
