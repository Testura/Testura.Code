using NUnit.Framework;
using Testura.Code.Helper;
using Testura.Code.Helper.Arguments;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Generate
{
    [TestFixture]
    class PropertyTests
    {
        [Test]
        public void Create_WhenCreatingPropertyWithOnlyGet_ShouldHaveNoSet()
        {
            Assert.AreEqual("publicInt32MyProperty{get;}", Property.Create("MyProperty", typeof(int), PropertyTypes.Get).ToString());   
        }

        [Test]
        public void Create_WhenCreatingPropertyWithOnlyGetAndSet_ShouldHaveBothGetAndSet()
        {
            Assert.AreEqual("publicInt32MyProperty{get;set;}", Property.Create("MyProperty", typeof(int), PropertyTypes.GetAndSet).ToString());
        }

        [Test]
        public void SetValue_WhenSettingValue_ShouldAssignValueToProperty()
        {
            Assert.AreEqual("myClass.MyProperty=1;", Property.SetValue("myClass", "MyProperty", 1, ArgumentType.Other).ToString());
        }

        [Test]
        public void SetValue_WhenSettingValueToAString_ShouldAssignValueToPropertyAndAddQuotes()
        {
            Assert.AreEqual("myClass.MyProperty=\"test\";", Property.SetValue("myClass", "MyProperty", "test", ArgumentType.String).ToString());
        }

        [Test]
        public void GetValue_WhenGettingValueFromProperty_ShouldGetValue()
        {
            Assert.AreEqual("myClass.MyProperty", Property.GetValue("myClass", "MyProperty").ToString());
        }

    }
}
