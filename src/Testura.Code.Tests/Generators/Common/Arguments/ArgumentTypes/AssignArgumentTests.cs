using System.Security.Cryptography.X509Certificates;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using Testura.Code.Generators.Common;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;
using Testura.Code.Models.References;

namespace Testura.Code.Tests.Generators.Common.Arguments.ArgumentTypes;

[TestFixture]
public class AssignArgumentTests
{
    [TestCase(10, "hello=10")]
    [TestCase(true, "hello=true")]
    public void GetArgumentSyntax_WhenUsingAssignArgumentWithObject_ShouldGetCode(object value, string expected)
    {
        var argument = new AssignArgument("hello", value);
        var syntax = argument.GetArgumentSyntax();

        Assert.IsInstanceOf<ArgumentSyntax>(syntax);
        Assert.AreEqual(expected, syntax.ToString());
    }

    [Test]
    public void GetArgumentSyntax_WhenUsingAssignArgumentWithString_ShouldGetCode()
    {
        var argument = new AssignArgument("hello", "meh");
        var syntax = argument.GetArgumentSyntax();

        Assert.IsInstanceOf<ArgumentSyntax>(syntax);
        Assert.AreEqual("hello=\"meh\"", syntax.ToString());
    }

    [Test]
    public void GetArgumentSyntax_WhenUsingAssignArgumentWithVariable_ShouldGetCode()
    {
        var argument = new AssignArgument("hello", ReferenceGenerator.Create(new VariableReference("myVariable", new MemberReference("hello"))));
        var syntax = argument.GetArgumentSyntax();

        Assert.IsInstanceOf<ArgumentSyntax>(syntax);
        Assert.AreEqual("hello=myVariable.hello", syntax.ToString());
    }
}
