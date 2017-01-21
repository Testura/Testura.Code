using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Testura.Code.Statements;
using NUnit.Framework;
using Testura.Code.Builders;
using Testura.Code.Compilations;
using Testura.Code.Generators.Class;
using Testura.Code.Generators.Common;
using Testura.Code.Models;
using Testura.Code.Models.Options;
using Testura.Code.Models.Properties;
using Testura.Code.Models.References;
using Testura.Code.Saver;

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
                    PropertyGenerator.Create(new AutoProperty("Name", typeof(string), PropertyTypes.GetAndSet, new List<Modifiers> { Modifiers.Public } )), 
                    PropertyGenerator.Create(new AutoProperty("Age", typeof(int), PropertyTypes.GetAndSet, new List<Modifiers> { Modifiers.Public })))
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

        [Test]
        public void Test_CreateModelClassWithBodyProperties()
        {
            var classBuilder = new ClassBuilder("Cat", "Models");
            var @class = classBuilder
                .WithUsings("System")
                .WithFields(
                    new Field("_name", typeof(string), new List<Modifiers>() { Modifiers.Private}),
                    new Field("_age", typeof(int), new List<Modifiers>() { Modifiers.Private }))
                .WithProperties(
                    PropertyGenerator.Create(
                        new BodyProperty(
                            "Name", 
                            typeof(string), 
                            BodyGenerator.Create(Statement.Jump.Return(new VariableReference("_name"))), BodyGenerator.Create(Statement.Decleration.Assign("_name", new ValueKeywordReference())),
                            new List<Modifiers> { Modifiers.Public })),
                    PropertyGenerator.Create(
                        new BodyProperty(
                            "Age",
                            typeof(int),
                            BodyGenerator.Create(Statement.Jump.Return(new VariableReference("_age"))), BodyGenerator.Create(Statement.Decleration.Assign("_age", new ValueKeywordReference())),
                            new List<Modifiers> { Modifiers.Public })))
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
                "usingSystem;namespaceModels{publicclassCat{privatestring_name;privateint_age;publicCat(stringname,intage){Name=name;Age=age;}publicstringName{get{return_name;}set{_name=value;}}publicintAge{get{return_age;}set{_age=value;}}}}",
                @class.ToString());
        }
    }
}
