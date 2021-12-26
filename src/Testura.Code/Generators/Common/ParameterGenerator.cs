using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Models;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Generators.Common
{
    /// <summary>
    /// Provides the functionality to generate parameter lists.
    /// </summary>
    public static class ParameterGenerator
    {
        /// <summary>
        /// Create syntax for a list of parameter.
        /// </summary>
        /// <param name="parameters">Parameter(s) to create.</param>
        /// <returns>The declared parameter list syntax.</returns>
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
            var parameterSyntax = Parameter(Identifier(parameter.Name)).WithType(TypeGenerator.Create(parameter.Type));

            switch (parameter.Modifier)
            {
                case ParameterModifiers.Out:
                    parameterSyntax = parameterSyntax.WithModifiers(TokenList(Token(SyntaxKind.OutKeyword)));
                    break;
                case ParameterModifiers.Ref:
                    parameterSyntax = parameterSyntax.WithModifiers(TokenList(Token(SyntaxKind.RefKeyword)));
                    break;
                case ParameterModifiers.This:
                    parameterSyntax = parameterSyntax.WithModifiers(TokenList(Token(SyntaxKind.ThisKeyword)));
                    break;
            }

            return parameterSyntax;
        }

        internal static ParameterListSyntax ConvertParameterSyntaxToList(params ParameterSyntax[] parameters)
        {
            if (parameters.Length == 0)
            {
                return ParameterList(SeparatedList<ParameterSyntax>(new SyntaxNodeOrToken[0]));
            }

            var list = new List<SyntaxNodeOrToken>();
            foreach (var parameter in parameters)
            {
                list.Add(parameter);
                list.Add(Token(SyntaxKind.CommaToken));
            }

            list.RemoveAt(list.Count - 1);
            return ParameterList(SeparatedList<ParameterSyntax>(list.ToArray()));
        }
    }
}
