using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Testura.Code.Generators.Common;

namespace Testura.Code.Tests.Generators.Common
{
    [TestFixture]
    public class ModifierGeneratorTests
    {
        [TestCase(Modifiers.Abstract, "abstract")]
        [TestCase(Modifiers.Private, "private")]
        [TestCase(Modifiers.Public, "public")]
        [TestCase(Modifiers.Static, "static")]
        [TestCase(Modifiers.Virtual, "virtual")]
        public void Create_WhenCreatingWithModifier_ShouldGenerateCode(Modifiers modifier, string expected)
        {
            Assert.AreEqual(expected, ModifierGenerator.Create(modifier).ToString());
        }

        [Test]
        public void Create_WhenCreatingWithMultipleModifier_ShouldGenerateCode()
        {
            Assert.AreEqual("publicabstract", ModifierGenerator.Create(Modifiers.Public, Modifiers.Abstract).ToString());
        }
    }
}