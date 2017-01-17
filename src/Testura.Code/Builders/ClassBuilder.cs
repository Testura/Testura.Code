using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Class;
using Testura.Code.Generators.Common;
using Testura.Code.Models;
using Attribute = Testura.Code.Models.Attribute;

namespace Testura.Code.Builders
{
    public class ClassBuilder
    {
        private readonly string _name;
        private readonly string _namespace;
        private readonly List<string> _usings;
        private readonly List<Modifiers> _modifiers;
        private readonly List<MethodDeclarationSyntax> _methods;
        private readonly List<Type> _inheritance;
        private readonly List<FieldDeclarationSyntax> _fields;
        private readonly List<PropertyDeclarationSyntax> _properties;
        private ConstructorDeclarationSyntax _constructor;

        private SyntaxList<AttributeListSyntax> _attributes;

        public ClassBuilder(string name, string @namespace)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            if (string.IsNullOrEmpty(@namespace))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(@namespace));
            }

            _name = name.Replace(" ", "_");
            _namespace = @namespace;
            _methods = new List<MethodDeclarationSyntax>();
            _inheritance = new List<Type>();
            _modifiers = new List<Modifiers> { Modifiers.Public };
            _fields = new List<FieldDeclarationSyntax>();
            _properties = new List<PropertyDeclarationSyntax>();
            _usings = new List<string>();
        }

        public ClassBuilder WithUsings(params string[] usings)
        {
            _usings.Clear();
            _usings.AddRange(usings);
            return this;
        }

        public ClassBuilder WithModifiers(params Modifiers[] modifier)
        {
            _modifiers.Clear();
            _modifiers.AddRange(modifier);
            return this;
        }

        public ClassBuilder ThatInheritFrom(params Type[] types)
        {
            _inheritance.Clear();
            _inheritance.AddRange(types);
            return this;
        }

        public ClassBuilder WithAttributes(params Attribute[] attributes)
        {
            _attributes = AttributeGenerator.Create(attributes);
            return this;
        }


        public ClassBuilder WithAttributes(SyntaxList<AttributeListSyntax> attributes)
        {
            _attributes = attributes;
            return this;
        }

        public ClassBuilder WithFields(params Field[] fields)
        {
            _fields.Clear();
            foreach (var field in fields)
            {
                _fields.Add(FieldGenerator.Create(field));
            }

            return this;
        }

        public ClassBuilder WithFields(params FieldDeclarationSyntax[] fields)
        {
            _fields.Clear();
            _fields.AddRange(fields);
            return this;
        }

        public ClassBuilder WithConstructor(ConstructorDeclarationSyntax constructor)
        {
            _constructor = constructor;
            return this;
        }

        public ClassBuilder WithMethods(params MethodDeclarationSyntax[] methods)
        {
            _methods.Clear();
            _methods.AddRange(methods);
            return this;
        }

        public ClassBuilder WithProperties(params Property[] properties)
        {
            _properties.Clear();
            foreach (var property in properties)
            {
                _properties.Add(PropertyGenerator.Create(property));
            }

            return this;
        }

        public ClassBuilder WithProperties(params PropertyDeclarationSyntax[] properties)
        {
            _properties.Clear();
            _properties.AddRange(properties);
            return this;
        }

        public CompilationUnitSyntax Build()
        {
            var members = new SyntaxList<MemberDeclarationSyntax>();
            members = BuildFields(members);
            members = BuildConstructor(members);
            members = BuildProperties(members);
            members = BuildMethods(members);
            var @class = BuildClassBase();
            @class = BuildAttributes(@class);
            @class = @class.WithMembers(members);
            var @base = SyntaxFactory.CompilationUnit();
            @base = BuildUsing(@base);
            @base = @base.AddMembers(SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName(_namespace)).AddMembers(@class));
            return @base;
        }

        private CompilationUnitSyntax BuildUsing(CompilationUnitSyntax @base)
        {
            var usingSyntaxes = new SyntaxList<UsingDirectiveSyntax>();
            foreach (var @using in _usings)
            {
                if (@using == null)
                {
                    continue;
                }

                usingSyntaxes = usingSyntaxes.Add(SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName(@using)));
            }

            foreach (var @using in usingSyntaxes)
            {
                @base = @base.AddUsings(@using);
            }

            return @base;
        }

        private ClassDeclarationSyntax BuildClassBase()
        {
            if (_inheritance.Any())
            {
                var syntaxNodeOrToken = new SyntaxNodeOrToken[(_inheritance.Count * 2) - 1];
                for (int n = 0; n < (_inheritance.Count * 2) - 1; n += 2)
                {
                    syntaxNodeOrToken[n] = SyntaxFactory.SimpleBaseType(TypeGenerator.Create(_inheritance[n]));
                    if (n + 1 < _inheritance.Count - 1)
                    {
                        syntaxNodeOrToken[n + 1] = SyntaxFactory.Token(SyntaxKind.CommaToken);
                    }
                }

                return SyntaxFactory.ClassDeclaration(_name).WithBaseList(SyntaxFactory.BaseList(SyntaxFactory.SeparatedList<BaseTypeSyntax>(syntaxNodeOrToken))).WithModifiers(ModifierGenerator.Create(_modifiers.ToArray()));
            }

            return SyntaxFactory.ClassDeclaration(_name).WithModifiers(ModifierGenerator.Create(_modifiers.ToArray()));
        }

        private ClassDeclarationSyntax BuildAttributes(ClassDeclarationSyntax @class)
        {
            return @class.WithAttributeLists(_attributes);
        }

        private SyntaxList<MemberDeclarationSyntax> BuildFields(SyntaxList<MemberDeclarationSyntax> members)
        {
            return AddMembers(members, _fields);
        }

        private SyntaxList<MemberDeclarationSyntax> BuildProperties(SyntaxList<MemberDeclarationSyntax> members)
        {
            return AddMembers(members, _properties);
        }

        private SyntaxList<MemberDeclarationSyntax> BuildConstructor(SyntaxList<MemberDeclarationSyntax> members)
        {
            if (_constructor == null)
            {
                return members;
            }

            return members.AddRange(SyntaxFactory.SingletonList<MemberDeclarationSyntax>(_constructor));
        }

        private SyntaxList<MemberDeclarationSyntax> BuildMethods(SyntaxList<MemberDeclarationSyntax> members)
        {
            return AddMembers(members, _methods);
        }

        private SyntaxList<MemberDeclarationSyntax> AddMembers(SyntaxList<MemberDeclarationSyntax> members, IEnumerable<MemberDeclarationSyntax> memberDeclarationSyntaxs)
        {
            if (!memberDeclarationSyntaxs.Any())
            {
                return members;
            }

            foreach (var memberDeclarationSyntax in memberDeclarationSyntaxs)
            {
                members = members.Add(memberDeclarationSyntax);
            }

            return members;
        }
    }
}
