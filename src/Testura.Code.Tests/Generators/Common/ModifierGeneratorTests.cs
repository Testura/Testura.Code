using NUnit.Framework;
using Testura.Code.Generators.Common;

namespace Testura.Code.Tests.Generators.Common;

[TestFixture]
public class ModifierGeneratorTests
{
    [TestCase(Modifiers.Abstract, "abstract")]
    [TestCase(Modifiers.Private, "private")]
    [TestCase(Modifiers.Public, "public")]
    [TestCase(Modifiers.Static, "static")]
    [TestCase(Modifiers.Virtual, "virtual")]
    [TestCase(Modifiers.Override, "override")]
    [TestCase(Modifiers.Readonly, "readonly")]
    [TestCase(Modifiers.Internal, "internal")]
    [TestCase(Modifiers.Partial, "partial")]
    [TestCase(Modifiers.Async, "async")]
    [TestCase(Modifiers.Sealed, "sealed")]
    [TestCase(Modifiers.New, "new")]
    [TestCase(Modifiers.Protected, "protected")]
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