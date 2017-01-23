using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;
using Testura.Code.Statements;

namespace Testura.Code.Tests.Generators.Common.Arguments.ArgumentTypes
{
    [TestFixture]
    public class ParenthesizedLambdaArgumentTests
    {
        [Test]
        public void GetArgumentSyntax_WhenCreatingEmpty_ShouldGetCorrectCode()
        {
            var argument = new ParenthesizedLambdaArgument(Statement.Expression.Invoke("MyMethod").AsExpression());
            var syntax = argument.GetArgumentSyntax();
            
            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("()=>MyMethod()", syntax.ToString());
        }
    }
}
