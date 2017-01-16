using NUnit.Framework;
using Testura.Code.Models.References;
using Testura.Code.Statements;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Statements
{
    [TestFixture]
    public class JumpStatementTests
    {
        private JumpStatement @return;

        [SetUp]
        public void SetUp()
        {
            @return = new JumpStatement();
        }

        [Test]
        public void ReturnTrue_WhenReturnTrue_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("returntrue;", @return.ReturnTrue().ToString());
        }

        [Test]
        public void ReturnFalse_WhenReturnFalse_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("returnfalse;", @return.ReturnFalse().ToString());
        }

        [Test]
        public void Return_WhenReturnReference_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("returni;", @return.Return(new VariableReference("i")).ToString());
        }

        [Test]
        public void Return_WhenReturnExpression_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("returntest();", @return.Return(Statement.Expression.Invoke("test").AsExpression()).ToString());
        }
    }
}
