using System.Collections.Generic;
using NUnit.Framework;
using Testura.Code.Generators.Class;
using Testura.Code.Models;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Helper.Class
{
    [TestFixture]
    public class FieldTests
    {
        [Test]
        public void Create_WhenCreatingField_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("Int32myField;", FieldGenerator.Create(new Field("myField", typeof(int))).ToString());
        }

        [Test]
        public void Create_WhenCreatingFieldWithGenericType_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("List<Int32>myField;", FieldGenerator.Create(new Field("myField", typeof(List<int>))).ToString());
        }

        [Test]
        public void Create_WhenCreatingFieldWithModifiers_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("publicInt32myField;", FieldGenerator.Create(new Field("myField", typeof(int), new List<Modifiers>() { Modifiers.Public})).ToString());
        }
    }
}
