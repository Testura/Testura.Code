using System.Collections.Generic;
using NUnit.Framework;
using Testura.Code.Generators.Common;
using Testura.Code.Models;
using Attribute = Testura.Code.Models.Attribute;

namespace Testura.Code.Tests.Generators.Common
{
    [TestFixture]
    public class EnumGeneratorTests
    {
        [Test]
        public void Enum_WhenCreatingEnum_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("enumMyEnum{MyValue,MyOtherValue}", EnumGenerator.Create("MyEnum", new List<EnumMember> { new EnumMember("MyValue"), new EnumMember("MyOtherValue") }).ToString());
        }

        [Test]
        public void Enum_WhenCreatingEnumWithModifiers_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("publicenumMyEnum{MyValue,MyOtherValue}", EnumGenerator.Create("MyEnum", new List<EnumMember> { new EnumMember("MyValue"), new EnumMember("MyOtherValue") }, modifiers: new List<Modifiers>() { Modifiers.Public }).ToString());
        }

        [Test]
        public void Enum_WhenCreatingEnumWithAttribute_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("[Test]enumMyEnum{MyValue,MyOtherValue}", EnumGenerator.Create("MyEnum", new List<EnumMember> { new EnumMember("MyValue"), new EnumMember("MyOtherValue") }, attributes: new List<Attribute> { new Attribute("Test") }).ToString());
        }

        [Test]
        public void Enum_WhenCreatingEnumWithParamterAndModifierAndAttribute_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual(
                "[Test]publicenumMyEnum{MyValue,MyOtherValue}",
                EnumGenerator.Create("MyEnum", new List<EnumMember> { new EnumMember("MyValue"), new EnumMember("MyOtherValue") }, new List<Modifiers> { Modifiers.Public },
                    new List<Attribute> { new("Test") }).ToString());
        }

        [Test]
        public void Enum_WhenCreatingEnumWithAttributeOnMember_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual(
                "[Test]publicenumMyEnum{[MyAttribute]MyValue,MyOtherValue}",
                EnumGenerator.Create("MyEnum", new List<EnumMember> { new EnumMember("MyValue", attributes: new[] { new Attribute("MyAttribute") }), new EnumMember("MyOtherValue") }, new List<Modifiers>() { Modifiers.Public },
                    new List<Attribute> { new Attribute("Test") }).ToString());
        }

        [Test]
        public void Enum_WhenCreatingEnumWithValueOnMember_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual(
                "[Test]publicenumMyEnum{MyValue=2,MyOtherValue}",
                EnumGenerator.Create("MyEnum", new List<EnumMember> { new EnumMember("MyValue", 2), new EnumMember("MyOtherValue") }, new List<Modifiers> { Modifiers.Public },
                    new List<Attribute> { new Attribute("Test") }).ToString());
        }
    }
}
