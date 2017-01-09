using System;
using System.Collections.Generic;
using NUnit.Framework;
using Testura.Code.Helpers.Arguments;
using Testura.Code.Helpers.Arguments.ArgumentTypes;
using Testura.Code.Helpers.References;
using Testura.Code.Statements;

namespace Testura.Code.Tests.Statements
{
    [TestFixture]
    public class ExpressionStatementTests
    {
        private ExpressionStatement expressionStatement;


        [OneTimeSetUp]
        public void SetUp()
        {
            expressionStatement = new ExpressionStatement();
        }

        [Test]
        public void Invoke_WhenUsingSimpleNames_ShouldGenerateCorrectCode()
        {
           var invocation = expressionStatement.Invoke("myClass", "Do", Argument.Create());
           Assert.IsNotNull(invocation);
            Assert.AreEqual("myClass.Do();", invocation.AsExpressionStatement().ToString());
        }

        [Test]
        public void Invoke_WhenUsingSimpleNamesWithArguments_ShouldGenerateCorrectCode()
        {
            var invocation = expressionStatement.Invoke("myClass", "Do", Argument.Create(new ValueArgument(1)));
            Assert.IsNotNull(invocation);
            Assert.AreEqual("myClass.Do(1);", invocation.AsExpressionStatement().ToString());
        }

        [Test]
        public void Invoke_WhenUsingReferenceThatisNotMethod_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => expressionStatement.Invoke(new VariableReference("test")));
        }

        [Test]
        public void Invoke_WhenUsingMethodReference_ShouldGenerateCorrectCode()
        {
            var invocation = expressionStatement.Invoke(new VariableReference("myClass", new MethodReference("Do", new List<IArgument>())));
            Assert.IsNotNull(invocation);
            Assert.AreEqual("myClass.Do();", invocation.AsExpressionStatement().ToString());
        }
    }
}