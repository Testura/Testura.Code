using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using NUnit.Framework;
using Testura.Code.Builders;
using Testura.Code.Models.Options;
using Testura.Code.Saver;

namespace Testura.Code.Tests.Saver;

[TestFixture]
public class CodeSaverTests
{
    private CodeSaver _coderSaver;

    [OneTimeSetUp]
    public void SetUp()
    {
        _coderSaver = new CodeSaver();
    }

    [Test]
    public void SaveCodeAsString_WhenSavingCodeAsString_ShouldGetString()
    {
        var code = _coderSaver.SaveCodeAsString(new ClassBuilder("TestClass", "test").Build());
        Assert.IsNotNull(code);
        Assert.AreEqual("namespace test\r\n{\r\n    public class TestClass\r\n    {\r\n    }\r\n}", code);
    }

    [Test]
    public void SaveCodeAsString_WhenSavingCodeAsStringAndOptions_ShouldGetString()
    {
        var codeSaver = new CodeSaver(new List<OptionKeyValue> { new OptionKeyValue(CSharpFormattingOptions.NewLinesForBracesInMethods, false) });
        var code = codeSaver.SaveCodeAsString(
            new ClassBuilder("TestClass", "test")
                .WithMethods(
                    new MethodBuilder("MyMethod")
                        .Build())
                .Build());
        Assert.IsNotNull(code);
        Assert.AreEqual("namespace test\r\n{\r\n    public class TestClass\r\n    {\r\n        void MyMethod() {\r\n        }\r\n    }\r\n}", code);
    }
}