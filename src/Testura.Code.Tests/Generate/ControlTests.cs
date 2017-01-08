using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using Testura.Code.Helper;
using Testura.Code.Helper.References;
using Testura.Code.Statements;
using Assert = NUnit.Framework.Assert;

namespace Testura.Code.Tests.Generate
{
    [TestFixture]
    public class ControlTests
    {
        private IterationStatement control;

        [OneTimeSetUp]
        public void SetUp()
        {
            control = new IterationStatement();
        }

        [Test]
        public void For_WhenCreatingForLoopWithStartAndEnd_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("for(inti=1;i<2;i++){}",
                control.For(1, 2, "i", Body.Create()).ToString());
        }

        [Test]
        public void For_WhenCreatingForLoopWithVariableReferences_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("for(inti=0;i<myClass.MyProperty;i++){}",
                control.For(new ConstantReference(0), new VariableReference("myClass", new MemberReference("MyProperty", MemberReferenceTypes.Property)), "i", Body.Create()).ToString());
        }
    }
}
