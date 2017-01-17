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
        /// Create the expression statement syntax to invoke a method on variable
        /// </summary>
        /// <param name="variableName">The variable name</param>
        /// <param name="methodName">The method we want to call</param>
        /// <param name="arguments">Arguments that we end to the method</param>
        /// <param name="generics">Optional list of types if the method is generic</param>
        /// <returns>A invocation object with both statement and expression</returns>
        public Invocation Invoke(string variableName, string methodName, IEnumerable<IArgument> arguments = null, IEnumerable<Type> generics = null)
        {
            return Invoke(new VariableReference(variableName, new MethodReference(methodName, arguments, generics)));
        }

        /// <summary>
        /// Create the expression statement syntax to invoke a method
        /// </summary>
        /// <param name="methodName">The method we want to call</param>
        /// <param name="arguments">Arguments that we end to the method</param>
        /// <param name="generics">Optional list of types if the method is generic</param>
        /// <returns>A invocation object with both statement and expression</returns>
        public Invocation Invoke(string methodName, IEnumerable<IArgument> arguments = null, IEnumerable<Type> generics = null)
        {
            return Invoke(new MethodReference(methodName, arguments, generics));
        }

        /// <summary>
        /// Create the expression statement syntax to invoke a method in a chain (for exemple myVariable.myProperty.MyMethod())
        /// </summary>
        /// <param name="reference">The reference chain</param>
        /// <returns>A invocation object with both statement and expression</returns>
        public Invocation Invoke(VariableReference reference)
        {
            if (!(reference is MethodReference))
            {
                var member = reference.GetLastMember();
                if (!(member is MethodReference))
                {
                    throw new ArgumentException($"{nameof(reference)} or last member in chain must be a method reference");
                }
            }

            return new Invocation((InvocationExpressionSyntax)ReferenceGenerator.Create(reference));
        }
    }
}
