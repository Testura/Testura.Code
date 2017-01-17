using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common;
using Testura.Code.Models;
using Attribute = Testura.Code.Models.Attribute;

namespace Testura.Code.Generators.Class
{
    public static class ConstructorGenerator
    {
        /// <summary>
        /// Create the syntax for a class constructor
        /// </summary>
        /// <param name="className">Name of the class</param>
        /// <param name="body">The generated body of the constructor</param>
        /// <param name="parameters">A list with parameters</param>
        /// <param name="modifiers">A list with modifiers</param>
        /// <param name="attributes">A list with attributes</param>
        /// <returns>The decleration syntax for a constructor</returns>
        public static ConstructorDeclarationSyntax Create(
            string className,
            BlockSyntax body,
            IEnumerable<Parameter> parameters = null,
            IEnumerable<Modifiers> modifiers = null,
            IEnumerable<Attribute> attributes = null)
        {
            if (className == null)
            {
                throw new ArgumentNullException(nameof(className));
            }

            var constructor = SyntaxFactory.ConstructorDeclaration(SyntaxFactory.Identifier(className))
                        .WithBody(body);
            if (parameters != null)
            {
                constructor = constructor.WithParameterList(ParameterGenerator.Create(parameters.ToArray()));
            }

            if (attributes != null)
            {
                constructor = constructor.WithAttributeLists(AttributeGenerator.Create(attributes.ToArray()));
            }

            if (modifiers != null)
            {
                constructor = constructor.WithModifiers(ModifierGenerator.Create(modifiers.ToArray()));
            }

            return constructor;
        }
    }
}
