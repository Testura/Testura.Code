using System;
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
    }
}
