using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Builders.BuildMembers;
using Testura.Code.Generators.Class;
using Testura.Code.Models;
using Testura.Code.Models.Properties;

namespace Testura.Code.Builders;

public class RegionBuilder
{
    private readonly string _name;
    private List<IBuildMember> _buildMembers;

    public RegionBuilder(string name)
    {
        _name = name;
        _buildMembers = new List<IBuildMember>();
    }

    /// <summary>
    /// Add region methods.
    /// </summary>
    /// <param name="methods">A set of already generated methods</param>
    /// <returns>The current region builder</returns>
    public RegionBuilder WithMethods(params MethodDeclarationSyntax[] methods)
    {
        _buildMembers.Add(new MethodBuildMember(methods));
        return this;
    }

    /// <summary>
    /// Add region properties.
    /// </summary>
    /// <param name="properties">A set of wanted properties.</param>
    /// <returns>The current builder</returns>
    public RegionBuilder WithProperties(params Property[] properties)
    {
        _buildMembers.Add(new PropertyBuildMember(properties.Select(PropertyGenerator.Create)));
        return this;
    }

    /// <summary>
    /// Add region properties.
    /// </summary>
    /// <param name="properties">A sete of already generated properties</param>
    /// <returns>The current builder</returns>
    public RegionBuilder WithProperties(params PropertyDeclarationSyntax[] properties)
    {
        _buildMembers.Add(new PropertyBuildMember(properties));
        return this;
    }

    /// <summary>
    /// Add region constructor.
    /// </summary>
    /// <param name="constructor">An already generated constructor.</param>
    /// <returns>The current class builder</returns>
    public RegionBuilder WithConstructor(params ConstructorDeclarationSyntax[] constructor)
    {
        _buildMembers.Add(new ConstructorBuildMember(constructor));
        return this;
    }

    /// <summary>
    /// Add region fields.
    /// </summary>
    /// <param name="fields">A set of wanted fields.</param>
    /// <returns>The current class builder</returns>
    public RegionBuilder WithFields(params Field[] fields)
    {
        _buildMembers.Add(new FieldBuildMember(fields.Select(FieldGenerator.Create)));
        return this;
    }

    /// <summary>
    /// Add region fields.
    /// </summary>
    /// <param name="fields">An array of already declared fields.</param>
    /// <returns>The current class builder</returns>
    public RegionBuilder WithFields(params FieldDeclarationSyntax[] fields)
    {
        _buildMembers.Add(new FieldBuildMember(fields));
        return this;
    }

    public RegionBuildMember Build()
    {
        return new RegionBuildMember(_name, new List<IBuildMember>(_buildMembers));
    }
}