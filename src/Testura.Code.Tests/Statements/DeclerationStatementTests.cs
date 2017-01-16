using System;
using NUnit.Framework;
using Testura.Code.Generators.Common;
using Testura.Code.Models.References;
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
        public void DeclareAndAssign_WhenCreatingVariableWithVar_ShouldUseVar()
        {
            Assert.AreEqual("vartestVariable=1;", statement.DeclareAndAssign("testVariable", 1).ToString());
        }

        [Test]
        public void DeclareAndAssign_WhenCreatingVariableWithoutVar_ShouldUseType()
        {
            Assert.AreEqual("vartestVariable=1;", statement.DeclareAndAssign("testVariable", 1).ToString());
        }

        [Test]
        public void CreateLocal_WhenCreatingStringVariable_ShouldAddQuotes()
        {
            Assert.AreEqual("vartestVariable=\"hello\";", statement.DeclareAndAssign("testVariable", "hello").ToString());
        }

        [Test]
        public void DeclareAndAssign_WhenCreateTypeAndAssignToVariableReference_ShouldAssignToVarible()
        {
            Assert.AreEqual("vartest=myVariable;", statement.DeclareAndAssign("test", typeof(int), new VariableReference("myVariable")).ToString());
        }

        [Test]
        public void DeclareAndAssign_WhenCreateTypeAndAssignToContant_ShouldAssignToVarible()
        {
            Assert.AreEqual("vartest=1;", statement.DeclareAndAssign("test", typeof(int), new ConstantReference(1)).ToString());
        }

        [Test]
        public void DeclareAndAssign_WhenCreateTypeAndAssignToStringConstant_ShouldAssignToVarible()
        {
            Assert.AreEqual("vartest=\"1\";", statement.DeclareAndAssign("test", typeof(int), new ConstantReference("1")).ToString());
        }

        [Test]
        public void DeclareAndAssign_WhenCreateTypeAndAssignToStringPathConstant_ShouldAssignToVarible()
        {
            Assert.AreEqual("vartest=@\"1\";", statement.DeclareAndAssign("test", typeof(int), new ConstantReference("1", StringType.Path)).ToString());
        }

        [Test]
        public void DeclareAndAssign_WhenCreateTypeAndAssignToVariableMember_ShouldAssignToVarible()
        {
            Assert.AreEqual("vartest=var.member;", statement.DeclareAndAssign("test", typeof(int), new VariableReference("var", new MemberReference("member"))).ToString());
        }

        [Test]
        public void DeclareAndAssign_WhenCreateTypeAndAssignToClassInstance_ShouldAssignToClassInstance()
        {
            Assert.AreEqual("vartest=newString();", statement.DeclareAndAssign("test", typeof(String), ArgumentGenerator.Create()).ToString());
        }

        [Test]
        public void DeclareAndAssign_WhenCreateTypeAndAssignToExpression_ShouldAssignToExpression()
        {
            Assert.AreEqual("vartest=MyMethod();", statement.DeclareAndAssign("test", typeof(int), new ExpressionStatement().Invoke("MyMethod").AsExpression()).ToString());
        }
        [Test]
        public void DeclareAndAssign_WhenCreateTypeAndAssignToExpressionAndNotUsingVarKeyword_ShouldAssignToExpressionWithoutVarKeyword()
        {
            Assert.AreEqual("inttest=MyMethod();", statement.DeclareAndAssign("test", typeof(int), new ExpressionStatement().Invoke("MyMethod").AsExpression(), useVarKeyword:false).ToString());
        }

        [Test]
        public void DeclareAndAssign_WhenCreateTypeAndAssignToExpressionAndCast_ShouldAssignToExpressionAndCast()
        {
            Assert.AreEqual("vartest=(int)MyMethod();", statement.DeclareAndAssign("test", typeof(int), new ExpressionStatement().Invoke("MyMethod").AsExpression(), typeof(int)).ToString());
        }

        [Test]
        public void Assign_WhenAssignVariableToClassInstance_ShouldAssignVariable()
        {
            Assert.AreEqual("test=newString();", statement.Assign("test", typeof(String), ArgumentGenerator.Create()).ToString());
        }

        [Test]
        public void Assign_WhenAssignVariableToReference_ShouldAssignVariable()
        {
            Assert.AreEqual("test=myVariable;", statement.Assign("test", new VariableReference("myVariable")).ToString());
        }

        [Test]
        public void Assign_WhenAssignVariableToExpression_ShouldAssignVariable()
        {
            Assert.AreEqual("test=MyMethod();", statement.Assign("test", new ExpressionStatement().Invoke("MyMethod").AsExpression()).ToString());
        }

        [Test]
        public void Assign_WhenAssignVariableToExpressionAndCast_ShouldAssignVariable()
        {
            Assert.AreEqual("test=(int)MyMethod();", statement.Assign("test", new ExpressionStatement().Invoke("MyMethod").AsExpression(), typeof(int)).ToString());
        }

        [Test]
        public void Assign_WhenAssignVariableReferenceToVariableReference_ShouldAssignVariable()
        {
            Assert.AreEqual("test.Test=test.Do;", statement.Assign(new VariableReference("test", new MemberReference("Test")), new VariableReference("test", new MemberReference("Do"))).ToString());
        }
        [Test]
        public void Assign_WhenAssignVariableReferenceThatIsAMethodToVariableReference_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => statement.Assign(new VariableReference("test", new MethodReference("Test")), new VariableReference("test", new MethodReference("Do"))));
        }

        [Test]
        public void Assign_WhenAssignVariableReferenceToVariableReferenceAndCast_ShouldAssignVariable()
        {
            Assert.AreEqual("test.Test=(int)test.Do;", statement.Assign(new VariableReference("test", new MemberReference("Test")), new VariableReference("test", new MemberReference("Do")), typeof(int)).ToString());
        }
    }
}
