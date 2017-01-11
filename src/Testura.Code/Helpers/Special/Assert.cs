﻿using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Helpers.Common.Arguments;
using Testura.Code.Helpers.Common.Arguments.ArgumentTypes;
using Testura.Code.Statements;

namespace Testura.Code.Helpers.Special
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
            return Statement.Expression.Invoke("Assert", "IsTrue", new List<IArgument>
            { 
                new InvocationArgument(Statement.Expression.Invoke(actual.GetArgumentSyntax().ToString(), "Contains", new List<IArgument> { expectedContain }).AsInvocationStatment()),
                new ValueArgument(message)
            }).AsExpressionStatement();
        }

        private static ExpressionStatementSyntax Are(AssertType assertType, IArgument expected, IArgument actual, string message)
        {
            return
                Statement.Expression.Invoke("Assert", Enum.GetName(typeof(AssertType), assertType),
                    new List<IArgument> { 
                        expected,
                        actual,
                        new ValueArgument(message)
                        }).AsExpressionStatement();
        }

        private static ExpressionStatementSyntax Is(bool exected, IArgument actual, string message)
        {
            return Statement.Expression.Invoke("Assert", exected ? "IsTrue" : "IsFalse", new List<IArgument>
            { 
                new ValueArgument(exected),
                actual,
                new ValueArgument(message)
            }).AsExpressionStatement();
        }


    }
}
