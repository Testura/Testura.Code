using System;
using System.Collections.Generic;
using NUnit.Framework;
using Testura.Code.Generators.Common;

namespace Testura.Code.Tests.Generators.Common
{
    [TestFixture]
    public class TypeGeneratorTests
    {
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
        public void Create_WhenCreatingPredefinedTypes_ShouldGenerateCorrectCode(Type type, string expected)
        {
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
    }
}