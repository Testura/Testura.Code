using System;
using System.Collections.Generic;

namespace Testura.Code.Models
{
    public class Field
    {
        public Field(
            string name,
            Type type,
            IEnumerable<Modifiers> modifiers = null)
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
        }

        /// <summary>
        /// Gets or sets the name of the fileds
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the field
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or set the modifier(s) of the field
        /// </summary>
        public IEnumerable<Modifiers> Modifiers { get; set; }
    }
}
