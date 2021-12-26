﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Builders.BuildMembers;
using Testura.Code.Generators.Class;
using Testura.Code.Generators.Common;
using Testura.Code.Generators.Special;
using Testura.Code.Models.Properties;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Attribute = Testura.Code.Models.Attribute;

namespace Testura.Code.Builders.Base
{
    public abstract class TypeBuilderBase<TBuilder> : BuilderBase<TBuilder>
        where TBuilder : TypeBuilderBase<TBuilder>
    {
        private readonly List<Type> _inheritance;
        private readonly List<Modifiers> _modifiers;
        private SyntaxList<AttributeListSyntax> _attributes;
        private string _summary;

        protected TypeBuilderBase(string name, string @namespace)
         : base(@namespace)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            Name = name.Replace(" ", "_");
            _inheritance = new List<Type>();
            _modifiers = new List<Modifiers> { Modifiers.Public };
        }

        protected string Name { get; }

        /// <summary>
        /// Set type modifiers
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
        /// Set type attributes.
        /// </summary>
        /// <param name="attributes">A syntax list of already generated attributes.</param>
        /// <returns>The current builder</returns>
        public TBuilder WithAttributes(SyntaxList<AttributeListSyntax> attributes)
        {
            _attributes = attributes;
            return (TBuilder)this;
        }

        /// <summary>
        /// Set type attributes.
        /// </summary>
        /// <param name="attributes">A set of wanted attributes.</param>
        /// <returns>The current builder</returns>
        public TBuilder WithAttributes(params Attribute[] attributes)
        {
            _attributes = AttributeGenerator.Create(attributes);
            return (TBuilder)this;
        }

        /// <summary>
        /// Set type methods.
        /// </summary>
        /// <param name="methods">A set of already generated methods</param>
        /// <returns>The current class builder</returns>
        public TBuilder WithMethods(params BaseMethodDeclarationSyntax[] methods)
        {
            return With(new MethodBuildMember(methods));
        }

        /// <summary>
        /// Set type properties.
        /// </summary>
        /// <param name="properties">A set of wanted properties.</param>
        /// <returns>The current builder</returns>
        public TBuilder WithProperties(params Property[] properties)
        {
            return With(new PropertyBuildMember(properties.Select(PropertyGenerator.Create)));
        }

        /// <summary>
        /// Add type properties.
        /// </summary>
        /// <param name="properties">A sete of already generated properties</param>
        /// <returns>The current builder</returns>
        public TBuilder WithProperties(params PropertyDeclarationSyntax[] properties)
        {
            return With(new PropertyBuildMember(properties));
        }

        /// <summary>
        /// Add region.
        /// </summary>
        /// <param name="regionMember">The region</param>
        /// <returns>The current builder.</returns>
        public TBuilder WithRegions(params RegionBuildMember[] regionMember)
        {
            return With(regionMember);
        }

        /// <summary>
        /// Add summary.
        /// </summary>
        /// <param name="summary">The summary text.</param>
        /// <returns>The current builder.</returns>
        public TBuilder WithSummary(string summary)
        {
            _summary = summary;
            return (TBuilder)this;
        }

        /// <summary>
        /// Build the type and return the generated code.
        /// </summary>
        /// <returns>The generated class.</returns>
        public CompilationUnitSyntax Build()
        {
            var @base = SyntaxFactory.CompilationUnit();
            @base = BuildUsings(@base);
            @base = BuildNamespace(@base, BuildWithoutNamespace());
            return @base;
        }

        /// <summary>
        /// Build the type without a namespace and return the generated code.
        /// </summary>
        /// <returns>The generated class.</returns>
        public TypeDeclarationSyntax BuildWithoutNamespace()
        {
            var @type = BuildBase();
            @type = @type.WithSummary(_summary);
            @type = BuildAttributes(@type);
            @type = BuildMembers(@type);
            return @type;
        }

        protected abstract TypeDeclarationSyntax BuildBase();

        protected TypeDeclarationSyntax BuildAttributes(TypeDeclarationSyntax @class)
        {
            return @class.WithAttributeLists(_attributes);
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

            return BaseList(
                SeparatedList<BaseTypeSyntax>(
                    _inheritance.Select(i => SimpleBaseType(TypeGenerator.Create(i))),
                    _inheritance.Select(i => Token(SyntaxKind.CommaToken))));
        }
    }
}
