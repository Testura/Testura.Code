using System.Collections.Generic;
using NUnit.Framework;
using Testura.Code.Generators.Class;
using Testura.Code.Generators.Common;
using Testura.Code.Models;
using Attribute = Testura.Code.Models.Attribute;

namespace Testura.Code.Tests.Generators.Class
{
    [TestFixture]
    public class ClassTests
    {
        [Test]
        public void Constructor_WhenCreatingConstructor_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("MyClass(){}", ClassGenerator.Constructor("MyClass", BodyGenerator.Create()).ToString());
        }

        [Test]
        public void Constructor_WhenCreatingConstructorWithParameters_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("MyClass(Int32test){}", ClassGenerator.Constructor("MyClass", BodyGenerator.Create(), new List<Parameter> { new Parameter("test", typeof(int))}).ToString());
        }

        [Test]
        public void Constructor_WhenCreatingConstructorWithModifiers_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("publicMyClass(){}", ClassGenerator.Constructor("MyClass", BodyGenerator.Create(), modifiers:new List<Modifiers>() { Modifiers.Public}).ToString());
        }

        [Test]
        public void Constructor_WhenCreatingConstructorWithAttribute_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("[Test]MyClass(){}", ClassGenerator.Constructor("MyClass", BodyGenerator.Create(), attributes:new List<Attribute> { new Attribute("Test")}).ToString());
        }

        [Test]
        public void Constructor_WhenCreatingConstructorWithParamterAndModifierAndAttribute_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("[Test]publicMyClass(Int32i){}",
                ClassGenerator.Constructor("MyClass", BodyGenerator.Create(),
                    new List<Parameter> {new Parameter("i", typeof(int))}, new List<Modifiers>() {Modifiers.Public},
                    new List<Attribute> {new Attribute("Test")}).ToString());
        }

    }
}