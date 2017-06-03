using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;

namespace Testura.Code.Tests.Generators.Common.Arguments.ArgumentTypes
{
    [TestFixture]
    public class ClassInitializationArgumentTests
    {
        [Test]
        public void GetArgumentSyntax_WhenInitializeClass_ShouldGetCorrectCode()
        {
            var argument = new ClassInitializationArgument(typeof(String));
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("newstring()", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenInitializeClassAsNamedArgument_ShouldGetCorrectCode()
        {
            var argument = new ClassInitializationArgument(typeof(String), namedArgument: "namedArgument");
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("namedArgument:newstring()", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenInitializeClassWithArgument_ShouldGetCorrectCode()
        {
            var argument = new ClassInitializationArgument(typeof(String), new List<IArgument> { new ValueArgument(0)});
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("newstring(0)", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenInitializeClassWithGeneric_ShouldGetCorrectCode()
        {
            var argument = new ClassInitializationArgument(typeof(List<string>));
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("newList<string>()", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenInitializeClassWithMultipleGeneric_ShouldGetCorrectCode()
        {
            var argument = new ClassInitializationArgument(typeof(List<List<List<int>>>));
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("newList<List<List<int>>>()", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenInitializeClassWithMultipleGenericAsArgument_ShouldGetCorrectCode()
        {
            var argument = new ClassInitializationArgument(typeof(List), genericTypes: new[] { typeof(List<List<int>>) });
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("newList<List<List<int>>>()", syntax.ToString());
        }
    }
}
