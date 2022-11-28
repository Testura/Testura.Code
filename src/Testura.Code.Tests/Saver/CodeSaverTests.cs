namespace Testura.Code.Tests.Saver;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Code.Builders;
using Code.Models.Options;
using Code.Saver;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using NUnit.Framework;

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
    public async Task SaveCodeToFileAsync_WhenSavingCodeAsFile_ShouldSaveCorrectly()
    {
        var cts = new CancellationTokenSource();
        var destFile = PrepareDestinationFile();
        var compiledCode = new ClassBuilder("TestClass", "test").Build();
        await _coderSaver.SaveCodeToFileAsync(compiledCode, destFile.FullName, cts.Token);
        Assert.IsTrue(File.Exists(destFile.FullName));
        var code = await File.ReadAllTextAsync(destFile.FullName, cts.Token);
        Assert.IsNotNull(code);
        Assert.AreEqual(
            "namespace test\r\n{\r\n    public class TestClass\r\n    {\r\n    }\r\n}",
            code);

        FileInfo PrepareDestinationFile()
        {
            var fi = GetDestinationFile();
            if (fi.Exists)
            {
                fi.Delete();
            }

            return fi;
        }

        // Returns a temporary predictable file name which will be saved to the filesystem for testing.
        // TODO: Use a filesystem abstraction library to avoid saving to file system.
        FileInfo GetDestinationFile()
        {
            var exampleFileName =
                nameof(SaveCodeToFileAsync_WhenSavingCodeAsFile_ShouldSaveCorrectly);
            var destinationFile = Path.Combine(
                Environment.CurrentDirectory,
                "UnitTests",
                "Saver",
                $"{exampleFileName}.cs");

            var fi = new FileInfo(destinationFile);
            Directory.CreateDirectory(fi.Directory.FullName);
            if (fi.Exists)
            {
                fi.Delete();
            }

            return fi;
        }
    }

    [Test]
    public void SaveCodeAsString_WhenSavingCodeAsString_ShouldGetString()
    {
        var code = _coderSaver.SaveCodeAsString(new ClassBuilder("TestClass", "test").Build());
        Assert.IsNotNull(code);
        Assert.AreEqual(
            "namespace test\r\n{\r\n    public class TestClass\r\n    {\r\n    }\r\n}",
            code);
    }

    [Test]
    public void SaveCodeAsString_WhenSavingCodeAsStringAndOptions_ShouldGetString()
    {
        var codeSaver = new CodeSaver(
            new List<OptionKeyValue>
            {
                new(CSharpFormattingOptions.NewLinesForBracesInMethods, false)
            });
        var code = codeSaver.SaveCodeAsString(
            new ClassBuilder("TestClass", "test").WithMethods(new MethodBuilder("MyMethod").Build())
                .Build());
        Assert.IsNotNull(code);
        Assert.AreEqual(
            "namespace test\r\n{\r\n    public class TestClass\r\n    {\r\n        void MyMethod() {\r\n        }\r\n    }\r\n}",
            code);
    }
}


