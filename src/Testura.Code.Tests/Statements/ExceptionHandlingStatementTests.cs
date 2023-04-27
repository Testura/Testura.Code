using System;
using Microsoft.CodeAnalysis.CSharp;
using NUnit.Framework;
using Testura.Code.Generators.Common;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;
using Testura.Code.Models.Types;
using Testura.Code.Statements;

namespace Testura.Code.Tests.Statements;

[TestFixture]
public class ExceptionHandlingStatementTests
{
    private ExceptionHandlingStatement exceptionHandlingStatement;

    [OneTimeSetUp]
    public void SetUp()
    {
        exceptionHandlingStatement = new ExceptionHandlingStatement();
    }

    [Test]
    public void TryCatch_WhenCreatingTryCatch_ShouldCreateTryCatch()
    {
        Assert.AreEqual("try{}catch(Exceptione){}", exceptionHandlingStatement.TryCatch(SyntaxFactory.Block(), SyntaxFactory.Block(), typeof(Exception), "e").ToString());
    }

    [Test]
    public void TryCatch_WhenCreatingTryCatchWithCustomType_ShouldCreateTryCatch()
    {
        Assert.AreEqual("try{}catch(SomeOtherExceptione){}", exceptionHandlingStatement.TryCatch(SyntaxFactory.Block(), SyntaxFactory.Block(), CustomType.Create("SomeOtherException"), "e").ToString());
    }

    [Test]
    public void Throw_WhenCreatingThrow_ShouldCreateThrow()
    {
        Assert.AreEqual("throw;", exceptionHandlingStatement.Throw().ToString());
    }

    [Test]
    public void ThrowNew_WhenCreatingThrow_ShouldCreateThrow()
    {
        Assert.AreEqual("thrownewException();", exceptionHandlingStatement.ThrowNew(typeof(Exception)).ToString());
    }

    [Test]
    public void ThrowNew_WhenCreatingThrowWithArguments_ShouldCreateThrow()
    {
        Assert.AreEqual("thrownewException(\"test\");", exceptionHandlingStatement.ThrowNew(typeof(Exception), new ValueArgument("test")).ToString());
    }

    [Test]
    public void ThrowNew_WhenCreatingThrowNewWithObjectInitializer_ShouldCreateThrow()
    {
        Assert.AreEqual("thrownewException();", exceptionHandlingStatement.ThrowNew(ObjectCreationGenerator.Create(typeof(Exception))).ToString());
    }
}
