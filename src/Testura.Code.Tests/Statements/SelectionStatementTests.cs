using NUnit.Framework;
using Testura.Code.Generators.Common;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;
using Testura.Code.Generators.Common.BinaryExpressions;
using Testura.Code.Models.References;
using Testura.Code.Statements;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Statements;

[TestFixture]
public class SelectionStatementTests
{
    private SelectionStatement conditional;

    [OneTimeSetUp]
    public void SetUp()
    {
        conditional = new SelectionStatement();
    }

    [Test]
    public void If_WhenCreatingAnIfWithEqual_ShouldGenerateCorrectIfStatement()
    {
        Assert.AreEqual(
            "if(2==3){}",
            conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatements.Equal, BodyGenerator.Create()).ToString());
    }

    [Test]
    public void If_WhenCreatingAnIfWithEqualAndExpressionStatement_ShouldGenerateCorrectIfStatementWithoutBraces()
    {
        Assert.AreEqual(
            "if(2==3)MyMethod();",
            conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatements.Equal, Statement.Expression.Invoke("MyMethod").AsStatement()).ToString());
    }

    [Test]
    public void If_WhenCreatingAnIfWithNotEqual_ShouldGenerateCorrectIfStatement()
    {
        Assert.AreEqual(
            "if(2!=3){}",
            conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatements.NotEqual, BodyGenerator.Create()).ToString());
    }

    [Test]
    public void If_WhenCreatingAnIfWithGreaterThan_ShouldGenerateCorrectIfStatement()
    {
        Assert.AreEqual(
            "if(2>3){}",
            conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatements.GreaterThan, BodyGenerator.Create()).ToString());
    }

    [Test]
    public void If_WhenCreatingAnIfWithGreaterThanOrEqual_ShouldGenerateCorrectIfStatement()
    {
        Assert.AreEqual(
            "if(2>=3){}",
            conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatements.GreaterThanOrEqual, BodyGenerator.Create()).ToString());
    }

    [Test]
    public void If_WhenCreatingAnIfWithLessThan_ShouldGenerateCorrectIfStatement()
    {
        Assert.AreEqual(
            "if(2<3){}",
            conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatements.LessThan, BodyGenerator.Create()).ToString());
    }

    [Test]
    public void If_WhenCreatingAnIfWithLessThanOrEqual_ShouldGenerateCorrectIfStatement()
    {
        Assert.AreEqual(
            "if(2<=3){}",
            conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatements.LessThanOrEqual, BodyGenerator.Create()).ToString());
    }

    [Test]
    public void If_WhenCreatingAnBinaryExpression_ShouldGenerateCorrectIfStatement()
    {
        Assert.AreEqual(
            "if(2<=3){}",
            conditional.If(new ConditionalBinaryExpression(new ConstantReference(2), new ConstantReference(3), ConditionalStatements.LessThanOrEqual), BodyGenerator.Create()).ToString());
    }

    [Test]
    public void If_WhenCreatingAnComplexBinaryExpression_ShouldGenerateCorrectIfStatement()
    {
        var leftBinaryExpression = new ConditionalBinaryExpression(
            new ConstantReference(1),
            new ConstantReference(2),
            ConditionalStatements.Equal);

        var rightBinaryExpression = new ConditionalBinaryExpression(
            new ConstantReference(1),
            new ConstantReference(2),
            ConditionalStatements.LessThan);

        var orBinaryExpression = new OrBinaryExpression(
            leftBinaryExpression,
            rightBinaryExpression);

        var binaryExpression = new OrBinaryExpression(
            leftBinaryExpression,
            orBinaryExpression);

        Assert.AreEqual(
            "if(1==2||1==2||1<2){}",
            conditional.If(binaryExpression, BodyGenerator.Create()).ToString());
    }

    [Test]
    public void If_WhenCreatingAnIfWithBinaryExpressionAndExpressionStatement_ShouldGenerateCorrectIfStatementWithoutBraces()
    {
        Assert.AreEqual(
            "if(2==3)MyMethod();",
            conditional.If(new ConditionalBinaryExpression(new ConstantReference(2), new ConstantReference(3), ConditionalStatements.Equal), Statement.Expression.Invoke("MyMethod").AsStatement()).ToString());
    }
}