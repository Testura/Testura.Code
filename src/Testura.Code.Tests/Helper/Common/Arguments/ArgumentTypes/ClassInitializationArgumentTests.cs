using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;

namespace Testura.Code.Tests.Helper.Common.Arguments.ArgumentTypes
{
    [TestFixture]
    public class ClassInitializationArgumentTests
    {
        [Test]
        public void GetArgumentSyntax_WhenInitializeClass_ShouldGetCorrectCode()
        {
            var argument = new ClassInitialiationArgument(typeof(String));
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("newString()", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenInitializeClassWithArgument_ShouldGetCorrectCode()
        {
            var argument = new ClassInitialiationArgument(typeof(String), new List<IArgument> { new ValueArgument(0)});
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("newString(0)", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenInitializeClassWithGeneric_ShouldGetCorrectCode()
        {
            var argument = new ClassInitialiationArgument(typeof(List), new List<Type> { typeof(string)});
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("newList<System.String>()", syntax.ToString());
        }
    }
}
