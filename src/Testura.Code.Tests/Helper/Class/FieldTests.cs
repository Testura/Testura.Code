using System.Collections.Generic;
using NUnit.Framework;
using Testura.Code.Helpers.Class;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Helper.Class
{
    [TestFixture]
    public class FieldTests
    {
        [Test]
        public void Create_WhenCreatingField_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("privateInt32myField;", Field.Create("myField", typeof(int)).ToString());
        }

        [Test]
        public void Create_WhenCreatingFieldWithGenericType_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("privateList<Int32>myField;", Field.Create("myField", typeof(List<int>)).ToString());
        }
    }
}
