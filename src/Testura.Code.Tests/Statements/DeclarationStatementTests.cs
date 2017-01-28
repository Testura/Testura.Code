using System;
using NUnit.Framework;
using Testura.Code.Generators.Common;
using Testura.Code.Generators.Common.BinaryExpressions;
using Testura.Code.Models.References;
using Testura.Code.Statements;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Statements
{
    [TestFixture]
    public class DeclarationStatementTests
    {
        private DeclarationStatement _statement;

        [OneTimeSetUp]
        public void SetUp()
        {
            _statement = new DeclarationStatement();
        }


        [Test]
        public void DeclareAndAssign_WhenCreatingVariableWithVar_ShouldUseVar()
        {
            Assert.AreEqual("vartestVariable=1;", _statement.DeclareAndAssign("testVariable", 1).ToString());
        }

        [Test]
        public void DeclareAndAssign_WhenCreatingVariableWithoutVar_ShouldUseType()
        {
            Assert.AreEqual("inttestVariable=1;", _statement.DeclareAndAssign("testVariable", 1, false).ToString());
        }

        [Test]
        public void CreateLocal_WhenCreatingStringVariable_ShouldAddQuotes()
        {
            Assert.AreEqual("vartestVariable=\"hello\";", _statement.DeclareAndAssign("testVariable", "hello").ToString());
        }

        [Test]
        public void DeclareAndAssign_WhenCreateTypeAndAssignToVariableReference_ShouldAssignToVarible()
        {
            Assert.AreEqual("vartest=myVariable;", _statement.DeclareAndAssign("test", typeof(int), new VariableReference("myVariable")).ToString());
        }

        [Test]
        public void DeclareAndAssign_WhenCreateTypeAndAssignToContant_ShouldAssignToVarible()
        {
            Assert.AreEqual("vartest=1;", _statement.DeclareAndAssign("test", typeof(int), new ConstantReference(1)).ToString());
        }

        [Test]
        public void DeclareAndAssign_WhenCreateTypeAndAssignToStringConstant_ShouldAssignToVarible()
        {
            Assert.AreEqual("vartest=\"1\";", _statement.DeclareAndAssign("test", typeof(int), new ConstantReference("1")).ToString());
        }

        [Test]
        public void DeclareAndAssign_WhenCreateTypeAndAssignToStringPathConstant_ShouldAssignToVarible()
        {
            Assert.AreEqual("vartest=@\"1\";", _statement.DeclareAndAssign("test", typeof(int), new ConstantReference("1", StringType.Path)).ToString());
        }

        [Test]
        public void DeclareAndAssign_WhenCreateTypeAndAssignToVariableMember_ShouldAssignToVarible()
        {
            Assert.AreEqual("vartest=var.member;", _statement.DeclareAndAssign("test", typeof(int), new VariableReference("var", new MemberReference("member"))).ToString());
        }

        [Test]
        public void DeclareAndAssign_WhenCreateTypeAndAssignToClassInstance_ShouldAssignToClassInstance()
        {
            Assert.AreEqual("vartest=newList();", _statement.DeclareAndAssign("test", typeof(List), ArgumentGenerator.Create()).ToString());
        }

        [Test]
        public void DeclareAndAssign_WhenCreateTypeAndAssignToExpression_ShouldAssignToExpression()
        {
            Assert.AreEqual("vartest=MyMethod();", _statement.DeclareAndAssign("test", typeof(int), new ExpressionStatement().Invoke("MyMethod").AsExpression()).ToString());
        }
        [Test]
        public void DeclareAndAssign_WhenCreateTypeAndAssignToExpressionAndNotUsingVarKeyword_ShouldAssignToExpressionWithoutVarKeyword()
        {
            Assert.AreEqual("inttest=MyMethod();", _statement.DeclareAndAssign("test", typeof(int), new ExpressionStatement().Invoke("MyMethod").AsExpression(), useVarKeyword:false).ToString());
        }

        [Test]
        public void DeclareAndAssign_WhenCreateTypeAndAssignToExpressionAndCast_ShouldAssignToExpressionAndCast()
        {
            Assert.AreEqual("vartest=(int)MyMethod();", _statement.DeclareAndAssign("test", typeof(int), new ExpressionStatement().Invoke("MyMethod").AsExpression(), typeof(int)).ToString());
        }

        [Test]
        public void DeclareAndAssign_WhenCreateTypeAndAssignToBinaryExpression_ShouldAssignToExpressionAndCast()
        {
            Assert.AreEqual("vartest=1+2;", _statement.DeclareAndAssign("test", typeof(int), new MathBinaryExpression(new ConstantReference(1), new ConstantReference(2), MathOperators.Add)).ToString());
        }

        [Test]
        public void Assign_WhenAssignVariableToClassInstance_ShouldAssignVariable()
        {
            Assert.AreEqual("test=newList();", _statement.Assign("test", typeof(List), ArgumentGenerator.Create()).ToString());
        }

        [Test]
        public void Assign_WhenAssignVariableToReference_ShouldAssignVariable()
        {
            Assert.AreEqual("test=myVariable;", _statement.Assign("test", new VariableReference("myVariable")).ToString());
        }

        [Test]
        public void Assign_WhenAssignVariableToExpression_ShouldAssignVariable()
        {
            Assert.AreEqual("test=MyMethod();", _statement.Assign("test", new ExpressionStatement().Invoke("MyMethod").AsExpression()).ToString());
        }

        [Test]
        public void Assign_WhenAssignVariableToExpressionAndCast_ShouldAssignVariable()
        {
            Assert.AreEqual("test=(int)MyMethod();", _statement.Assign("test", new ExpressionStatement().Invoke("MyMethod").AsExpression(), typeof(int)).ToString());
        }

        [Test]
        public void Assign_WhenAssignVariableReferenceToVariableReference_ShouldAssignVariable()
        {
            Assert.AreEqual("test.Test=test.Do;", _statement.Assign(new VariableReference("test", new MemberReference("Test")), new VariableReference("test", new MemberReference("Do"))).ToString());
        }
        [Test]
        public void Assign_WhenAssignVariableReferenceThatIsAMethodToVariableReference_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => _statement.Assign(new VariableReference("test", new MethodReference("Test")), new VariableReference("test", new MethodReference("Do"))));
        }

        [Test]
        public void Assign_WhenAssignVariableReferenceToVariableReferenceAndCast_ShouldAssignVariable()
        {
            Assert.AreEqual("test.Test=(int)test.Do;", _statement.Assign(new VariableReference("test", new MemberReference("Test")), new VariableReference("test", new MemberReference("Do")), typeof(int)).ToString());
        }


        [Test]
        public void Assign_WhenAssignVariableReferenceToBinaryExpression_ShouldAssignVariable()
        {
            Assert.AreEqual("test.Test=1+2;", _statement.Assign(new VariableReference("test", new MemberReference("Test")), new MathBinaryExpression(new ConstantReference(1), new ConstantReference(2), MathOperators.Add)).ToString());
        }
    }
}
