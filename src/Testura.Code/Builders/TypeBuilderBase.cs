using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Class;
using Testura.Code.Generators.Common;
using Testura.Code.Models.Properties;
using Attribute = Testura.Code.Models.Attribute;

namespace Testura.Code.Builders
{
    public abstract class TypeBuilderBase<TBuilder>
        where TBuilder : TypeBuilderBase<TBuilder>
    {
        private readonly string _namespace;
        private readonly List<MethodDeclarationSyntax> _methods;
        private readonly List<Type> _inheritance;
        private SyntaxList<AttributeListSyntax> _attributes;
        private readonly List<PropertyDeclarationSyntax> _properties;

        private readonly List<string> _usings;
        private readonly List<Modifiers> _modifiers;

        protected TypeBuilderBase(string name, string @namespace)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            if (string.IsNullOrEmpty(@namespace))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(@namespace));
            }

            Name = name.Replace(" ", "_");
            _namespace = @namespace;
            _methods = new List<MethodDeclarationSyntax>();
            _inheritance = new List<Type>();
            _modifiers = new List<Modifiers> { Modifiers.Public };
            _usings = new List<string>();
            _properties = new List<PropertyDeclarationSyntax>();
        }

        protected string Name { get; }

        /// <summary>
        /// Set the using directives.
        /// </summary>
        /// <param name="usings">A set of wanted using directive names.</param>
        /// <returns>The current builder</returns>
        public TBuilder WithUsings(params string[] usings)
        {
            _usings.Clear();
            _usings.AddRange(usings);
            return (TBuilder)this;
        }

        /// <summary>
        /// Set class modifiers
        /// </summary>
        /// <param name="modifier">A set of wanted modifiers.</param>
        /// <returns>The current builder</returns>
        public TBuilder WithModifiers(params Modifiers[] modifier)
        {
            _modifiers.Clear();
            _modifiers.AddRange(modifier);
            return (TBuilder)this;
        }

        /// <summary>
        /// Set type(s) that the generated class should inherit from.
        /// </summary>
        /// <param name="types">A set of types to inherit from.</param>
        /// <returns>The current builder</returns>
        public TBuilder ThatInheritFrom(params Type[] types)
        {
            _inheritance.Clear();
            _inheritance.AddRange(types);
            return (TBuilder)this;
        }

        /// <summary>
        /// Set class attributes.
        /// </summary>
        /// <param name="attributes">A syntax list of already generated attributes.</param>
        /// <returns>The current builder</returns>
        public TBuilder WithAttributes(SyntaxList<AttributeListSyntax> attributes)
        {
            _attributes = attributes;
            return (TBuilder)this;
        }

        /// <summary>
        /// Set class attributes.
        /// </summary>
        /// <param name="attributes">A set of wanted attributes.</param>
        /// <returns>The current builder</returns>
        public TBuilder WithAttributes(params Attribute[] attributes)
        {
            _attributes = AttributeGenerator.Create(attributes);
            return (TBuilder)this;
        }

        /// <summary>
        /// Set class methods.
        /// </summary>
        /// <param name="methods">A set of already generated methods</param>
        /// <returns>The current class builder</returns>
        public TBuilder WithMethods(params MethodDeclarationSyntax[] methods)
        {
            _methods.Clear();
            _methods.AddRange(methods);
            return (TBuilder)this;
        }

        /// <summary>
        /// Set class properties.
        /// </summary>
        /// <param name="properties">A set of wanted properties.</param>
        /// <returns>The current builder</returns>
        public TBuilder WithProperties(params Property[] properties)
        {
            _properties.Clear();
            foreach (var property in properties)
            {
                _properties.Add(PropertyGenerator.Create(property));
            }

            return (TBuilder)this;
        }

        /// <summary>
        /// A class properties.
        /// </summary>
        /// <param name="properties">A sete of already generated properties</param>
        /// <returns>The current builder</returns>
        public TBuilder WithProperties(params PropertyDeclarationSyntax[] properties)
        {
            _properties.Clear();
            _properties.AddRange(properties);
            return (TBuilder)this;
        }

        protected CompilationUnitSyntax BuildUsings(CompilationUnitSyntax @base)
        {
            var usingSyntaxes = default(SyntaxList<UsingDirectiveSyntax>);
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

        protected SyntaxList<MemberDeclarationSyntax> BuildProperties(SyntaxList<MemberDeclarationSyntax> members)
        {
            return AddMembers(members, _properties);
        }

        protected SyntaxList<MemberDeclarationSyntax> BuildMethods(SyntaxList<MemberDeclarationSyntax> members)
        {
            return AddMembers(members, _methods);
        }

        protected TypeDeclarationSyntax BuildAttributes(TypeDeclarationSyntax @class)
        {
            return @class.WithAttributeLists(_attributes);
        }

        protected CompilationUnitSyntax BuildNamespace(CompilationUnitSyntax @base, TypeDeclarationSyntax @interface)
        {
            return @base.AddMembers(SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName(_namespace)).AddMembers(@interface));
        }

        protected SyntaxTokenList CreateModifiers()
        {
            return ModifierGenerator.Create(_modifiers.ToArray());
        }

        protected BaseListSyntax CreateBaseList()
        {
            if (!_inheritance.Any())
            {
                return null;
            }

            var syntaxNodeOrToken = new SyntaxNodeOrToken[(_inheritance.Count * 2) - 1];
            for (int n = 0; n < (_inheritance.Count * 2) - 1; n += 2)
            {
                syntaxNodeOrToken[n] = SyntaxFactory.SimpleBaseType(TypeGenerator.Create(_inheritance[n]));
                if (n + 1 < _inheritance.Count - 1)
                {
                    syntaxNodeOrToken[n + 1] = SyntaxFactory.Token(SyntaxKind.CommaToken);
                }
            }

            return SyntaxFactory.BaseList(SyntaxFactory.SeparatedList<BaseTypeSyntax>(syntaxNodeOrToken));
        }

        protected SyntaxList<MemberDeclarationSyntax> AddMembers(SyntaxList<MemberDeclarationSyntax> members, IEnumerable<MemberDeclarationSyntax> memberDeclarationSyntaxs)
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
