using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using Testura.Code.Helpers.Common.Arguments.ArgumentTypes;

namespace Testura.Code.Tests.Helper.Common.Arguments.ArgumentTypes
{
    [TestFixture]
    public class TypeOfArgumentTests
    {
        [Test]
        public void GetArgumentSyntax_WhenUsingType_ShouldGetCode()
        {
            var argument = new TypeOfArgument(typeof(int));
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("typeof(Int32)", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenUsingTypeName_ShouldGetCode()
        {
            var argument = new TypeOfArgument("int");
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("typeof(int)", syntax.ToString());
        }
    }
}
