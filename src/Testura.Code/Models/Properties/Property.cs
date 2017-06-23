using System;
using System.Collections.Generic;

namespace Testura.Code.Models.Properties
{
    /// <summary>
    /// Provides the base class from which classes that represent properties are derived.
    /// </summary>
    public abstract class Property
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Property"/> class.
        /// </summary>
        /// <param name="name">Name of the property.</param>
        /// <param name="type">The property type.</param>
        /// <param name="modifiers">The properpty modifiers.</param>
        /// <param name="attributes">Attributes on the property.</param>
        protected Property(
            string name,
            Type type,
            IEnumerable<Code.Modifiers> modifiers = null,
            IEnumerable<Attribute> attributes = null)
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
            Modifiers = modifiers;
            Attributes = attributes;
        }

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the property.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets the modifiers of the property.
        /// </summary>
        public IEnumerable<Modifiers> Modifiers { get; set; }

        /// <summary>
        /// Gets or sets the attributes of the property.
        /// </summary>
        public IEnumerable<Attribute> Attributes { get; set; }
    }
}
