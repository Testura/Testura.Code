using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Class;
using Testura.Code.Models;

namespace Testura.Code.Builders
{
    /// <summary>
    /// Provides a builder to generate a class
    /// </summary>
    public class ClassBuilder : TypeBuilderBase<ClassBuilder>
    {
        private readonly List<FieldDeclarationSyntax> _fields;
        private ConstructorDeclarationSyntax _constructor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassBuilder"/> class.
        /// </summary>
        /// <param name="name">Name of the class.</param>
        /// <param name="namespace">Name of the class namespace.</param>
        public ClassBuilder(string name, string @namespace)
         : base(name, @namespace)
        {
            _fields = new List<FieldDeclarationSyntax>();
        }

        /// <summary>
        /// Set class fields.
        /// </summary>
        /// <param name="fields">A set of wanted fields.</param>
        /// <returns>The current class builder</returns>
        public ClassBuilder WithFields(params Field[] fields)
        {
            _fields.Clear();
            foreach (var field in fields)
            {
                _fields.Add(FieldGenerator.Create(field));
            }

            return this;
        }

        /// <summary>
        /// Set class fields.
        /// </summary>
        /// <param name="fields">An array of already declared fields.</param>
        /// <returns>The current class builder</returns>
        public ClassBuilder WithFields(params FieldDeclarationSyntax[] fields)
        {
            _fields.Clear();
            _fields.AddRange(fields);
            return this;
        }

        /// <summary>
        /// Set class constructor.
        /// </summary>
        /// <param name="constructor">An already generated constructor.</param>
        /// <returns>The current class builder</returns>
        public ClassBuilder WithConstructor(ConstructorDeclarationSyntax constructor)
        {
            _constructor = constructor;
            return this;
        }

        /// <summary>
        /// Build the class and return the generated code.
        /// </summary>
        /// <returns>The generated class.</returns>
        public CompilationUnitSyntax Build()
        {
            var members = default(SyntaxList<MemberDeclarationSyntax>);
            members = BuildFields(members);
            members = BuildConstructor(members);
            members = BuildProperties(members);
            members = BuildMethods(members);
            var @class = BuildClassBase();
            @class = BuildAttributes(@class);
            @class = @class.WithMembers(members);
            var @base = SyntaxFactory.CompilationUnit();
            @base = BuildUsings(@base);
            @base = BuildNamespace(@base, @class);
            return @base;
        }

        private SyntaxList<MemberDeclarationSyntax> BuildFields(SyntaxList<MemberDeclarationSyntax> members)
        {
            return AddMembers(members, _fields);
        }

        private SyntaxList<MemberDeclarationSyntax> BuildConstructor(SyntaxList<MemberDeclarationSyntax> members)
        {
            if (_constructor == null)
            {
                return members;
            }

            return members.AddRange(SyntaxFactory.SingletonList<MemberDeclarationSyntax>(_constructor));
        }


        private TypeDeclarationSyntax BuildClassBase()
        {
            return SyntaxFactory.ClassDeclaration(Name).WithBaseList(CreateBaseList()).WithModifiers(CreateModifiers());
        }
    }
}
