using NUnit.Framework;
using Testura.Code.Statements;

namespace Testura.Code.Tests.Statements
{
    [TestFixture]
    public class StatementTests
    {
        [Test]
        public void Declaration_WhenGettingDeclaration_ShouldNotBeNull()
        {
            Assert.IsNotNull(Statement.Declaration);
        }

        [Test]
        public void Expression_WhenGettingExpression_ShouldNotBeNull()
        {
            Assert.IsNotNull(Statement.Expression);
        }

        [Test]
        public void Iteration_WhenGettingIteration_ShouldNotBeNull()
        {
            Assert.IsNotNull(Statement.Iteration);
        }

        [Test]
        public void Jump_WhenGettingJump_ShouldNotBeNull()
        {
            Assert.IsNotNull(Statement.Jump);
        }

        [Test]
        public void Selection_WhenGettingSelection_ShouldNotBeNull()
        {
            Assert.IsNotNull(Statement.Selection);
        }
    }
}
