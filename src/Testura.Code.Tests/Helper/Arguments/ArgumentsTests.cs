using NUnit.Framework;
using Testura.Code.Helpers.Arguments;
using Testura.Code.Helpers.Arguments.ArgumentTypes;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Helper.Arguments
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