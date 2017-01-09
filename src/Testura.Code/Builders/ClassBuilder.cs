using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Builders
{
    /// <summary>
    /// Help class to build a class
    /// </summary>
    public class ClassBuilder
    {
        private readonly string name;
        private readonly NamespaceDeclarationSyntax @namespace;
        private SyntaxList<UsingDirectiveSyntax> usings;
        private SyntaxList<AttributeListSyntax> attributes;
        private readonly List<MethodDeclarationSyntax> methods;
        private readonly List<Type> inheritance;
        private readonly List<FieldDeclarationSyntax> fields;
        private readonly List<PropertyDeclarationSyntax> properties;
        private ConstructorDeclarationSyntax constructor;

        public ClassBuilder(string name, string @namespace )
        {
            this.name = name.Replace(" ", "_");
            this.@namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName(@namespace));
            attributes = new SyntaxList<AttributeListSyntax>();
            methods = new List<MethodDeclarationSyntax>();
            inheritance = new List<Type>();
            fields = new List<FieldDeclarationSyntax>();
            properties = new List<PropertyDeclarationSyntax>();
        }

        /// <summary>
        /// Set attributes for class
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public ClassBuilder WithAttributes(SyntaxList<AttributeListSyntax> attributes)
        {
            this.attributes = new SyntaxList<AttributeListSyntax>();
            this.attributes = this.attributes.AddRange(attributes);
            return this;
        }

        /// <summary>
        /// Set using statements for class
        /// </summary>
        /// <param name="usings"></param>
        /// <returns></returns>
        public ClassBuilder WithUsings(params string[] usings)
        {
            this.usings = new SyntaxList<UsingDirectiveSyntax>();
            foreach (var @using in usings)
            {
                if (@using == null)
                    continue;
                this.usings = this.usings.Add(SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName(@using)));
            }
            return this; 
        }

        /// <summary>
        /// Set methods for class
        /// </summary>
        /// <param name="methods"></param>
        /// <returns></returns>
        public ClassBuilder WithMethods(params MethodDeclarationSyntax[] methods)
        {
            this.methods.Clear();
            this.methods.AddRange(methods);
            return this; 
        }

        /// <summary>
        /// Set fields for class
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public ClassBuilder WithFields(IList<FieldDeclarationSyntax> fields)
        {
            this.fields.Clear();
            this.fields.AddRange(fields);
            return this; 
        }

        /// <summary>
        /// Set properties for class
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public ClassBuilder WithProperties(params PropertyDeclarationSyntax[] properties)
        {
            this.properties.Clear();
            this.properties.AddRange(properties);
            return this;
        }

        /// <summary>
        /// Set constructor for class
        /// </summary>
        /// <param name="constructor"></param>
        /// <returns></returns>
        public ClassBuilder WithConstructor(ConstructorDeclarationSyntax constructor)
        {
            this.constructor = constructor;
            return this;
        }

        /// <summary>
        /// Set types that class should inherit from
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public ClassBuilder ThatInheritFrom(IList<Type> types)
        {
            inheritance.Clear();
            inheritance.AddRange(inheritance);
            return this;
        }

        /// <summary>
        /// Build the final compilation syntax for a class
        /// </summary>
        /// <returns></returns>
        public CompilationUnitSyntax Build()
        {
            var members = new SyntaxList<MemberDeclarationSyntax>();
            members = BuildFields(members);
            members = BuildProperties(members);
            members = BuildConstructor(members);
            members = BuildMethods(members);
            var @class = BuildClassBase();
            @class = BuildAttributes(@class);
            @class = @class.WithMembers(members);
            var @base = SyntaxFactory.CompilationUnit();
            @base = BuildUsing(@base);
            @base = @base.AddMembers(@namespace.AddMembers(@class));
            return @base;
        }

        private CompilationUnitSyntax BuildUsing(CompilationUnitSyntax @base)
        {
            foreach (var @using in usings)
            {
                @base = @base.AddUsings(@using);
            }
            return @base;
        }

        private ClassDeclarationSyntax BuildClassBase()
        {
            if (inheritance.Any())
            {
                var syntaxNodeOrToken = new SyntaxNodeOrToken[inheritance.Count*2-1];
                for (int n = 0; n < inheritance.Count*2 - 1; n += 2)
                {
                    syntaxNodeOrToken[n] = SyntaxFactory.SimpleBaseType(SyntaxFactory.IdentifierName("MyClass"));
                    if(n+1 != inheritance.Count - 1)
                        syntaxNodeOrToken[n + 1] = SyntaxFactory.Token(SyntaxKind.CommaToken);
                }
                return SyntaxFactory.ClassDeclaration(name).WithBaseList(SyntaxFactory.BaseList(SyntaxFactory.SeparatedList<BaseTypeSyntax>(syntaxNodeOrToken))).AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

            }
            return SyntaxFactory.ClassDeclaration(name).AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
        }

        private ClassDeclarationSyntax BuildAttributes(ClassDeclarationSyntax @class)
        {
            return @class.WithAttributeLists(SyntaxFactory.List(attributes));
        }

        private SyntaxList<MemberDeclarationSyntax> BuildFields(SyntaxList<MemberDeclarationSyntax> members)
        {
            return AddMembers(members, fields);
        }

        private SyntaxList<MemberDeclarationSyntax> BuildProperties(SyntaxList<MemberDeclarationSyntax> members)
        {
            return AddMembers(members, properties);
        }

        private SyntaxList<MemberDeclarationSyntax> BuildConstructor(SyntaxList<MemberDeclarationSyntax> members)
        {
            if (constructor == null)
                return members;
            return members.AddRange(SyntaxFactory.SingletonList<MemberDeclarationSyntax>(constructor));
        }

        private SyntaxList<MemberDeclarationSyntax> BuildMethods(SyntaxList<MemberDeclarationSyntax> members)
        {
            return AddMembers(members, methods);
        }

        private SyntaxList<MemberDeclarationSyntax> AddMembers(SyntaxList<MemberDeclarationSyntax> members,
            IEnumerable<MemberDeclarationSyntax> memberDeclarationSyntaxs)
        {
            if (!memberDeclarationSyntaxs.Any())
                return members;
            foreach (var memberDeclarationSyntax in memberDeclarationSyntaxs)
            {
                members = members.Add(memberDeclarationSyntax);
            }
            return members;
        }
    }
}
