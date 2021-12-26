using NUnit.Framework;
using Testura.Code.Extensions.Naming;

namespace Testura.Code.Tests.Extensions.Naming
{
    [TestFixture]
    public class StringNamingExtensionsTests
    {
        [Test]
        public void FirstLetterToLowerCase_WhenHavingAString_ShouldSetLFirstLetterToLowerCase()
        {
            Assert.AreEqual("test", "Test".FirstLetterToLowerCase());
        }

        [Test]
        public void FirstLetterToLowerCase_WhenHavingAEmptyString_ShouldReturnSameString()
        {
            Assert.AreEqual(string.Empty, string.Empty.FirstLetterToLowerCase());
        }

        [Test]
        public void FirstLetterToUpperCase_WhenHavingAString_ShouldSetLFirstLetterToUpperCase()
        {
            Assert.AreEqual("Test", "test".FirstLetterToUpperCase());
        }

        [Test]
        public void FirstLetterToUpperCase_WhenHavingAEmptyString_ShouldReturnSameString()
        {
            Assert.AreEqual(string.Empty, string.Empty.FirstLetterToUpperCase());
        }
    }
}
