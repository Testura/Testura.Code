using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework.Internal;
using NUnit.Framework;
using Testura.Code.Builder;
using Testura.Code.Generate;
using Testura.Code.Generate.ArgumentTypes;
using Testura.Code.Reference;

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
                    .WithModifiers(Code.Generate.Modifiers.Create(Modifiers.Public, Modifiers.Static))
                    .WithBody(Body.Create(
                        SyntaxFactory.ExpressionStatement(References.Create(new VariableReference("Console", new MethodReference("WriteLine", new List<IArgument>() { new ValueArgument("Hello world", ArgumentType.String) })))),
                        Method.Invoke("Console", "ReadLine", Argument.Create()).AsExpressionStatement()
                        ))
                    .Build())
                .Build();
            var m = @class.NormalizeWhitespace().ToString();
        }
    }
}
