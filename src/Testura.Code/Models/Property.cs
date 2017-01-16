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

        public string Name { get; set; }

        public Type Type { get; set; }

        public PropertyTypes PropertyType { get; set; }

        public IEnumerable<Modifiers> Modifiers { get; set; }

        public IEnumerable<Attribute> Attributes { get; set; }
    }
}
