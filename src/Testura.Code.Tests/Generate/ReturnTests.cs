using NUnit.Framework;
using Testura.Code.Statements;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Generate
{
    [TestFixture]
    public class ReturnTests
    {
        private JumpStatement @return;

        [SetUp]
        public void SetUp()
        {
            @return = new JumpStatement();
        }

        [Test]
        public void True_WhenReturnTrue_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("returntrue;", @return.True().ToString());
        }

        [Test]
        public void True_WhenReturnFalse_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("returnfalse;", @return.False().ToString());
        }
    }
}
