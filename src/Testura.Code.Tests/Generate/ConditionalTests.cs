using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using Testura.Code.Generate;
using Testura.Code.Generate.ArgumentTypes;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Generate
{
    [TestFixture]
    public class ConditionalTests
    {
        [Test]
        public void If_WhenCreatingAnIfWithEqual_ShouldGenerateCorrectIfStatement()
        {
            Assert.AreEqual("if(2==3){}",
                Conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatement.Equal, Body.Create(new List<StatementSyntax>())).ToString());
        }

        [Test]
        public void If_WhenCreatingAnIfWithNotEqual_ShouldGenerateCorrectIfStatement()
        {
            Assert.AreEqual("if(2!=3){}",
                Conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatement.NotEqual, Body.Create(new List<StatementSyntax>())).ToString());
        }

        [Test]
        public void If_WhenCreatingAnIfWithGreaterThan_ShouldGenerateCorrectIfStatement()
        {
            Assert.AreEqual("if(2>3){}",
                Conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatement.GreaterThan, Body.Create(new List<StatementSyntax>())).ToString());
        }

        [Test]
        public void If_WhenCreatingAnIfWithGreaterThanOrEqual_ShouldGenerateCorrectIfStatement()
        {
            Assert.AreEqual("if(2>=3){}",
                Conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatement.GreaterThanOrEqual, Body.Create(new List<StatementSyntax>())).ToString());
        }

        [Test]
        public void If_WhenCreatingAnIfWithLessThan_ShouldGenerateCorrectIfStatement()
        {
            Assert.AreEqual("if(2<3){}",
                Conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatement.LessThan, Body.Create(new List<StatementSyntax>())).ToString());
        }

        [Test]
        public void If_WhenCreatingAnIfWithLessThanOrEqual_ShouldGenerateCorrectIfStatement()
        {
            Assert.AreEqual("if(2<=3){}",
                Conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatement.LessThanOrEqual, Body.Create(new List<StatementSyntax>())).ToString());
        }
    }
}
