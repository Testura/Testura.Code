using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Helpers.Arguments.ArgumentTypes
{
    public interface IArgument
    {
        ArgumentSyntax GetArgumentSyntax();
    }
}
