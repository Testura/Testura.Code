using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Testura.Code.Generate;

namespace Testura.Code.Tests.Generate
{
    [TestFixture]
    public class VariableTests
    {

        [Test]
        public void ggdfgfdg()
        {
            var variable = Variable.CreateType("hello", typeof(int), 1, true, true);
            var s = variable.ToString();
        }
    }
}
