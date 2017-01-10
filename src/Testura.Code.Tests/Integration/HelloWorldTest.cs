using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NUnit.Framework;
using Testura.Code.Builders;
using Testura.Code.Helpers;
using Testura.Code.Helpers.Common;
using Testura.Code.Helpers.Common.Arguments;
using Testura.Code.Helpers.Common.Arguments.ArgumentTypes;
using Testura.Code.Helpers.Common.References;
using Testura.Code.Models;
using Testura.Code.Statements;

namespace Testura.Code.Tests.Integration
{
    [TestFixture]
    public class HelloWorldTest
    {
        [Test]
        public void Test_HelloWorld()
        {
            var classBuilder = new ClassBuilder("Program", "HelloWorld");
            var @class = classBuilder
                .WithUsings("System")
                .WithMethods(new MethodBuilder("Main")
                    .WithParameters(Parameters.Create(new Parameter("args", typeof(string[]))))
                    .WithModifiers(Helpers.Common.Modifiers.Create(Modifiers.Public, Modifiers.Static))
                    .WithBody(Body.Create(
                        Statement.Expression.Invoke(new VariableReference("Console", new MethodReference("WriteLine", new List<IArgument>() { new ValueArgument("Hello world") }))).AsExpressionStatement(),
                        Statement.Expression.Invoke("Console", "ReadLine", Argument.Create()).AsExpressionStatement()
                        ))
                    .Build())
                .Build();
            var m = @class.NormalizeWhitespace().ToString();
        }
    }
}
