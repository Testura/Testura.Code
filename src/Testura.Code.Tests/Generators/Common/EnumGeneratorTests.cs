using System.Collections.Generic;
using NUnit.Framework;
using Testura.Code.Generators.Common;
using Attribute = Testura.Code.Models.Attribute;

namespace Testura.Code.Tests.Generators.Common
{
    [TestFixture]
    public class EnumGeneratorTests
    {
        [Test]
        public void Enum_WhenCreatingEnum_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("enumMyEnum{MyValue,MyOtherValue}", EnumGenerator.Create("MyEnum", new List<string> { "MyValue", "MyOtherValue" }).ToString());
        }

        [Test]
        public void Enum_WhenCreatingEnumWithModifiers_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("publicenumMyEnum{MyValue,MyOtherValue}", EnumGenerator.Create("MyEnum", new List<string> { "MyValue", "MyOtherValue" }, modifiers:new List<Modifiers>() { Modifiers.Public}).ToString());
        }

        [Test]
        public void Enum_WhenCreatingEnumWithAttribute_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("[Test]enumMyEnum{MyValue,MyOtherValue}", EnumGenerator.Create("MyEnum", new List<string> { "MyValue", "MyOtherValue" }, attributes:new List<Attribute> { new Attribute("Test")}).ToString());
        }

        [Test]
        public void Enum_WhenCreatingEnumWithParamterAndModifierAndAttribute_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("[Test]publicenumMyEnum{MyValue,MyOtherValue}",
                EnumGenerator.Create("MyEnum", new List<string> { "MyValue", "MyOtherValue" },new List<Modifiers>() {Modifiers.Public},
                    new List<Attribute> {new Attribute("Test")}).ToString());
        }

    }
}