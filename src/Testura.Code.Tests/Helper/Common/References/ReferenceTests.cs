using System.Collections.Generic;
using NUnit.Framework;
using Testura.Code.Helpers.Common.Arguments;
using Testura.Code.Helpers.Common.Arguments.ArgumentTypes;
using Testura.Code.Helpers.Common.References;
using Assert = NUnit.Framework.Assert;
using IArgument = Testura.Code.Helpers.Common.Arguments.ArgumentTypes.IArgument;

namespace Testura.Code.Tests.Helper.Common.References
{
    [TestFixture]
    public class ReferenceTests
    {
        [Test]
        public void Create_WhenCreatingVariableRefernce_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("myVariable", Reference.Create(new VariableReference("myVariable")).ToString());
        }

        [Test]
        public void Create_WhenCreatingVariableRefernceWithMethodMember_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("myVariable.MyMethod()", Reference.Create(new VariableReference("myVariable", new MethodReference("MyMethod", new List<IArgument>()))).ToString());
        }

        [Test]
        public void Create_WhenCreatingVariableRefernceWithMethodMemberThatHasArgument_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("myVariable.MyMethod(1,\"test\")", Reference.Create(new VariableReference("myVariable", new MethodReference("MyMethod", new List<IArgument> { new ValueArgument(1), new ValueArgument("test")}))).ToString());
        }

        [Test]
        public void Create_WhenCreatingVariableRefernceWithFieldMember_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("myVariable.field", Reference.Create(new VariableReference("myVariable", new MemberReference("field"))).ToString());
        }

        [Test]
        public void Create_WhenCreatingVariableRefernceWithPropertyMember_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("myVariable.Property", Reference.Create(new VariableReference("myVariable", new MemberReference("Property"))).ToString());
        }

        [Test]
        public void Create_WhenCreatingVariableRefernceWithChainedMembers_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("myVariable.MyMethod().MyProperty", Reference.Create(new VariableReference("myVariable", new MethodReference("MyMethod", new List<IArgument>(), new MemberReference("MyProperty")))).ToString());
        }
    }
}
