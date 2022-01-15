using System.Collections.Generic;
using NUnit.Framework;
using Testura.Code.Builders;
using Testura.Code.Models;
using Testura.Code.Models.Properties;

namespace Testura.Code.Tests.Builders;

[TestFixture]
public class RecordBuilderTests
{
    private RecordBuilder _recordBuilder;

    [SetUp]
    public void SetUp()
    {
        _recordBuilder = new RecordBuilder("TestRecord", "MyNamespace");
    }

    [Test]
    public void Build_WhenGivenClassName_CodeShouldContainClassName()
    {
        Assert.IsTrue(_recordBuilder.Build().ToString().Contains("TestRecord"));
    }

    [Test]
    public void Build_WhenGivenNamespace_CodeShouldContainNamespace()
    {
        Assert.IsTrue(_recordBuilder.Build().ToString().Contains("MyNamespace"));
    }

    [Test]
    public void Build_WhenGivenNamespaceWithFileScopedNamespace_CodeShouldContainNamespace()
    {
        var classBuilder = new ClassBuilder("TestRecord", "MyNamespace", NamespaceType.FileScoped);
        Assert.IsTrue(classBuilder.Build().ToString().Contains("MyNamespace;"));
    }

    [Test]
    public void Build_WhenGivenField_CodeShouldContainField()
    {
        Assert.IsTrue(_recordBuilder.WithFields(new Field("myField", typeof(int), new List<Modifiers>() { Modifiers.Public })).Build().ToString().Contains("publicintmyField;"));
    }

    [Test]
    public void Build_WhenGivenAttributes_CodeShouldContainAttributes()
    {
        Assert.IsTrue(_recordBuilder.WithAttributes(new Attribute("MyAttribute")).Build().ToString().Contains("[MyAttribute]"));
    }

    [Test]
    public void Build_WhenGivenProperty_CodeShouldContainProperty()
    {
        Assert.IsTrue(_recordBuilder.WithProperties(new AutoProperty("MyProperty", typeof(int), PropertyTypes.GetAndSet)).Build().ToString().Contains("intMyProperty{get;set;}"));
    }

    [Test]
    public void Build_WhenGivenUsing_CodeShouldContainUsing()
    {
        Assert.IsTrue(_recordBuilder.WithUsings("some.namespace").Build().ToString().Contains("some.namespace"));
    }

    [Test]
    public void Build_WhenGivenModifiers_CodeShouldContainModifiers()
    {
        Assert.IsTrue(_recordBuilder.WithModifiers(Modifiers.Public, Modifiers.Abstract).Build().ToString().Contains("publicabstractrecordTestRecord"));
    }

    [Test]
    public void Build_WhenGivenInheritance_CodeShouldContainInheritance()
    {
        Assert.IsTrue(_recordBuilder.ThatInheritFrom(typeof(int)).Build().ToString().Contains("TestRecord:int"));
    }

    [Test]
    public void Build_WhenGivenPrimaryConstructor_CodeShouldContainPrimaryConstructor()
    {
        _recordBuilder
            .WithPrimaryConstructor(new Parameter("par1", typeof(int)))
            .WithFields(new Field("hello", typeof(string)));

        Assert.IsTrue(_recordBuilder.Build().ToString().Contains("TestRecord(intpar1){"));
    }

    [Test]
    public void Build_WhenGivenPrimaryConstructorAndNoMembers_CodeShouldContainPrimaryConstructor()
    {
        _recordBuilder
            .WithPrimaryConstructor(new Parameter("par1", typeof(int)));

        Assert.IsTrue(_recordBuilder.Build().ToString().Contains("TestRecord(intpar1);"));
    }
}
