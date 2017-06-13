﻿using System.Collections.Generic;
using NUnit.Framework;
using Testura.Code.Generators.Class;
using Testura.Code.Models;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Generators.Class
{
    [TestFixture]
    public class FieldGeneratorTests
    {
        [Test]
        public void Create_WhenCreatingField_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("intmyField;", FieldGenerator.Create(new Field("myField", typeof(int))).ToString());
        }

        [Test]
        public void Create_WhenCreatingFieldWithGenericType_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("List<int>myField;", FieldGenerator.Create(new Field("myField", typeof(List<int>))).ToString());
        }

        [Test]
        public void Create_WhenCreatingFieldWithMultipleGenericType_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("List<List<string>>myField;", FieldGenerator.Create(new Field("myField", typeof(List<List<string>>))).ToString());
        }

        [Test]
        public void Create_WhenCreatingFieldWithModifiers_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("publicintmyField;", FieldGenerator.Create(new Field("myField", typeof(int), new List<Modifiers>() { Modifiers.Public})).ToString());
        }

        [Test]
        public void Create_WhenCreatingFieldWithAttribute_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("[Test]intmyField;", FieldGenerator.Create(new Field("myField", typeof(int), attributes: new List<Attribute>() { new Attribute("Test") })).ToString());
        }
    }
}
