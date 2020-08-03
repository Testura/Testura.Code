using System.IO;
using System.Reflection;
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
            var outputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "test01.dll");
            var result = await _compiler.CompileSourceAsync(outputPath, new ClassBuilder("TestClass", "Test").Build().NormalizeWhitespace().ToString());
            Assert.AreEqual(0, result.OutputRows.Count);
            Assert.IsTrue(result.Success);
            Assembly.LoadFrom(outputPath);
        }

        [Test]
        public async Task CompileSourceAsync_WhenCompilingSourceWithError_ShouldGetListContainingErrors()
        {
            var result = await _compiler.CompileSourceAsync(Path.Combine(TestContext.CurrentContext.TestDirectory, "test02.dll"), "gfdgdfgfdg");
            Assert.AreEqual(1, result.OutputRows.Count);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public async Task CompileSourceInMemoryAsync_WhenCompilingSource_ShouldGetEmptyResultList()
        {
            var result = await _compiler.CompileSourceInMemoryAsync(new ClassBuilder("TestClass", "Test").Build().NormalizeWhitespace().ToString());
            Assert.AreEqual(0, result.OutputRows.Count);
            Assert.IsTrue(result.Success);
        }

        [Test]
        public async Task CompileSourceInMemoryAsync_WhenCompilingSourceWithError_ShouldGetListContainingErrors()
        {
            var result = await _compiler.CompileSourceInMemoryAsync("gfdgdfgfdg");
            Assert.AreEqual(1, result.OutputRows.Count);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public async Task CompileSourceToStreamAsync_WhenCompilingSource_ShouldHaveAProperlyLoadingAssembly()
        {
            using (var ms = new MemoryStream())
            {
                var result = await _compiler.CompileSourceToStreamAsync("test", ms, new ClassBuilder("TestClass", "Test").Build().NormalizeWhitespace().ToString());
                var assemblyBytes = ms.ToArray();
                Assert.IsTrue(result.Success);
                Assert.NotZero(assemblyBytes.Length);
                Assembly.Load(assemblyBytes);
            }
        }
    }
}
