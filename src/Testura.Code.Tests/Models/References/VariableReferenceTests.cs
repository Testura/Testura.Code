using NUnit.Framework;
using Testura.Code.Models.References;

namespace Testura.Code.Tests.Models.References;

[TestFixture]
public class VariableReferenceTests
{
    [Test]
    public void GetLastMember_WhenHavingNoChild_ShouldReturnNull()
    {
        Assert.IsNull(new VariableReference("test").GetLastMember());
    }

    [Test]
    public void GetLastMember_WhenHavingMember_ShouldReturnMember()
    {
        var memberReference = new MemberReference("test");

        Assert.AreSame(memberReference, new VariableReference("test", memberReference).GetLastMember());
    }

    [Test]
    public void GetLastMember_WhenHavingChainOfMember_ShouldReturnLastMemberInChain()
    {
        var memberReference = new MemberReference("test");

        Assert.AreSame(memberReference, new VariableReference("test", new MethodReference("test", memberReference)).GetLastMember());
    }
}