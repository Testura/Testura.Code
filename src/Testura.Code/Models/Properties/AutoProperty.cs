using System;
using System.Collections.Generic;

namespace Testura.Code.Models.Properties
{
    /// <summary>
    /// Represent an auto property
    /// </summary>
    public class AutoProperty : Property
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoProperty"/> class.
        /// </summary>
        /// <param name="name">Name of the property.</param>
        /// <param name="type">The property type.</param>
        /// <param name="propertyType">What kind of of property to generate (get or get/set).</param>
        /// <param name="modifiers">The property modifiers.</param>
        /// <param name="attributes">Attributes on the property.</param>
        /// <param name="getModifiers">The get modifiers.</param>
        /// <param name="setModifiers">The set modifiers.</param>
        public AutoProperty(
            string name,
            Type type,
            PropertyTypes propertyType,
            IEnumerable<Code.Modifiers> modifiers = null,
            IEnumerable<Attribute> attributes = null,
            IEnumerable<Modifiers> getModifiers = null,
            IEnumerable<Modifiers> setModifiers = null)
            : base(name, type, modifiers, attributes, getModifiers, setModifiers)
        {
            PropertyType = propertyType;
        }

        /// <summary>
        /// Gets or sets if the property is a get or set.
        /// </summary>
        public PropertyTypes PropertyType { get; set; }
    }
}
