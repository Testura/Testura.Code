using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;
using Testura.Code.Models.References;
using Testura.Code.Statements;

namespace Testura.Code.Tests.Helper.Common.Arguments.ArgumentTypes
{
    [TestFixture]
    public class InvocationArgumentTests
    {
        [Test]
        public void GetArgumentSyntax_WhenUsingMethod_ShouldGetCode()
        {
            var argument =
                new InvocationArgument(Statement.Expression.Invoke(new MethodReference("Do")).AsInvocationStatment());
            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("Do()", syntax.ToString());
        }
    }
}
