using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testura.Code.Models
{
    public class Field
    {
        public Field(
            string name,
            Type type,
            IEnumerable<Modifiers> modifiers = null)
        {
            Name = name;
            Type = type;
            Modifiers = modifiers;
        }

        public string Name { get; set; }

        public Type Type { get; set; }

        public IEnumerable<Modifiers> Modifiers { get; set; }

    }
}
