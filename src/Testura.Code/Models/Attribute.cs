using System;
using System.Collections.Generic;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;

namespace Testura.Code.Models
{
    /// <summary>
    /// Represent an attribute
    /// </summary>
    public class Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Attribute"/> class.
        /// </summary>
        /// <param name="name">Name of the attribute.</param>
        public Attribute(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            Arguments = new List<IArgument>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Attribute"/> class.
        /// </summary>
        /// <param name="name">Name of the attribute.</param>
        /// <param name="arguments">Arguments sent into the attribute.</param>
        public Attribute(string name, List<IArgument> arguments)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (arguments == null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            Name = name;
            Arguments = arguments;
        }

        /// <summary>
        /// Gets or sets the name of the attribute.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the argument to the attribute.
        /// </summary>
        public List<IArgument> Arguments { get; set; }
    }
}
