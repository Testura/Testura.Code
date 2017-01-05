using System;
using System.Collections.Generic;
using NUnit.Framework;
using Testura.Code.Generate;
using Testura.Code.Generate.ArgumentTypes;
using Testura.Code.Reference;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Generate
{
    [TestFixture]
    public class VariableTests
    {

        [Test]
        public void CreateLocalVariable_WhenCreatingVariableWithVar_ShouldUseVar()
        {
            Assert.AreEqual("vartestVariable=1;", Variable.CreateLocalVariable("testVariable", 1).ToString());
        }

        [Test]
        public void CreateLocalVariable_WhenCreatingVariableWithoutVar_ShouldUseType()
        {
            Assert.AreEqual("vartestVariable=1;", Variable.CreateLocalVariable("testVariable", 1).ToString());
        }

        [Test]
        public void CreateLocalVariable_WhenCreatingStringVariable_ShouldAddQuotes()
        {
            Assert.AreEqual("vartestVariable=\"hello\";", Variable.CreateLocalVariable("testVariable", "hello").ToString());
        }

        [Test]
        public void CreateLocalVariable_WhenCreateTypeAndAssignToVariableReference_ShouldAssignToVarible()
        {
            Assert.AreEqual("vartest=myVariable;", Variable.CreateLocalVariable("test", typeof(int), new VariableReference("myVariable", typeof(int))).ToString());
        }

        [Test]
        public void CreateLocalVariable_WhenCreateTypeAndAssignToVariableContant_ShouldAssignToVarible()
        {
            Assert.AreEqual("vartest=1;", Variable.CreateLocalVariable("test", typeof(int), new ConstantReference(1)).ToString());
        }

        [Test]
        public void CreateLocalVariable_WhenCreateTypeAndAssignToVariableMember_ShouldAssignToVarible()
        {
            Assert.AreEqual("vartest=var.member;", Variable.CreateLocalVariable("test", typeof(int), new VariableReference("var", typeof(int), new MemberReference("member", typeof(int), typeof(int), MemberReferenceTypes.Field))).ToString());
        }

        [Test]
        public void CreateLocalVariable_WhenCreateTypeAndAssignToClassInstance_ShouldAssignToClassInstance()
        {
            Assert.AreEqual("vartest=newString();", Variable.CreateLocalVariable("test", typeof(String), Argument.Create(new List<IArgument>())).ToString());
        }

        [Test]
        public void AssignVariable_WhenAssignVariableToClassInstance_ShouldAssignVariable()
        {
            Assert.AreEqual("test=newString();", Variable.AssignVariable("test", typeof(String), Argument.Create(new List<IArgument>())).ToString());
        }
    }
}
