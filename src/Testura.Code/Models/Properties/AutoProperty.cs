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
        /// <param name="propertyType">What kind of of property to geneate (get or get/set).</param>
        /// <param name="modifiers">The properpty modifiers.</param>
        /// <param name="attributes">Attributes on the property.</param>
        public AutoProperty(
            string name,
            Type type,
            PropertyTypes propertyType,
            IEnumerable<Code.Modifiers> modifiers = null,
            IEnumerable<Attribute> attributes = null)
            : base(name, type, modifiers, attributes)
        {
            PropertyType = propertyType;
        }

        /// <summary>
        /// Gets or sets if the property is a get or set.
        /// </summary>
        public PropertyTypes PropertyType { get; set; }
    }
}
