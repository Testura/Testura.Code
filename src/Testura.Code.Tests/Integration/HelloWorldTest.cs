using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NUnit.Framework;
using Testura.Code.Builder;
using Testura.Code.Helper;
using Testura.Code.Helper.Arguments;
using Testura.Code.Helper.Arguments.ArgumentTypes;
using Testura.Code.Helper.References;
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
                    .WithModifiers(Helper.Modifiers.Create(Modifiers.Public, Modifiers.Static))
                    .WithBody(Body.Create(
                        Statement.Expression.Invoke(new VariableReference("Console", new MethodReference("WriteLine", new List<IArgument>() { new ValueArgument("Hello world", ArgumentType.String) }))).AsExpressionStatement(),
                        Statement.Expression.Invoke("Console", "ReadLine", Argument.Create()).AsExpressionStatement()
                        ))
                    .Build())
                .Build();
            var m = @class.NormalizeWhitespace().ToString();
        }
    }
}
