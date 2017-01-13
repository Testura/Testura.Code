using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Testura.Code.Statements;
using NUnit.Framework;
using Testura.Code.Builders;
using Testura.Code.Generators.Class;
using Testura.Code.Generators.Common;
using Testura.Code.Models;
using Testura.Code.Models.References;

namespace Testura.Code.Tests.Integration
{
    [TestFixture]
    public class ModelClassTests
    {
        [Test]
        public void Test_Modela()
        {
            var classBuilder = new ClassBuilder("Cat", "Models");
            var @class = classBuilder
                .WithUsings("System")
                .WithProperties(PropertyGenerator.Create(new Property("Name", typeof(string), PropertyTypes.GetAndSet)),
                    PropertyGenerator.Create(new Property("Age", typeof(int), PropertyTypes.GetAndSet)))
                .WithConstructor(ClassGenerator.Constructor("Cat",
                BodyGenerator.Create(
                            Statement.Decleration.Assign("Name", ReferenceGenerator.Create(new VariableReference("name"))),
                            Statement.Decleration.Assign("Age", ReferenceGenerator.Create(new VariableReference("age")))
                            ),
                new List<Parameter> { new Parameter("name", typeof(string)), new Parameter("age", typeof(int)) }))
                       .Build(); 
            var m = @class.NormalizeWhitespace().ToString();
        }

        [Test]
        public void Test_Access_Model()
        {
            var classBuilder = new ClassBuilder("CatFarm", "Models");
            var @class = classBuilder
                .WithUsings("System")
                .WithMethods(
                    new MethodBuilder("Meow")
                        .WithParameters(new Parameter("cat", "Cat"))
                        .WithBody(BodyGenerator.Create(
                                Statement.Decleration.DeclareAndAssign("meow", typeof(string), new VariableReference("cat", new MemberReference("MyProperty")))
                            ))
                        .Build()
                ).Build();
            var m = @class.NormalizeWhitespace().ToString();
        }
    }
}
