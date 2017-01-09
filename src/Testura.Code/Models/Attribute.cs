using System.Collections.Generic;
using Testura.Code.Helpers.Common.Arguments.ArgumentTypes;

namespace Testura.Code.Models
{
    public class Attribute
    {
        /// <summary>
        /// Gets or sets the name of the attribute
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the argument to the attribute
        /// </summary>
        public List<IArgument> Arguments { get; set; }

        public Attribute(string name)
        {
            Name = name;
            Arguments = new List<IArgument>();
        }

        public Attribute(string name, List<IArgument> arguments)
        {
            Name = name;
            Arguments = arguments;
        }
    }
}
