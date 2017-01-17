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
        public void Test_CreateModelClass()
        {
            var classBuilder = new ClassBuilder("Cat", "Models");
            var @class = classBuilder
                .WithUsings("System")
                .WithProperties(
                    PropertyGenerator.Create(new Property("Name", typeof(string), PropertyTypes.GetAndSet, new List<Modifiers> { Modifiers.Public } )), 
                    PropertyGenerator.Create(new Property("Age", typeof(int), PropertyTypes.GetAndSet, new List<Modifiers> { Modifiers.Public })))
                .WithConstructor(
                    ConstructorGenerator.Create(
                        "Cat",
                        BodyGenerator.Create(
                            Statement.Decleration.Assign("Name", ReferenceGenerator.Create(new VariableReference("name"))),
                            Statement.Decleration.Assign("Age", ReferenceGenerator.Create(new VariableReference("age")))),
                        new List<Parameter> { new Parameter("name", typeof(string)), new Parameter("age", typeof(int)) },
                        new List<Modifiers> { Modifiers.Public }))

                       .Build();
            Assert.AreEqual(
                "usingSystem;namespaceModels{publicclassCat{publicCat(stringname,intage){Name=name;Age=age;}publicstringName{get;set;}publicintAge{get;set;}}}",
                @class.ToString());
        }
    }
}
