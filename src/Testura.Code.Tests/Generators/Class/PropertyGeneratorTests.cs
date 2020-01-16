using System.Collections.Generic;
using NUnit.Framework;
using Testura.Code.Generators.Class;
using Testura.Code.Generators.Common;
using Testura.Code.Models;
using Testura.Code.Models.Properties;
using Testura.Code.Models.References;
using Testura.Code.Statements;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Generators.Class
{
    [TestFixture]
    class PropertyGeneratorTests
    {
        [Test]
        public void Create_WhenCreatingAutoPropertyWithOnlyGet_ShouldHaveNoSet()
        {
            Assert.AreEqual("intMyProperty{get;}", PropertyGenerator.Create(new AutoProperty("MyProperty", typeof(int), PropertyTypes.Get)).ToString());   
        }

        [Test]
        public void Create_WhenCreatingAutoPropertyWithGetAndSet_ShouldHaveBothGetAndSet()
        {
            Assert.AreEqual("intMyProperty{get;set;}", PropertyGenerator.Create(new AutoProperty("MyProperty", typeof(int), PropertyTypes.GetAndSet)).ToString());
        }

        [Test]
        public void Create_WhenCreatingPropertyWithGetModifiers_ShouldHaveGetModifiers()
        {
            Assert.AreEqual("intMyProperty{protectedget;set;}", PropertyGenerator.Create(new AutoProperty("MyProperty", typeof(int), PropertyTypes.GetAndSet, getModifiers: new List<Modifiers> { Modifiers.Protected})).ToString());
        }

        [Test]
        public void Create_WhenCreatingPropertyWithSetModifiers_ShouldHaveGetModifiers()
        {
            Assert.AreEqual("intMyProperty{get;privateinternalset;}", PropertyGenerator.Create(new AutoProperty("MyProperty", typeof(int), PropertyTypes.GetAndSet, setModifiers: new List<Modifiers> { Modifiers.Private, Modifiers.Internal })).ToString());
        }

        [Test]
        public void Create_WhenCreatingAutoPropertyWithAttribute_ShouldHaveAttribute()
        {
            Assert.AreEqual("[Test]intMyProperty{get;set;}", PropertyGenerator.Create(new AutoProperty("MyProperty", typeof(int), PropertyTypes.GetAndSet, new List<Modifiers>(), new List<Attribute> { new Attribute("Test")})).ToString());
        }

        [Test]
        public void Create_WhenCreatingAutoPropertyWithModifer_ShouldHaveModifier()
        {
            Assert.AreEqual("publicstaticintMyProperty{get;set;}", PropertyGenerator.Create(new AutoProperty("MyProperty", typeof(int), PropertyTypes.GetAndSet, new List<Modifiers>() { Modifiers.Public, Modifiers.Static })).ToString());
        }

        [Test]
        public void Create_WhenCreatingBodyPropertyWithOnlyGet_ShouldHaveNoSet()
        {
            Assert.AreEqual("intMyProperty{get{return1;}}", PropertyGenerator.Create(new BodyProperty("MyProperty", typeof(int), BodyGenerator.Create(Statement.Jump.Return(new ConstantReference(1))))).ToString());
        }

        [Test]
        public void Create_WhenCreatingBodyPropertyWithGetAndSet_ShouldHaveBothGetAndSet()
        {
            Assert.AreEqual("intMyProperty{get{return1;}}", PropertyGenerator.Create(new BodyProperty("MyProperty", typeof(int), BodyGenerator.Create(Statement.Jump.Return(new ConstantReference(1))))).ToString());
        }

        [Test]
        public void Create_WhenCreatingBodyPropertyWithAttribute_ShouldHaveAttribute()
        {
            Assert.AreEqual("[Test]intMyProperty{get{return1;}}", PropertyGenerator.Create(new BodyProperty(
                "MyProperty", 
                typeof(int),
                BodyGenerator.Create(Statement.Jump.Return(new ConstantReference(1))), 
                attributes: new List<Attribute> { new Attribute("Test")})).ToString());
        }
        [Test]
        public void Create_WhenCreatingBodyPropertyWithModifier_ShouldHaveModifier()
        {
            Assert.AreEqual("publicvirtualintMyProperty{get{return1;}}", PropertyGenerator.Create(new BodyProperty(
                "MyProperty", 
                typeof(int), 
                BodyGenerator.Create(Statement.Jump.Return(new ConstantReference(1))), 
                new List<Modifiers> { Modifiers.Public, Modifiers.Virtual })).ToString());
        }
    }
}
