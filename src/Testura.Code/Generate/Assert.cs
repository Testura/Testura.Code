using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generate.ArgumentTypes;

namespace Testura.Code.Generate
{
    public static class Assert
    {
        /// <summary>
        /// Create an AreEqual assert
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ExpressionStatementSyntax AreEqual(IArgument expected, IArgument actual, string message)
        {
            return Are(AssertType.AreEqual, expected, actual, message);
        }

        /// <summary>
        /// Create an AreNotEqual assert
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ExpressionStatementSyntax AreNotEqual(IArgument expected, IArgument actual, string message)
        {
            return Are(AssertType.AreNotEqual, expected, actual, message);
        }

        /// <summary>
        /// Create an IsTrue assert
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ExpressionStatementSyntax IsTrue(IArgument actual, string message)
        {
            return Is(true, actual, message);
        }

        /// <summary>
        /// Create an IsFalse assert
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ExpressionStatementSyntax IsFalse(IArgument actual, string message)
        {
            return Is(false, actual, message);
        }

        /// <summary>
        /// Create an IsTrue assert that check if a string contains contains some content
        /// </summary>
        /// <param name="expectedContain"></param>
        /// <param name="actual"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ExpressionStatementSyntax Contains(IArgument expectedContain, IArgument actual, string message)
        {
            return Method.Invoke("Assert", "IsTrue", Argument.Create(
                new InvocationArgument(Method.Invoke(actual.GetArgumentSyntax().ToString(), "Contains", Argument.Create(expectedContain)).AsInvocationStatment()),
                new ValueArgument(message, ArgumentType.String)
                )).AsExpressionStatement();
        }

        private static ExpressionStatementSyntax Are(AssertType assertType, IArgument expected, IArgument actual, string message)
        {
            return
                Method.Invoke("Assert", Enum.GetName(typeof(AssertType), assertType),
                    Argument.Create(
                        expected,
                        actual,
                        new ValueArgument(message, ArgumentType.String)
                        )).AsExpressionStatement();
        }

        private static ExpressionStatementSyntax Is(bool exected, IArgument actual, string message)
        {
            return Method.Invoke("Assert", exected ? "IsTrue" : "IsFalse", Argument.Create(
                new ValueArgument(exected),
                actual,
                new ValueArgument(message, ArgumentType.String)
                )).AsExpressionStatement();
        }


    }
}
