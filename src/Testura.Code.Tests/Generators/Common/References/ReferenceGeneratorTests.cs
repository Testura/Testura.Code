using System.Collections.Generic;
using NUnit.Framework;
using Testura.Code.Generators.Common;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;
using Testura.Code.Models.References;
using Assert = NUnit.Framework.Assert;
using IArgument = Testura.Code.Generators.Common.Arguments.ArgumentTypes.IArgument;

namespace Testura.Code.Tests.Generators.Common.References;

[TestFixture]
public class ReferenceGeneratorTests
{
    [Test]
    public void Create_WhenCreatingVariableRefernce_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual("myVariable", ReferenceGenerator.Create(new VariableReference("myVariable")).ToString());
    }

    [Test]
    public void Create_WhenCreatingVariableRefernceWithMethodMember_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual("myVariable.MyMethod()", ReferenceGenerator.Create(new VariableReference("myVariable", new MethodReference("MyMethod", new List<IArgument>()))).ToString());
    }

    [Test]
    public void Create_WhenCreatingVariableRefernceWithMethodMemberThatHasArgument_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual("myVariable.MyMethod(1,\"test\")", ReferenceGenerator.Create(new VariableReference("myVariable", new MethodReference("MyMethod", new List<IArgument> { new ValueArgument(1), new ValueArgument("test") }))).ToString());
    }

    [Test]
    public void Create_WhenCreatingVariableRefernceWithFieldMember_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual("myVariable.field", ReferenceGenerator.Create(new VariableReference("myVariable", new MemberReference("field"))).ToString());
    }

    [Test]
    public void Create_WhenCreatingVariableRefernceWithPropertyMember_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual("myVariable.Property", ReferenceGenerator.Create(new VariableReference("myVariable", new MemberReference("Property"))).ToString());
    }

    [Test]
    public void Create_WhenCreatingVariableRefernceWithChainedMembers_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual("myVariable.MyMethod().MyProperty", ReferenceGenerator.Create(new VariableReference("myVariable", new MethodReference("MyMethod", new MemberReference("MyProperty"), new List<IArgument>()))).ToString());
    }
}