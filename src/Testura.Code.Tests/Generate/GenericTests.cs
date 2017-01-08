using NUnit.Framework;
using Testura.Code.Helper;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Generate
{
    [TestFixture]
    public class GenericTests
    {
        [Test]
        public void gfdg()
        {
            Assert.AreEqual("test<System.Int32>>", Generic.Create("test", typeof(int)));
        }
    }
}
