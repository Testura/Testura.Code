using NUnit.Framework;
using Testura.Code.Generators.Common;
using Testura.Code.Models;

namespace Testura.Code.Tests.Generators.Common
{
    [TestFixture]
    public class ParameterGeneratorTests
    {
        [Test]
        public void Create_WhenNotProvidingAnyParameters_ShouldGetEmptyBraces()
        {
            Assert.AreEqual("()", ParameterGenerator.Create().ToString());
        }

        [Test]
        public void Create_WhenProvidingParameterWithNoneModifier_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("(inttest)", ParameterGenerator.Create(new Parameter("test", typeof(int))).ToString());
        }

        [Test]
        public void Create_WhenProvidingParameterWithOutModifier_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("(outinttest)", ParameterGenerator.Create(new Parameter("test", typeof(int), ParameterModifiers.Out)).ToString());
        }

        [Test]
        public void Create_WhenProvidingParameterWithRefModifier_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("(refinttest)", ParameterGenerator.Create(new Parameter("test", typeof(int), ParameterModifiers.Ref)).ToString());
        }

        [Test]
        public void Create_WhenProvidingParameterWithThisModifier_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("(thisinttest)", ParameterGenerator.Create(new Parameter("test", typeof(int), ParameterModifiers.This)).ToString());
        }
    }
}
