using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using Testura.Code.Generators.Common;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;

namespace Testura.Code.Tests.Generators.Common.Arguments.ArgumentTypes
{
    [TestFixture]
    public class ValueArgumentTests
    {
        public void GetArgumentSyntax_WhenUsingNumberValue_ShouldGetCode()
        {
            var argument = new ValueArgument(1);
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("1", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenUsingBooleanValue_ShouldGetCorrectFormat()
        {
            var argument = new ValueArgument(true);
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("true", syntax.ToString());
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

        [Test]
        public void Constructor_WhenUsingNonBooleanStringOrNumber_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => new ValueArgument(new List<string>()));
        }
    }
}
