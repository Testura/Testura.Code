using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Builders.BuildMembers;
using Testura.Code.Generators.Class;
using Testura.Code.Models;

namespace Testura.Code.Builders
{
    /// <summary>
    /// Provides a builder to generate a class.
    /// </summary>
    public class ClassBuilder : TypeBuilderBase<ClassBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassBuilder"/> class.
        /// </summary>
        /// <param name="name">Name of the class.</param>
        /// <param name="namespace">Name of the class namespace.</param>
        public ClassBuilder(string name, string @namespace)
         : base(name, @namespace)
        {
        }

        /// <summary>
        /// Add class fields.
        /// </summary>
        /// <param name="fields">A set of wanted fields.</param>
        /// <returns>The current class builder</returns>
        public ClassBuilder WithFields(params Field[] fields)
        {
            Members.Add(new FieldMember(fields.Select(FieldGenerator.Create)));
            return this;
        }

        /// <summary>
        /// Add class fields.
        /// </summary>
        /// <param name="fields">An array of already declared fields.</param>
        /// <returns>The current class builder</returns>
        public ClassBuilder WithFields(params FieldDeclarationSyntax[] fields)
        {
            Members.Add(new FieldMember(fields));
            return this;
        }

        /// <summary>
        /// Add class constructor.
        /// </summary>
        /// <param name="constructor">An already generated constructor.</param>
        /// <returns>The current class builder</returns>
        public ClassBuilder WithConstructor(params ConstructorDeclarationSyntax[] constructor)
        {
            Members.Add(new ConstructorMember(constructor));
            return this;
        }

        /// <summary>
        /// Build the class and return the generated code.
        /// </summary>
        /// <returns>The generated class.</returns>
        public CompilationUnitSyntax Build()
        {
            var members = default(SyntaxList<MemberDeclarationSyntax>);

            foreach (var member in Members)
            {
                members = member.AddMember(members);
            }

            var @class = BuildClassBase();
            @class = BuildAttributes(@class);
            @class = @class.WithMembers(members);
            var @base = SyntaxFactory.CompilationUnit();
            @base = BuildUsings(@base);
            @base = BuildNamespace(@base, @class);
            return @base;
        }

        private TypeDeclarationSyntax BuildClassBase()
        {
            return SyntaxFactory.ClassDeclaration(Name).WithBaseList(CreateBaseList()).WithModifiers(CreateModifiers());
        }
    }
}
