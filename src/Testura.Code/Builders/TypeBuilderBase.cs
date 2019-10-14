using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Builders.BuildMembers;
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
        private readonly List<Type> _inheritance;
        private readonly List<string> _usings;
        private readonly List<Modifiers> _modifiers;
        private SyntaxList<AttributeListSyntax> _attributes;

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
            _inheritance = new List<Type>();
            _modifiers = new List<Modifiers> { Modifiers.Public };
            _usings = new List<string>();
            Members = new List<IBuildMember>();
        }

        protected string Name { get; }

        protected List<IBuildMember> Members { get; set; }

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
            Members.Add(new MethodMember(methods));
            return (TBuilder)this;
        }

        /// <summary>
        /// Set class properties.
        /// </summary>
        /// <param name="properties">A set of wanted properties.</param>
        /// <returns>The current builder</returns>
        public TBuilder WithProperties(params Property[] properties)
        {
            Members.Add(new PropertyMember(properties.Select(PropertyGenerator.Create)));
            return (TBuilder)this;
        }

        /// <summary>
        /// Add class properties.
        /// </summary>
        /// <param name="properties">A sete of already generated properties</param>
        /// <returns>The current builder</returns>
        public TBuilder WithProperties(params PropertyDeclarationSyntax[] properties)
        {
            Members.Add(new PropertyMember(properties));
            return (TBuilder)this;
        }

        /// <summary>
        /// Add region.
        /// </summary>
        /// <param name="regionMember">The region</param>
        /// <returns>The current builder</returns>
        public TBuilder WithRegions(params RegionMember[] regionMember)
        {
            Members.AddRange(regionMember);
            return (TBuilder)this;
        }

        /// <summary>
        /// Add build members that will be generated.
        /// </summary>
        /// <param name="buidMembers">Build members to add</param>
        /// <returns>The current builder</returns>
        public TBuilder With(params IBuildMember[] buidMembers)
        {
            Members.AddRange(buidMembers);
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
    }
}
