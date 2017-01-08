using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Helper.Arguments.ArgumentTypes
{
    public interface IArgument
    {
        ArgumentSyntax GetArgumentSyntax();
    }
}
