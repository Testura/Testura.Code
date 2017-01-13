using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;
using Testura.Code.Models;
using Testura.Code.Models.References;

namespace Testura.Code.Statements
{
    public class ExpressionStatement
    {
        /// <summary>
        /// Create code to invoke a method on class
        /// </summary>
        /// <param name="variableName">Decleration to invoke</param>
        /// <param name="methodName">The method we want to call</param>
        /// <param name="arguments">Arguments in the method</param>
        /// <param name="genericTypes">Optional list of types if the method is generic</param>
        /// <returns>A statement syntax</returns>
        public Invocation Invoke(string variableName, string methodName, IEnumerable<IArgument> arguments = null, IEnumerable<Type> generics = null)
        {
            return Invoke(new VariableReference(variableName, new MethodReference(methodName, arguments, generics)));
        }

        public Invocation Invoke(string methodName, IEnumerable<IArgument> arguments = null, IEnumerable<Type> generics = null)
        {
            return Invoke(new MethodReference(methodName, arguments, generics));
        }

        public Invocation Invoke(VariableReference reference)
        {
            if (!(reference is MethodReference))
            {
                VariableReference child = reference.Member;
                while (child?.Member != null)
                {
                    child = child.Member;
                }

                if (!(child is MethodReference))
                {
                    throw new ArgumentException(nameof(reference), "Must be a method reference");
                }

            }

            return new Invocation((InvocationExpressionSyntax)ReferenceGenerator.Create(reference));
        }
    }
}
