using NUnit.Framework;
using Testura.Code.Helpers;
using Testura.Code.Helpers.Arguments.ArgumentTypes;
using Testura.Code.Statements;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Statements
{
    [TestFixture]
    public class SelectionStatementTests
    {
        private SelectionStatement conditional;

        [OneTimeSetUp]
        public void SetUp()
        {
            conditional = new SelectionStatement();
        }

        [Test]
        public void If_WhenCreatingAnIfWithEqual_ShouldGenerateCorrectIfStatement()
        {
            Assert.AreEqual("if(2==3){}",
                conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatements.Equal, Body.Create()).ToString());
        }

        [Test]
        public void If_WhenCreatingAnIfWithNotEqual_ShouldGenerateCorrectIfStatement()
        {
            Assert.AreEqual("if(2!=3){}",
                conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatements.NotEqual, Body.Create()).ToString());
        }

        [Test]
        public void If_WhenCreatingAnIfWithGreaterThan_ShouldGenerateCorrectIfStatement()
        {
            Assert.AreEqual("if(2>3){}",
                conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatements.GreaterThan, Body.Create()).ToString());
        }

        [Test]
        public void If_WhenCreatingAnIfWithGreaterThanOrEqual_ShouldGenerateCorrectIfStatement()
        {
            Assert.AreEqual("if(2>=3){}",
                conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatements.GreaterThanOrEqual, Body.Create()).ToString());
        }

        [Test]
        public void If_WhenCreatingAnIfWithLessThan_ShouldGenerateCorrectIfStatement()
        {
            Assert.AreEqual("if(2<3){}",
                conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatements.LessThan, Body.Create()).ToString());
        }

        [Test]
        public void If_WhenCreatingAnIfWithLessThanOrEqual_ShouldGenerateCorrectIfStatement()
        {
            Assert.AreEqual("if(2<=3){}",
                conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatements.LessThanOrEqual, Body.Create()).ToString());
        }
    }
}
