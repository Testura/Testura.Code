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
        public void CreateLocal_WhenCreatingVariableWithVar_ShouldUseVar()
        {
            Assert.AreEqual("vartestVariable=1;", Variable.CreateLocal("testVariable", 1).ToString());
        }

        [Test]
        public void CreateLocal_WhenCreatingVariableWithoutVar_ShouldUseType()
        {
            Assert.AreEqual("vartestVariable=1;", Variable.CreateLocal("testVariable", 1).ToString());
        }

        [Test]
        public void CreateLocal_WhenCreatingStringVariable_ShouldAddQuotes()
        {
            Assert.AreEqual("vartestVariable=\"hello\";", Variable.CreateLocal("testVariable", "hello").ToString());
        }

        [Test]
        public void CreateLocal_WhenCreateTypeAndAssignToVariableReference_ShouldAssignToVarible()
        {
            Assert.AreEqual("vartest=myVariable;", Variable.CreateLocal("test", typeof(int), new VariableReference("myVariable")).ToString());
        }

        [Test]
        public void CreateLocal_WhenCreateTypeAndAssignToVariableContant_ShouldAssignToVarible()
        {
            Assert.AreEqual("vartest=1;", Variable.CreateLocal("test", typeof(int), new ConstantReference(1)).ToString());
        }

        [Test]
        public void CreateLocal_WhenCreateTypeAndAssignToVariableMember_ShouldAssignToVarible()
        {
            Assert.AreEqual("vartest=var.member;", Variable.CreateLocal("test", typeof(int), new VariableReference("var", new MemberReference("member", MemberReferenceTypes.Field))).ToString());
        }

        [Test]
        public void CreateLocal_WhenCreateTypeAndAssignToClassInstance_ShouldAssignToClassInstance()
        {
            Assert.AreEqual("vartest=newString();", Variable.CreateLocal("test", typeof(String), Argument.Create(new List<IArgument>())).ToString());
        }

        [Test]
        public void Assign_WhenAssignVariableToClassInstance_ShouldAssignVariable()
        {
            Assert.AreEqual("test=newString();", Variable.Assign("test", typeof(String), Argument.Create(new List<IArgument>())).ToString());
        }
    }
}
