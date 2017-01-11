using System;
using System.Collections.Generic;
using NUnit.Framework;
using Testura.Code.Helpers.Common.Arguments;
using Testura.Code.Helpers.Common.Arguments.ArgumentTypes;
using Testura.Code.Helpers.Common.References;
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
           var invocation = expressionStatement.Invoke("myClass", "Do");
           Assert.IsNotNull(invocation);
            Assert.AreEqual("myClass.Do();", invocation.AsExpressionStatement().ToString());
        }

        [Test]
        public void Invoke_WhenUsingSimpleNamesWithArguments_ShouldGenerateCorrectCode()
        {
            var invocation = expressionStatement.Invoke("myClass", "Do", new List<IArgument> { new ValueArgument(1) });
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

        [Test]
        public void Invoke_WhenUsingGenericMethodReference_ShouldGenerateCorrectCode()
        {
            var invocation = expressionStatement.Invoke(new VariableReference("myClass", new MethodReference("Do", new List<IArgument>(), new List<Type>() { typeof(int)})));
            Assert.IsNotNull(invocation);
            Assert.AreEqual("myClass.Do<System.Int32>();", invocation.AsExpressionStatement().ToString());
        }
    }
}