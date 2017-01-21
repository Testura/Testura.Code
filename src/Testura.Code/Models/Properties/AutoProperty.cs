using System;
using System.Collections.Generic;

namespace Testura.Code.Models.Properties
{
    public class AutoProperty : Property
    {
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
