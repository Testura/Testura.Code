using NUnit.Framework;
using Testura.Code.Generators.Common.BinaryExpressions;
using Testura.Code.Models.References;

namespace Testura.Code.Tests.Generators.Common.BinaryExpressions;

[TestFixture]
public class ConditionalBinaryExpressionTests
{
    [Test]
    public void GetBinaryExpression_WhenHavingTwoReferencesAndEqual_ShouldGenerateCode()
    {
        var binaryExpression = new ConditionalBinaryExpression(
            new ConstantReference(1),
            new ConstantReference(2),
            ConditionalStatements.Equal);
        Assert.AreEqual("1==2", binaryExpression.GetBinaryExpression().ToString());
    }
}