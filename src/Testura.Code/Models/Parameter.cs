using System;

namespace Testura.Code.Models
{
    public class Parameter
    {
        public Parameter(string name, Type type)
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
        }

        /// <summary>
        /// Gets or sets the name of the parameter
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the parameters
        /// </summary>
        public Type Type { get; set; }
    }
}
