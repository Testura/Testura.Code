using System.Collections.Generic;
using NUnit.Framework;
using Testura.Code.Generators.Common;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;
using Testura.Code.Models.Types;

namespace Testura.Code.Tests.Generators.Common;

[TestFixture]
public class ObjectCreationTests
{
    [Test]
    public void ObjectCreation_WhenCreateObjectWithJustType_ShouldGenerateCorrectCode()
    {
        var objectCreation = ObjectCreationGenerator.Create(CustomType.Create("Hello"));
        Assert.IsNotNull(objectCreation);
        Assert.AreEqual("newHello()", objectCreation.ToString());
    }

    [Test]
    public void ObjectCreation_WhenCreateObjectWithTypeAndArguments_ShouldGenerateCorrectCode()
    {
        var objectCreation = ObjectCreationGenerator.Create(CustomType.Create("Hello"),  new List<IArgument> { new ValueArgument(1) });
        Assert.IsNotNull(objectCreation);
        Assert.AreEqual("newHello(1)", objectCreation.ToString());
    }

    [Test]
    public void ObjectCreation_WhenCreateObjectWithTypeThatHaveGenerics_ShouldGenerateCorrectCode()
    {
        var objectCreation = ObjectCreationGenerator.Create(typeof(List<string>));
        Assert.IsNotNull(objectCreation);
        Assert.AreEqual("newList<string>()", objectCreation.ToString());
    }
}
