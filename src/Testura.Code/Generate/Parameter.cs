using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Generate
{
    /// <summary>
    /// Generate code for parameters
    /// </summary>
    public static class Parameters
    {
        /// <summary>
        /// Create parameters for constructor/method/function
        /// </summary>
        /// <param name="parameters">A list of parameters</param>
        /// <returns>A parameter list syntax</returns>
        public static ParameterListSyntax Create(List<Parameter> parameters)
        {
            var list = new List<SyntaxNodeOrToken>();
            foreach (Parameter parameter in parameters)
            {
                list.Add(SyntaxFactory.Parameter(SyntaxFactory.Identifier(parameter.Name)).WithType(SyntaxFactory.IdentifierName(parameter.Type)));
                list.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));
            }
            list.RemoveAt(list.Count - 1);
            return SyntaxFactory.ParameterList(SyntaxFactory.SeparatedList<ParameterSyntax>(list.ToArray()));
        }
    }

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
