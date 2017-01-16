using NUnit.Framework;
using Testura.Code.Builders;
using Testura.Code.Saver;

namespace Testura.Code.Tests.Saver
{
    [TestFixture]
    public class CodeSaverTests
    {
        private CodeSaver _coderSaver;

        [OneTimeSetUp]
        public void SetUp()
        {
            _coderSaver = new CodeSaver();
        }

        [Test]
        public void SaveCodeAsString_WhenSavingCodeAsString_ShouldGetString()
        {
            var code = _coderSaver.SaveCodeAsString(new ClassBuilder("TestClass", "test").Build());
            Assert.IsNotNull(code);
            Assert.AreEqual("namespace test\r\n{\r\n    public class TestClass\r\n    {\r\n    }\r\n}", code);
        }
    }
}