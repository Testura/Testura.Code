using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Models;

namespace Testura.Code.Generators.Common
{
    public static class ParameterGenerator
    {
        /// <summary>
        /// Create syntax for a list of parameter
        /// </summary>
        /// <param name="parameters">Parameter(s) to create</param>
        /// <returns>The declared parameter list syntax</returns>
        public static ParameterListSyntax Create(params Parameter[] parameters)
        {
            var parameterSyntaxes = new ParameterSyntax[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                parameterSyntaxes[i] = Create(parameters[i]);
            }

            return ConvertParameterSyntaxToList(parameterSyntaxes);
        }

        internal static ParameterSyntax Create(Parameter parameter)
        {
            return
                SyntaxFactory.Parameter(SyntaxFactory.Identifier(parameter.Name))
                    .WithType(TypeGenerator.Create(parameter.Type));
        }

        internal static ParameterListSyntax ConvertParameterSyntaxToList(params ParameterSyntax[] parameters)
        {
            var list = new List<SyntaxNodeOrToken>();
            foreach (ParameterSyntax parameter in parameters)
            {
                list.Add(parameter);
                list.Add(SyntaxFactory.Token(SyntaxKind.CommaToken));
            }

            list.RemoveAt(list.Count - 1);
            return SyntaxFactory.ParameterList(SyntaxFactory.SeparatedList<ParameterSyntax>(list.ToArray()));
        }
    }
}
