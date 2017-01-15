using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;

namespace Testura.Code.Tests.Generators.Common.Arguments.ArgumentTypes
{
    [TestFixture]
    class ArrayInitializeArgumentTests
    {
        [Test]
        public void GetArgumentSyntax_WhenUsingIntArray_ShouldGetCorrectCode()
        {
            var argument = new ArrayInitializationArgument(typeof(int), new List<IArgument>() { new ValueArgument(1), new ValueArgument(2)});
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("newint[]{1,2}", syntax.ToString());
        }
    }
}
