using System.Collections.Generic;
using NUnit.Framework;
using Testura.Code.Builders;
using Testura.Code.Models;
using Testura.Code.Models.Properties;

namespace Testura.Code.Tests.Builders;

[TestFixture]
public class StructBuilderTests
{
    private StructBuilder _structBuilder;

    [SetUp]
    public void SetUp()
    {
        _structBuilder = new StructBuilder("TestStruct", "MyNamespace");
    }

    [Test]
    public void Build_WhenGivenClassName_CodeShouldContainClassName()
    {
        Assert.IsTrue(_structBuilder.Build().ToString().Contains("TestStruct"));
    }

    [Test]
    public void Build_WhenGivenNamespace_CodeShouldContainNamespace()
    {
        Assert.IsTrue(_structBuilder.Build().ToString().Contains("MyNamespace"));
    }

    [Test]
    public void Build_WhenGivenNamespaceWithFileScopedNamespace_CodeShouldContainNamespace()
    {
        var classBuilder = new ClassBuilder("TestStruct", "MyNamespace", NamespaceType.FileScoped);
        Assert.IsTrue(classBuilder.Build().ToString().Contains("MyNamespace;"));
    }

    [Test]
    public void Build_WhenGivenField_CodeShouldContainField()
    {
        Assert.IsTrue(_structBuilder.WithFields(new Field("myField", typeof(int), new List<Modifiers>() { Modifiers.Public })).Build().ToString().Contains("publicintmyField;"));
    }

    [Test]
    public void Build_WhenGivenAttributes_CodeShouldContainAttributes()
    {
        Assert.IsTrue(_structBuilder.WithAttributes(new Attribute("MyAttribute")).Build().ToString().Contains("[MyAttribute]"));
    }

    [Test]
    public void Build_WhenGivenProperty_CodeShouldContainProperty()
    {
        Assert.IsTrue(_structBuilder.WithProperties(new AutoProperty("MyProperty", typeof(int), PropertyTypes.GetAndSet)).Build().ToString().Contains("intMyProperty{get;set;}"));
    }

    [Test]
    public void Build_WhenGivenUsing_CodeShouldContainUsing()
    {
        Assert.IsTrue(_structBuilder.WithUsings("some.namespace").Build().ToString().Contains("some.namespace"));
    }

    [Test]
    public void Build_WhenGivenModifiers_CodeShouldContainModifiers()
    {
        Assert.IsTrue(_structBuilder.WithModifiers(Modifiers.Public, Modifiers.Abstract).Build().ToString().Contains("publicabstractstructTestStruct"));
    }

    [Test]
    public void Build_WhenGivenInheritance_CodeShouldContainInheritance()
    {
        Assert.IsTrue(_structBuilder.ThatInheritFrom(typeof(int)).Build().ToString().Contains("TestStruct:int"));
    }
}
