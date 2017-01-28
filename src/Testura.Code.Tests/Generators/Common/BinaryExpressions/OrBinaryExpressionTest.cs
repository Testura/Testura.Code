using NUnit.Framework;
using Testura.Code.Generators.Common.BinaryExpressions;
using Testura.Code.Models.References;

namespace Testura.Code.Tests.Generators.Common.BinaryExpressions
{
    [TestFixture]
    public class OrBinaryExpressionTest
    {
        [Test]
        public void GetBinaryExpression_WhenHavingTwoConditionalBinaryExpressions_ShouldGenerateCode()
        {
            var leftBinaryExpression = new ConditionalBinaryExpression(
                new ConstantReference(1),
                new ConstantReference(2),
                ConditionalStatements.Equal);

            var rightBinaryExpression = new ConditionalBinaryExpression(
                new ConstantReference(1),
                new ConstantReference(2),
                ConditionalStatements.LessThan);

            var binaryExpression = new OrBinaryExpression(
                leftBinaryExpression,
                rightBinaryExpression);
            Assert.AreEqual("1==2||1<2", binaryExpression.GetBinaryExpression().ToString());
        }

        [Test]
        public void GetBinaryExpression_WhenHavingAnotherOrExpression_ShouldGenerateCode()
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

            Assert.AreEqual("1==2||1==2||1<2", binaryExpression.GetBinaryExpression().ToString());
        }

        [Test]
        public void GetBinaryExpression_WhenHavingAnotherAndExpression_ShouldGenerateCode()
        {
            var leftBinaryExpression = new ConditionalBinaryExpression(
                new ConstantReference(1),
                new ConstantReference(2),
                ConditionalStatements.Equal);

            var rightBinaryExpression = new ConditionalBinaryExpression(
                new ConstantReference(1),
                new ConstantReference(2),
                ConditionalStatements.LessThan);

            var orBinaryExpression = new AndBinaryExpression(
                leftBinaryExpression,
                rightBinaryExpression);

            var binaryExpression = new OrBinaryExpression(
                leftBinaryExpression,
                orBinaryExpression);

            Assert.AreEqual("1==2||1==2&&1<2", binaryExpression.GetBinaryExpression().ToString());
        }
    }
}
