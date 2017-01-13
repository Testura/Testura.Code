using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes
{
    public interface IArgument
    {
        ArgumentSyntax GetArgumentSyntax();
    }
}
