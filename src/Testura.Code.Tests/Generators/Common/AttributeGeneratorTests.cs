using System.Collections.Generic;
using NUnit.Framework;
using Testura.Code.Generators.Common;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;
using Testura.Code.Generators.Common.BinaryExpressions;
using Testura.Code.Models;
using Testura.Code.Models.References;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Generators.Common;

[TestFixture]
public class AttributeGeneratorTests
{
    [Test]
    public void Create_WhenNotProvidingAnyAttributes_ShouldGetEmptyString()
    {
        Assert.AreEqual(string.Empty, AttributeGenerator.Create().ToString());
    }

    [Test]
    public void Create_WhenCreatingWithSingleAttrbute_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual("[Test]", AttributeGenerator.Create(new Attribute("Test", new List<IArgument>())).ToString());
    }

    [Test]
    public void Create_WhenCreatingWithMultipleAttributes_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual("[Test][TestCase]", AttributeGenerator.Create(new Attribute("Test"), new Attribute("TestCase")).ToString());
    }

    [Test]
    public void Create_WhenCreatingAttributeWithArguments_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual("[Test(1,2)]", AttributeGenerator.Create(new Attribute("Test", new List<IArgument>() { new ValueArgument(1), new ValueArgument(2) })).ToString());
    }

    [Test]
    public void Create_WhenCreatingAttributeWithNamedArgument_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual("[Test(with:1,value:2)]", AttributeGenerator.Create(new Attribute("Test", new List<IArgument>() { new ValueArgument(1, namedArgument: "with"), new ValueArgument(2, namedArgument: "value") })).ToString());
    }

    [Test]
    public void Create_WhenCreatingAttributeWithAssignedArgument_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual("[Test(with=1,value=true)]", AttributeGenerator.Create(new Attribute("Test", new List<IArgument>() { new AssignArgument("with", 1), new AssignArgument("value", true) })).ToString());
    }
}