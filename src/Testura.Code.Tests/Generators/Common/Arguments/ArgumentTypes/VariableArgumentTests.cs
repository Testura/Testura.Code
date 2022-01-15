using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;

namespace Testura.Code.Tests.Generators.Common.Arguments.ArgumentTypes;

[TestFixture]
public class VariableArgumentTests
{
    [Test]
    public void GetArgumentSyntax_WhenUsingNormalValue_ShouldGetCode()
    {
        var argument = new VariableArgument("variableName");
        var syntax = argument.GetArgumentSyntax();

        Assert.IsInstanceOf<ArgumentSyntax>(syntax);
        Assert.AreEqual("variableName", syntax.ToString());
    }

    [Test]
    public void GetArgumentSyntax_WhenUsingNormalValueAsNamedArgument_ShouldGetCode()
    {
        var argument = new VariableArgument("variableName", "namedArgument");
        var syntax = argument.GetArgumentSyntax();

        Assert.IsInstanceOf<ArgumentSyntax>(syntax);
        Assert.AreEqual("namedArgument:variableName", syntax.ToString());
    }
}