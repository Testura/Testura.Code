using NUnit.Framework;
using Testura.Code.Helper;
using Testura.Code.Helper.References;
using Testura.Code.Statements;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Statements
{
    [TestFixture]
    public class IterationStatementTests
    {
        private IterationStatement control;

        [OneTimeSetUp]
        public void SetUp()
        {
            control = new IterationStatement();
        }

        [Test]
        public void For_WhenCreatingForLoopWithStartAndEnd_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("for(inti=1;i<2;i++){}",
                control.For(1, 2, "i", Body.Create()).ToString());
        }

        [Test]
        public void For_WhenCreatingForLoopWithVariableReferences_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("for(inti=0;i<myClass.MyProperty;i++){}",
                control.For(new ConstantReference(0), new VariableReference("myClass", new MemberReference("MyProperty", MemberReferenceTypes.Property)), "i", Body.Create()).ToString());
        }
    }
}
