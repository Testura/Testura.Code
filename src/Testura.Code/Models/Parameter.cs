using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Models
{
    public class Parameter
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public Parameter(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public Parameter(string name, Type type) : this(name, type.Name)
        {
        }
    }
}
