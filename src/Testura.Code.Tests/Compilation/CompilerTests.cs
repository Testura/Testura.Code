using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using NUnit.Framework;
using Testura.Code.Builders;
using Testura.Code.Compilations;

namespace Testura.Code.Tests.Compilation
{
    [TestFixture]
    public class CompilerTests
    {
        private Compiler _compiler;

        [OneTimeSetUp]
        public void SetUp()
        {
            _compiler = new Compiler(null);
        }

        [Test]
        public async Task CompileSourceAsync_WhenCompilingSource_ShouldGetADll()
        {
            var result = await _compiler.CompileSourceAsync(Path.Combine(TestContext.CurrentContext.TestDirectory, "test.dll"), new ClassBuilder("TestClass", "Test").Build().NormalizeWhitespace().ToString());
            Assert.IsNotNull(result.PathToDll);
            Assert.AreEqual(0, result.OutputRows.Count);
            Assert.IsTrue(result.Success);
        }

        [Test]
        public async Task CompileSourceAsync_WhenCompilingSourceWithError_ShouldGetListContainingErrors()
        {
            var result = await _compiler.CompileSourceAsync(Path.Combine(TestContext.CurrentContext.TestDirectory, "test.dll"), "gfdgdfgfdg");
            Assert.AreEqual(1, result.OutputRows.Count);
            Assert.IsFalse(result.Success);
        }
    }
}
