﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;
using Testura.Code.Models.References;
using Testura.Code.Statements;

namespace Testura.Code.Tests.Generators.Common.Arguments.ArgumentTypes
{
    [TestFixture]
    public class InvocationArgumentTests
    {
        [Test]
        public void GetArgumentSyntax_WhenUsingMethod_ShouldGetCode()
        {
            var argument =
                new InvocationArgument(Statement.Expression.Invoke(new MethodReference("Do")).AsExpression());
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("Do()", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenUsingMethodAsNamedArgument_ShouldGetCode()
        {
            var argument =
                new InvocationArgument(Statement.Expression.Invoke(new MethodReference("Do")).AsExpression(), namedArgument:"namedArgument");
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("namedArgument:Do()", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenUsingReference_ShouldGetCode()
        {
            var argument =
                new InvocationArgument(new MethodReference("Do"));
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("Do()", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenUsingMethodAndCasting_ShouldGetCode()
        {
            var argument =
                new InvocationArgument(Statement.Expression.Invoke(new MethodReference("Do")).AsExpression(), typeof(int));
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("(int)Do()", syntax.ToString());
        }
    }
}
