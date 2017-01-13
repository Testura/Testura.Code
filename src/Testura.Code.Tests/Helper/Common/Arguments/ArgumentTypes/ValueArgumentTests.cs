using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using Testura.Code.Generators.Common;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;

namespace Testura.Code.Tests.Helper.Common.Arguments.ArgumentTypes
{
    [TestFixture]
    public class ValueArgumentTests
    {
        [Test]
        public void GetArgumentSyntax_WhenUsingNormalValue_ShouldGetCode()
        {
            var argument = new ValueArgument(1);
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("1", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenUsingStringValue_ShouldGetCodeThatContainsQuotes()
        {
            var argument = new ValueArgument("test");
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("\"test\"", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenUsingStringValueAndArgumentTypePath_ShouldGetCodeThatContainsQuotesAndAtSign()
        {
            var argument = new ValueArgument("test", ArgumentType.Path);
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("@\"test\"", syntax.ToString());
        }

    }
}
