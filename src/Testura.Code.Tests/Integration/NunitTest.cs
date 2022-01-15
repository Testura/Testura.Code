using System;
using System.Collections.Generic;
using NUnit.Framework;
using Testura.Code.Builders;
using Testura.Code.Generators.Common;
using Testura.Code.Generators.Special;
using Testura.Code.Models.References;
using Testura.Code.Statements;
using Attribute = Testura.Code.Models.Attribute;

namespace Testura.Code.Tests.Integration;

[TestFixture]
public class NunitTest
{
    [Test]
    public void Test_ArgumentNull()
    {
        var classBuilder = new ClassBuilder("NullTest", "MyTest");
        var @class = classBuilder
            .WithUsings("System", "NUnit.Framework")
            .WithModifiers(Modifiers.Public)
            .WithMethods(
                new MethodBuilder("SetUp")
                    .WithAttributes(new Attribute("SetUp"))
                    .WithModifiers(Modifiers.Public)
                    .Build(),
                new MethodBuilder("Test_WhenAddingNumber_ShouldBeCorrectSum")
                    .WithAttributes(new Attribute("Test"))
                    .WithModifiers(Modifiers.Public)
                    .WithBody(
                        BodyGenerator.Create(
                            Statement.Declaration.Declare("myList", typeof(List<int>)),
                            NunitAssertGenerator.Throws(new VariableReference("myList", new MethodReference("First")), typeof(ArgumentNullException))))
                    .Build())
            .Build();
        Assert.AreEqual(@"usingSystem;usingNUnit.Framework;namespaceMyTest{publicclassNullTest{[SetUp]publicvoidSetUp(){}[Test]publicvoidTest_WhenAddingNumber_ShouldBeCorrectSum(){List<int>myList;Assert.Throws<ArgumentNullException>(()=>myList.First(),"""");}}}", @class.ToString());
    }
}
