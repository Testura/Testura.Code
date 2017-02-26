using System.Collections.Generic;
using NUnit.Framework;
using Testura.Code.Builders;
using Testura.Code.Generators.Common;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;
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
                .WithModifiers(Modifiers.Public)
                .WithMethods(
                    new MethodBuilder("Main")
                    .WithParameters(new Parameter("args", typeof(string[])))
                    .WithBody(
                        BodyGenerator.Create(
                            Statement.Expression.Invoke("Console", "WriteLine", new List<IArgument>() { new ValueArgument("Hello world") }).AsStatement(),
                            Statement.Expression.Invoke("Console", "ReadLine").AsStatement()
                            ))
                    .WithModifiers(Modifiers.Public, Modifiers.Static)
                    .Build())
                .Build();

            Assert.AreEqual(
                @"usingSystem;namespaceHelloWorld{publicclassProgram{publicstaticvoidMain(string[]args){Console.WriteLine(""Hello world"");Console.ReadLine();}}}",
                @class.ToString());
        }
    }
}
