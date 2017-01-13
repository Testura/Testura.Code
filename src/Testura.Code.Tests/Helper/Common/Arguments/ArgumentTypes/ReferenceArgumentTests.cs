using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework.Internal;
using NUnit.Framework;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;
using Testura.Code.Models.References;

namespace Testura.Code.Tests.Helper.Common.Arguments.ArgumentTypes
{
    [TestFixture]
    public class ReferenceArgumentTests
    {
        [Test]
        public void GetArgumentSyntax_WhenUsingVariableReference_ShouldGetCode()
        {
            var argument = new ReferenceArgument(new VariableReference("test"));
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("test", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenUsingMethodReference_ShouldGetCode()
        {
            var argument = new ReferenceArgument(new MethodReference("test"));
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("test()", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenUsingConstReference_ShouldGetCode()
        {
            var argument = new ReferenceArgument(new ConstantReference(1));
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("1", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenUsingNullReference_ShouldGetCode()
        {
            var argument = new ReferenceArgument(new NullReference());
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("null", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenUsingMemberReferenceReference_ShouldGetCode()
        {
            var argument = new ReferenceArgument(new VariableReference("test", new MemberReference("MyProperty")));
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("test.MyProperty", syntax.ToString());
        }
    }
}
