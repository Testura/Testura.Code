using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common;
using Testura.Code.Models;

namespace Testura.Code.Generators.Class
{
    /// <summary>
    /// Generate code for a class
    /// </summary>
    public static class ClassGenerator
    {
        /// <summary>
        /// Create the constructor for a class
        /// </summary>
        /// <param name="className">Name of the class</param>
        /// <param name="parameters">Parameters of the constructor</param>
        /// <param name="body">Body of the constructor</param>
        /// <returns>A constructor decleration</returns>
        public static ConstructorDeclarationSyntax Constructor(
            string className,
            BlockSyntax body,
            IEnumerable<Parameter> parameters = null,
            IEnumerable<Modifiers> modifiers = null,
            IEnumerable<Attribute> attributes = null)
        {
            var constructor = SyntaxFactory.ConstructorDeclaration(SyntaxFactory.Identifier(className))
                        .WithBody(body);
            if (parameters != null)
            {
                constructor = constructor.WithParameterList(Parameters.Create(parameters.ToArray()));
            }

            if (attributes != null)
            {
                constructor = constructor.WithAttributeLists(AttributeGenerator.Create(attributes.ToArray()));
            }

            if (modifiers != null)
            {
                constructor = constructor.WithModifiers(Common.ModifierGenerator.Create(modifiers.ToArray()));
            }

            return constructor;
        }
    }
}
