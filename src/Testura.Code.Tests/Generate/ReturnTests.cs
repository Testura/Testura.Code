using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Testura.Code.Generate;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Generate
{
    [TestFixture]
    public class ReturnTests 
    {
        [Test]
        public void True_WhenReturnTrue_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("returntrue;", Return.True().ToString());
        }

        [Test]
        public void True_WhenReturnFalse_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("returnfalse;", Return.False().ToString());
        }
    }
}
