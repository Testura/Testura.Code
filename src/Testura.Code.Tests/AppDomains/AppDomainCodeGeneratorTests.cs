using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using NUnit.Framework;
using Testura.Code.Builders;
using Testura.Code.Compilations;
using Testura.Code.Saver;
using Testura.Code.Util.AppDomains;

namespace Testura.Code.Tests.AppDomains
{
    public class AppDomainCodeGeneratorTests
    {
        private Compiler _compiler;
        private AppDomainCodeGenerator _appDomainCodeGenerator;

        [SetUp]
        public void SetUp()
        {
            _compiler = new Compiler();
            _appDomainCodeGenerator = new AppDomainCodeGenerator();
        }

        [Test]
        public async Task GenerateCode_WhenLoadingAssemblyInExternalDomain_ShouldUnload()
        {
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "test.dll");
            await _compiler.CompileSourceAsync(path, new ClassBuilder("TestClass", "Test").Build().NormalizeWhitespace().ToString());

            _appDomainCodeGenerator.GenerateCode(path, (assembly, extraData) =>
            {
                var types = assembly.GetExportedTypes();
                Assert.AreEqual(1, types.Length);
            });

            var currentAssemblies = GetCurrentAssemblies(AppDomain.CurrentDomain);
            Assert.IsFalse(currentAssemblies.Any(c => c.ToString().Contains("test")));
        }

        private List<string> GetCurrentAssemblies(AppDomain appDomain)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            //Provide the current application domain evidence for the assembly.
            Evidence asEvidence = currentDomain.Evidence;
            //Load the assembly from the application directory using a simple name. 

            //Make an array for the list of assemblies.
            Assembly[] assems = currentDomain.GetAssemblies();

            return assems.Select(a => a.ToString()).ToList();
        }
    }
}
