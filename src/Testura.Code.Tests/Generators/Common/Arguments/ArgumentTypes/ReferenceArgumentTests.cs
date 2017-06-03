﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;
using Testura.Code.Models.References;

namespace Testura.Code.Tests.Generators.Common.Arguments.ArgumentTypes
{
    [TestFixture]
    public class ReferenceArgumentTests
    {
        [Test]
        public void GetArgumentSyntax_WhenUsingVariableReference_ShouldGetCode()
        {
            var argument = new ReferenceArgument(new VariableReference("test"));
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("test", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenUsingVariableReferenceAsNamedArgument_ShouldGetCode()
        {
            var argument = new ReferenceArgument(new VariableReference("test"), "namedArgument");
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("namedArgument:test", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenUsingMethodReference_ShouldGetCode()
        {
            var argument = new ReferenceArgument(new MethodReference("test"));
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("test()", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenUsingConstReference_ShouldGetCode()
        {
            var argument = new ReferenceArgument(new ConstantReference(1));
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("1", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenUsingNullReference_ShouldGetCode()
        {
            var argument = new ReferenceArgument(new NullReference());
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("null", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenUsingMemberReferenceReference_ShouldGetCode()
        {
            var argument = new ReferenceArgument(new VariableReference("test", new MemberReference("MyProperty")));
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("test.MyProperty", syntax.ToString());
        }
    }
}
