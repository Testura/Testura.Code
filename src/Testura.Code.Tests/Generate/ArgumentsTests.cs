using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using NUnit.Framework.Internal;
using NUnit.Framework;
using Testura.Code.Generate;
using Testura.Code.Generate.ArgumentTypes;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Generate
{
    [TestFixture]
    public class ArgumentsTests
    {
        [Test]
        public void Create_WhenNotProvidingAnyArguments_ShouldGetEmptyString()
        {
            Assert.AreEqual("()", Argument.Create().ToString());
        }

        [Test]
        public void Create_WhenNotProvidingSingleArgument_ShouldContainArgument()
        {
            Assert.AreEqual("(1)", Argument.Create(new ValueArgument(1)).ToString());
        }

        [Test]
        public void Create_WhenNotProvidingMultipleArgument_ShouldContainArguments()
        {
            Assert.AreEqual("(1,2)", Argument.Create(new ValueArgument(1), new ValueArgument(2)).ToString());
        }
    }
}