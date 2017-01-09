using System.Collections.Generic;
using NUnit.Framework;
using Testura.Code.Helpers.Class;
using Testura.Code.Models;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Helper.Class
{
    [TestFixture]
    class PropertyTests
    {
        [Test]
        public void Create_WhenCreatingPropertyWithOnlyGet_ShouldHaveNoSet()
        {
            Assert.AreEqual("Int32MyProperty{get;}", Property.Create("MyProperty", typeof(int), PropertyTypes.Get).ToString());   
        }

        [Test]
        public void Create_WhenCreatingPropertyWithOnlyGetAndSet_ShouldHaveBothGetAndSet()
        {
            Assert.AreEqual("Int32MyProperty{get;set;}", Property.Create("MyProperty", typeof(int), PropertyTypes.GetAndSet).ToString());
        }

        [Test]
        public void Create_WhenCreatingPropertyWithAttribute_ShouldHaveAttribute()
        {
            Assert.AreEqual("[Test]Int32MyProperty{get;set;}", Property.Create("MyProperty", typeof(int), PropertyTypes.GetAndSet, new List<Modifiers>(), new List<Attribute> { new Attribute("Test")}).ToString());
        }

        [Test]
        public void Create_WhenCreatingPropertyWithModifer_ShouldHaveModifier()
        {
            Assert.AreEqual("publicstaticInt32MyProperty{get;set;}", Property.Create("MyProperty", typeof(int), PropertyTypes.GetAndSet, new List<Modifiers>() { Modifiers.Public, Modifiers.Static }).ToString());
        }
    }
}
