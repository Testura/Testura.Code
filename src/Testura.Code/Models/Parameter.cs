using System;

namespace Testura.Code.Models
{
    public class Parameter
    {
        public Parameter(string name, Type type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; set; }

        public Type Type { get; set; }
    }
}
