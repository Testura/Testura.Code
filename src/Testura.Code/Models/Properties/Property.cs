using System;
using System.Collections.Generic;

namespace Testura.Code.Models.Properties
{
    public abstract class Property
    {
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
        /// Gets or sets the name of the property
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the property
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets the modifiers of the property
        /// </summary>
        public IEnumerable<Modifiers> Modifiers { get; set; }

        /// <summary>
        /// Gets or sets the attrbites of a property
        /// </summary>
        public IEnumerable<Attribute> Attributes { get; set; }
    }
}
