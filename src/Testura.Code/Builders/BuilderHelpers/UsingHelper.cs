using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Builders.BuilderHelpers;

internal class UsingHelper
{
    public UsingHelper()
    {
        Usings = new List<string>();
    }

    public List<string> Usings { get; }

    /// <summary>
    /// Set the using directives.
    /// </summary>
    /// <param name="usings">A set of wanted using directive names.</param>
    public void AddUsings(params string[] usings)
    {
        Usings.Clear();
        Usings.AddRange(usings);
    }

    public CompilationUnitSyntax BuildUsings(CompilationUnitSyntax @base)
    {
        var usingSyntaxes = default(SyntaxList<UsingDirectiveSyntax>);
        foreach (var @using in Usings)
        {
            if (@using == null)
            {
                continue;
            }

            usingSyntaxes = usingSyntaxes.Add(UsingDirective(IdentifierName(@using)));
        }

        foreach (var @using in usingSyntaxes)
        {
            @base = @base.AddUsings(@using);
        }

        return @base;
    }
}