using System.Collections.Generic;
using NUnit.Framework;
using Testura.Code.Generators.Class;
using Testura.Code.Generators.Common;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;
using Testura.Code.Models;
using Attribute = Testura.Code.Models.Attribute;

namespace Testura.Code.Tests.Generators.Class
{
    [TestFixture]
    public class ConstructorGeneratorTests
    {
        [Test]
        public void Constructor_WhenCreatingConstructor_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("MyClass(){}", ConstructorGenerator.Create("MyClass", BodyGenerator.Create()).ToString());
        }

        [Test]
        public void Constructor_WhenCreatingConstructorWithThisInitializer_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("MyClass():this(){}", ConstructorGenerator.Create("MyClass", BodyGenerator.Create(), constructorInitializer: new ConstructorInitializer(ConstructorInitializerTypes.This, null)).ToString());
        }

        [Test]
        public void Constructor_WhenCreatingConstructorWithBaseInitializer_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("MyClass():base(){}", ConstructorGenerator.Create("MyClass", BodyGenerator.Create(), constructorInitializer: new ConstructorInitializer(ConstructorInitializerTypes.Base, null)).ToString());
        }
        
        [Test]
        public void Constructor_WhenCreatingConstructorWithBaseInitializerWithArgument_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("MyClass():base(\"myText\"){}", ConstructorGenerator.Create("MyClass", BodyGenerator.Create(), constructorInitializer: new ConstructorInitializer(ConstructorInitializerTypes.Base, new List<Argument> { new ValueArgument("myText")})).ToString());
        }

        [Test]
        public void Constructor_WhenCreatingConstructorWithParameters_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("MyClass(inttest){}", ConstructorGenerator.Create("MyClass", BodyGenerator.Create(), new List<Parameter> { new Parameter("test", typeof(int))}).ToString());
        }

        [Test]
        public void Constructor_WhenCreatingConstructorWithModifiers_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("publicMyClass(){}", ConstructorGenerator.Create("MyClass", BodyGenerator.Create(), modifiers:new List<Modifiers>() { Modifiers.Public}).ToString());
        }

        [Test]
        public void Constructor_WhenCreatingConstructorWithAttribute_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("[Test]MyClass(){}", ConstructorGenerator.Create("MyClass", BodyGenerator.Create(), attributes:new List<Attribute> { new Attribute("Test")}).ToString());
        }

        [Test]
        public void Constructor_WhenCreatingConstructorWithParamterAndModifierAndAttribute_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("[Test]publicMyClass(inti){}",
                ConstructorGenerator.Create("MyClass", BodyGenerator.Create(),
                    new List<Parameter> {new Parameter("i", typeof(int))}, new List<Modifiers>() {Modifiers.Public},
                    new List<Attribute> {new Attribute("Test")}).ToString());
        }

    }
}