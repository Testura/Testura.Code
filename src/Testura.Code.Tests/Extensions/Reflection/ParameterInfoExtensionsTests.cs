using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;
using Testura.Code.Extensions.Reflection;

namespace Testura.Code.Tests.Extensions.Reflection
{
    [TestFixture]
    public class ParameterInfoExtensionsTests
    {
        public void ToParameter_WhenHavingAParamterInfo_ShouldGetParameterObject()
        {
            var parameterInfo = typeof(TestClass).GetMethods().First().GetParameters().First();
            var parameter = parameterInfo.ToParameter();
            Assert.AreEqual(parameterInfo.Name, parameter.Name);
            Assert.AreEqual(parameterInfo.ParameterType, parameter.Type);
        }

        private class TestClass
        {
            public void Method(int firstPar)
            {
                
            }
        }
    }
}
