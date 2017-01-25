using NUnit.Framework;
using Testura.Code.Generators.Common;
using Testura.Code.Models.References;
using Testura.Code.Statements;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Statements
{
    [TestFixture]
    public class IterationStatementTests
    {
        private IterationStatement _control;

        [OneTimeSetUp]
        public void SetUp()
        {
            _control = new IterationStatement();
        }

        [Test]
        public void For_WhenCreatingForLoopWithStartAndEnd_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("for(inti=1;i<2;i++){}",
                _control.For(1, 2, "i", BodyGenerator.Create()).ToString());
        }

        [Test]
        public void For_WhenCreatingForLoopWithVariableReferences_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("for(inti=0;i<myClass.MyProperty;i++){}",
                _control.For(new ConstantReference(0), new VariableReference("myClass", new MemberReference("MyProperty")), "i", BodyGenerator.Create()).ToString());
        }

        [Test]
        public void For_WhenCreatingForeachLoopWithNamesAndVar_ShouldGenerateCodeWithVar()
        {
            Assert.AreEqual("foreach(variinmyList){}",
                _control.ForEach("i", typeof(int), "myList", BodyGenerator.Create()).ToString());
        }

        [Test]
        public void For_WhenCreatingForeachLoopWithNamesAndNotVar_ShouldGenerateCodeWithType()
        {
            Assert.AreEqual("foreach(intiinmyList){}",
                _control.ForEach("i", typeof(int), "myList", BodyGenerator.Create(), false).ToString());
        }

        [Test]
        public void For_WhenCreatingForeachLoopWithReference_ShouldGenerateCodeWithType()
        {
            Assert.AreEqual("foreach(intiina.MyMethod()){}",
                _control.ForEach("i", typeof(int), new VariableReference("a", new MethodReference("MyMethod")), BodyGenerator.Create(), false).ToString());
        }
    }
}
