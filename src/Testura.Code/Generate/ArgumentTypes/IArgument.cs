using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Generate.ArgumentTypes
{
    public interface IArgument
    {
        ArgumentSyntax GetArgumentSyntax();
    }
}
