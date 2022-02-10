using System.Linq;
using NUnit.Framework;
using Testura.Code.Generators.Common;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;
using Testura.Code.Models.References;
using Testura.Code.Models.Types;
using Testura.Code.Statements;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Statements;

[TestFixture]
public class JumpStatementTests
{
    private JumpStatement @return;

    [SetUp]
    public void SetUp()
    {
        @return = new JumpStatement();
    }

    [Test]
    public void ReturnTrue_WhenReturnTrue_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual("returntrue;", @return.ReturnTrue().ToString());
    }

    [Test]
    public void ReturnFalse_WhenReturnFalse_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual("returnfalse;", @return.ReturnFalse().ToString());
    }

    [Test]
    public void Return_WhenReturnReference_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual("returni;", @return.Return(new VariableReference("i")).ToString());
    }

    [Test]
    public void Return_WhenReturnExpression_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual("returntest();", @return.Return(Statement.Expression.Invoke("test").AsExpression()).ToString());
    }

    [Test]
    public void Return_WhenReturnThis_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual("returnthis;", @return.ReturnThis().ToString());
    }

    [Test]
    public void Return_WhenReturnNewObject_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual("returnnewHello(1,\"test\");", @return.Return(ObjectCreationGenerator.Create(CustomType.Create("Hello"), new[] { new ValueArgument(1), new ValueArgument("test") }) ).ToString());
    }

    [Test]
    public void Return_WhenReturnNewObjectWithoutParameters_ShouldGenerateCorrectCode()
    {
        Assert.AreEqual("returnnewHello();", @return.Return(ObjectCreationGenerator.Create(CustomType.Create("Hello"))).ToString());
    }

    [Test]
    public void Return_WhenReturnNewObjectWithInitializer_ShouldGenerateCorrectCode()
    {
        var initializers = new[]
        {
            Statement.Declaration.Assign("hej", new VariableReference("test"))
        };

        var objectCreation = ObjectCreationGenerator.Create(CustomType.Create("Hello"), initialization: initializers.Select(i => i.Expression));

        Assert.AreEqual("returnnewHello{hej=test};", @return.Return(objectCreation).ToString());
    }
}
