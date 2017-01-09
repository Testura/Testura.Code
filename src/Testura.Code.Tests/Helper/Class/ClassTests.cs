using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;
using Testura.Code.Helpers.Common;
using Testura.Code.Models;
using Attribute = Testura.Code.Models.Attribute;

namespace Testura.Code.Tests.Helper.Class
{
    [TestFixture]
    public class ClassTests
    {
        [Test]
        public void Constructor_WhenCreatingConstructor_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("MyClass(){}", Testura.Code.Helpers.Class.Class.Constructor("MyClass", Body.Create()).ToString());
        }

        [Test]
        public void Constructor_WhenCreatingConstructorWithParameters_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("MyClass(Int32test){}", Testura.Code.Helpers.Class.Class.Constructor("MyClass", Body.Create(), new List<Parameter> { new Parameter("test", typeof(int))}).ToString());
        }

        [Test]
        public void Constructor_WhenCreatingConstructorWithModifiers_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("publicMyClass(){}", Testura.Code.Helpers.Class.Class.Constructor("MyClass", Body.Create(), modifiers:new List<Modifiers>() { Modifiers.Public}).ToString());
        }

        [Test]
        public void Constructor_WhenCreatingConstructorWithAttribute_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("[Test]MyClass(){}", Testura.Code.Helpers.Class.Class.Constructor("MyClass", Body.Create(), attributes:new List<Attribute> { new Attribute("Test")}).ToString());
        }

        [Test]
        public void Constructor_WhenCreatingConstructorWithParamterAndModifierAndAttribute_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("[Test]publicMyClass(Int32i){}",
                Testura.Code.Helpers.Class.Class.Constructor("MyClass", Body.Create(),
                    new List<Parameter> {new Parameter("i", typeof(int))}, new List<Modifiers>() {Modifiers.Public},
                    new List<Attribute> {new Attribute("Test")}).ToString());
        }

    }
}
