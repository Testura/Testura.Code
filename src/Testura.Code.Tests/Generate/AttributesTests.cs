using NUnit.Framework;
using Testura.Code.Helper;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Generate
{
    [TestFixture]
    public class AttributesTests
    {
        [Test]
        public void Create_WhenNotProvidingAnyAttributes_ShouldGetEmptyString()
        {
            Assert.AreEqual(string.Empty, Attributes.Create().ToString());
        }

        [Test]
        public void Create_WhenCreatingWithSingleAttrbute_ShoulGenerateCorrectCode()
        {
            Assert.AreEqual("[Test]", Attributes.Create(new Attribute("Test")).ToString());
        }

        [Test]
        public void Create_WhenCreatingWithMultipleAttributes_ShoulGenerateCorrectCode()
        {
            Assert.AreEqual("[Test][TestCase]", Attributes.Create(new Attribute("Test"), new Attribute("TestCase")).ToString());
        }

    }
}
