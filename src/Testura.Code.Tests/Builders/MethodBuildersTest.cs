using NUnit.Framework;
using Testura.Code.Builders;
using Testura.Code.Generators.Common;
using Testura.Code.Models;

namespace Testura.Code.Tests.Builders
{
    [TestFixture]
    public class MethodBuildersTest
    {
        [Test]
        public void Build_WhenGivingMethodName_CodeShouldContainName()
        {
            var builder = new MethodBuilder("MyMethod");
            Assert.IsTrue(builder.Build().ToString().Contains("MyMethod()"));
        }

        [Test]
        public void Build_WhenGivingAttribute_CodeShouldContainAttribute()
        {
            var builder = new MethodBuilder("MyMethod");
            Assert.IsTrue(builder.WithAttributes(new Attribute("MyAttribute")).Build().ToString().Contains("[MyAttribute]"));
        }

        [Test]
        public void Build_WhenGivingModifier_CodeShouldContainModifiers()
        {
            var builder = new MethodBuilder("MyMethod");
            Assert.IsTrue(builder.WithModifiers(Modifiers.Public, Modifiers.Abstract).Build().ToString().Contains("publicabstract"));
        }

        [Test]
        public void Build_WhenGivingParameters_CodeShouldContainParamters()
        {
            var builder = new MethodBuilder("MyMethod");
            Assert.IsTrue(builder.WithParameters(new Parameter("myParamter", typeof(int))).Build().ToString().Contains("intmyParamter"));
        }

        [Test]
        public void Build_WhenGivingReturnType_CodeShouldContainReturn()
        {
            var builder = new MethodBuilder("MyMethod");
            Assert.IsTrue(builder.WithReturnType(typeof(int)).Build().ToString().Contains("intMyMethod()"));
        }

        [Test]
        [Ignore("hmm")]
        public void Build_WhenGivingXmlComment_CodeShouldContainComment()
        {
            var builder = new MethodBuilder("MyMethod");
            Assert.IsTrue(builder.WithXmlComments("Test comment").Build().ToString().Contains("///Testcomment"));
        }
    }
}
