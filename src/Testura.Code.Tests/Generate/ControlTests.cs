using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using Testura.Code.Generate;
using Testura.Code.Reference;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Generate
{
    [TestFixture]
    public class ControlTests
    {
        [Test]
        public void For_WhenCreatingForLoopWithStartAndEnd_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("for(inti=1;i<2;i++){}",
                Control.For(1, 2, "i", Body.Create(new List<StatementSyntax>())).ToString());
        }

        [Test]
        public void For_WhenCreatingForLoopWithVariableReferences_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("for(inti=0;i<myClass.MyProperty;i++){}",
                Control.For(new ConstantReference(0), new VariableReference("myClass", new MemberReference("MyProperty", MemberReferenceTypes.Property)), "i", Body.Create(new List<StatementSyntax>())).ToString());
        }
    }
}
