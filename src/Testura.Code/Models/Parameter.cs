using System;

namespace Testura.Code.Models
{
    /// <summary>
    /// Represent a parameter.
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Parameter"/> class.
        /// </summary>
        /// <param name="name">Name of the paramter.</param>
        /// <param name="type">Type of the paramter.</param>
        /// <param name="modifier">The paramter modifiers.</param>
        /// <param name="xmlDocumentation">The parameters xml documentation.</param>
        public Parameter(string name, Type type, ParameterModifiers modifier = ParameterModifiers.None, string xmlDocumentation = null)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            Name = name;
            Type = type;
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
        /// Gets or sets the xml documentation
        /// </summary>
        public string XmlDocumentation { get; }
    }
}
