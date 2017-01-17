using System;
using System.Collections.Generic;

namespace Testura.Code.Models
{
    public class Property
    {
        public Property(
            string name,
            Type type,
            PropertyTypes propertyType,
            IEnumerable<Code.Modifiers> modifiers = null,
            IEnumerable<Attribute> attributes = null)
        {
            Name = name;
            Type = type;
            PropertyType = propertyType;
            Modifiers = modifiers;
            Attributes = attributes;
        }

        /// <summary>
        /// Gets or sets the name of the property
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the property
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets if the property is a get or set.
        /// </summary>
        public PropertyTypes PropertyType { get; set; }

        /// <summary>
        /// Gets or set the modifiers of the property
        /// </summary>
        public IEnumerable<Modifiers> Modifiers { get; set; }

        /// <summary>
        /// Gets or sets the attrbites of a property
        /// </summary>
        public IEnumerable<Attribute> Attributes { get; set; }
    }
}
