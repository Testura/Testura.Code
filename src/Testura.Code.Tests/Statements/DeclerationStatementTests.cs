using System;
using NUnit.Framework;
using Testura.Code.Helpers.Common.Arguments;
using Testura.Code.Helpers.Common.References;
using Testura.Code.Statements;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Statements
{
    [TestFixture]
    public class DeclerationStatementTests
    {
        private DeclerationStatement statement;

        [OneTimeSetUp]
        public void SetUp()
        {
            statement = new DeclerationStatement();
        }


        [Test]
        public void CreateLocal_WhenCreatingVariableWithVar_ShouldUseVar()
        {
            Assert.AreEqual("vartestVariable=1;", statement.DeclareAndAssign("testVariable", 1).ToString());
        }

        [Test]
        public void CreateLocal_WhenCreatingVariableWithoutVar_ShouldUseType()
        {
            Assert.AreEqual("vartestVariable=1;", statement.DeclareAndAssign("testVariable", 1).ToString());
        }

        [Test]
        public void CreateLocal_WhenCreatingStringVariable_ShouldAddQuotes()
        {
            Assert.AreEqual("vartestVariable=\"hello\";", statement.DeclareAndAssign("testVariable", "hello").ToString());
        }

        [Test]
        public void CreateLocal_WhenCreateTypeAndAssignToVariableReference_ShouldAssignToVarible()
        {
            Assert.AreEqual("vartest=myVariable;", statement.DeclareAndAssign("test", typeof(int), new VariableReference("myVariable")).ToString());
        }

        [Test]
        public void CreateLocal_WhenCreateTypeAndAssignToVariableContant_ShouldAssignToVarible()
        {
            Assert.AreEqual("vartest=1;", statement.DeclareAndAssign("test", typeof(int), new ConstantReference(1)).ToString());
        }

        [Test]
        public void CreateLocal_WhenCreateTypeAndAssignToVariableMember_ShouldAssignToVarible()
        {
            Assert.AreEqual("vartest=var.member;", statement.DeclareAndAssign("test", typeof(int), new VariableReference("var", new MemberReference("member"))).ToString());
        }

        [Test]
        public void CreateLocal_WhenCreateTypeAndAssignToClassInstance_ShouldAssignToClassInstance()
        {
            Assert.AreEqual("vartest=newString();", statement.DeclareAndAssign("test", typeof(String), Argument.Create()).ToString());
        }

        [Test]
        public void Assign_WhenAssignVariableToClassInstance_ShouldAssignVariable()
        {
            Assert.AreEqual("test=newString();", statement.Assign("test", typeof(String), Argument.Create()).ToString());
        }
    }
}
