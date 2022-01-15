using NUnit.Framework;
using Testura.Code.Generators.Common.BinaryExpressions;
using Testura.Code.Models.References;

namespace Testura.Code.Tests.Generators.Common.BinaryExpressions;

[TestFixture]
public class MathBinaryExpressionTests
{
    [Test]
    public void GetBinaryExpression_WhenAddingTwoReferences_ShouldGenerateCode()
    {
        var binaryExpression = new MathBinaryExpression(new ConstantReference(1), new ConstantReference(2), MathOperators.Add);
        Assert.AreEqual("1+2", binaryExpression.GetBinaryExpression().ToString());
    }

    [Test]
    public void GetBinaryExpression_WhenSubtractTwoReferences_ShouldGenerateCode()
    {
        var binaryExpression = new MathBinaryExpression(new ConstantReference(1), new ConstantReference(2), MathOperators.Subtract);
        Assert.AreEqual("1-2", binaryExpression.GetBinaryExpression().ToString());
    }

    [Test]
    public void GetBinaryExpression_WhenMultiplyTwoReferences_ShouldGenerateCode()
    {
        var binaryExpression = new MathBinaryExpression(new ConstantReference(1), new ConstantReference(2), MathOperators.Multiply);
        Assert.AreEqual("1*2", binaryExpression.GetBinaryExpression().ToString());
    }

    [Test]
    public void GetBinaryExpression_WhenDivideTwoReferences_ShouldGenerateCode()
    {
        var binaryExpression = new MathBinaryExpression(new ConstantReference(1), new ConstantReference(2), MathOperators.Divide);
        Assert.AreEqual("1/2", binaryExpression.GetBinaryExpression().ToString());
    }

    [Test]
    public void GetBinaryExpression_WhenUsingMultipleMathBinaryExpression_ShouldGenerateCode()
    {
        var rightBinaryExpression = new MathBinaryExpression(new ConstantReference(3), new ConstantReference(5), MathOperators.Add);
        var binaryExpression = new MathBinaryExpression(new ConstantReference(1), rightBinaryExpression, MathOperators.Divide);
        Assert.AreEqual("1/3+5", binaryExpression.GetBinaryExpression().ToString());
    }

    [Test]
    public void GetBinaryExpression_WhenUsingMultipleMathBinaryExpressionAndUsingParentheses_ShouldGenerateCode()
    {
        var rightBinaryExpression = new MathBinaryExpression(new ConstantReference(3), new ConstantReference(5), MathOperators.Add, true);
        var binaryExpression = new MathBinaryExpression(new ConstantReference(1), rightBinaryExpression, MathOperators.Divide);
        Assert.AreEqual("1/(3+5)", binaryExpression.GetBinaryExpression().ToString());
    }
}