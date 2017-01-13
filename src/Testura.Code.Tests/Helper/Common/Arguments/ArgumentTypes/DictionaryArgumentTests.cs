using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;

namespace Testura.Code.Tests.Helper.Common.Arguments.ArgumentTypes
{
    [TestFixture]
    public class DictionaryArgumentTests
    {
        [Test]
        public void GetArgumentSyntax_WhenUsingDictionary_ShouldGetCorrectCode()
        {
            var argument = new DictionaryInitializationArgument<int, int>(new Dictionary<int, IArgument>() { [1] = new ValueArgument(2)});
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("newDictionary<System.Int32,System.Int32>{[1]=2}", syntax.ToString());
        }
    }
}
