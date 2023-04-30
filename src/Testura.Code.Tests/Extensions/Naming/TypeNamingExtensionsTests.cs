using System.Collections.Generic;
using NUnit.Framework;
using Testura.Code.Extensions.Naming;
using Testura.Code.Models.Types;

namespace Testura.Code.Tests.Extensions.Naming;

[TestFixture]
public class TypeNamingExtensionsTests
{
    [Test]
    public void FormattedTypeName_WhenHavingValueType_ShouldHaveCorrectName()
    {
        Assert.AreEqual("int", typeof(int).FormattedTypeName());
    }

    [Test]
    public void FormattedTypeName_WhenHavingCustomType_ShouldHaveCorrectName()
    {
        Assert.AreEqual("test", CustomType.Create("test").FormattedTypeName());
    }

    [Test]
    public void FormattedTypeName_WhenHavingGenericType_ShouldHaveCorrectName()
    {
        Assert.AreEqual("List<int>", typeof(List<int>).FormattedTypeName());
    }

    [Test]
    public void FormattedFieldName_WhenHavingGenericType_ShouldHaveCorrectName()
    {
        Assert.AreEqual("list", typeof(List<int>).FormattedFieldName());
    }

    [Test]
    public void FormattedClassName_WhenHavingGenericType_ShouldHaveCorrectName()
    {
        Assert.AreEqual("List", typeof(List<int>).FormattedClassName());
    }
}
