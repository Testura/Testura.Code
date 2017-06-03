using System;
using System.Collections.Generic;
using NUnit.Framework;
using Testura.Code.Generators.Common;
using Testura.Code.Models.Types;

namespace Testura.Code.Tests.Generators.Common
{
    [TestFixture]
    public class TypeGeneratorTests
    {
        private enum MyCustomEnum { Stuff }

        [TestCase(typeof(int), "int")]
        [TestCase(typeof(double), "double")]
        [TestCase(typeof(float), "float")]
        [TestCase(typeof(decimal), "decimal")]
        [TestCase(typeof(long), "long")]
        [TestCase(typeof(string), "string")]
        [TestCase(typeof(uint), "uint")]
        [TestCase(typeof(ushort), "ushort")]
        [TestCase(typeof(ulong), "ulong")]
        [TestCase(typeof(sbyte), "sbyte")]
        [TestCase(typeof(byte), "byte")]
        [TestCase(typeof(char), "char")]
        [TestCase(typeof(bool), "bool")]
        [TestCase(typeof(MyCustomEnum), "MyCustomEnum")]
        public void Create_WhenCreatingPredefinedTypes_ShouldGenerateCorrectCode(Type type, string expected)
        {
            Assert.AreEqual(expected, TypeGenerator.Create(type).ToString());
        }

        [TestCase(typeof(int?), "int?")]
        [TestCase(typeof(double?), "double?")]
        [TestCase(typeof(float?), "float?")]
        [TestCase(typeof(decimal?), "decimal?")]
        [TestCase(typeof(long?), "long?")]
        [TestCase(typeof(uint?), "uint?")]
        [TestCase(typeof(ushort?), "ushort?")]
        [TestCase(typeof(ulong?), "ulong?")]
        [TestCase(typeof(sbyte?), "sbyte?")]
        [TestCase(typeof(byte?), "byte?")]
        [TestCase(typeof(char?), "char?")]
        [TestCase(typeof(bool?), "bool?")]
        [TestCase(typeof(MyCustomEnum?), "MyCustomEnum?")]
        public void Create_WhenCreatingPredefinedTypesAsNullable_ShouldGenerateCorrectCode(Type type, string expected)
        {
            if (Nullable.GetUnderlyingType(type) != null)
            {
                // It's Nullable
                var o = 0;
            }

            Assert.AreEqual(expected, TypeGenerator.Create(type).ToString());
        }

        [Test]
        public void Create_WhenCreatingWithNoPredfinedType_ShouldGenerateCode()
        {
            Assert.AreEqual("List", TypeGenerator.Create(typeof(List)).ToString());
        }

        [Test]
        public void Create_WhenCreatingWithNoPredfinedGenericType_ShouldGenerateCode()
        {
            Assert.AreEqual("List<string>", TypeGenerator.Create(typeof(List<string>)).ToString());
        }

        [Test]
        public void Create_WhenCreatingCustomType_ShouldGenerateCode()
        {
            Assert.AreEqual("MyNewClass", TypeGenerator.Create(CustomType.Create("MyNewClass")).ToString());
        }

        [Test]
        public void Create_WhenCreatingArrayWithPredefinedType_ShouldGenerateCode()
        {
            Assert.AreEqual("int[]", TypeGenerator.Create(typeof(int[])).ToString());
        }

        [Test]
        public void Create_WhenCreatingArrayWithClassType_ShouldGenerateCode()
        {
            Assert.AreEqual("TypeGeneratorTests[]", TypeGenerator.Create(typeof(TypeGeneratorTests[])).ToString());
        }

        [Test]
        public void Create_WhenCreatingArrayWithGenericClassType_ShouldGenerateCode()
        {
            Assert.AreEqual("List<string>[]", TypeGenerator.Create(typeof(List<string>[])).ToString());
        }
    }
}