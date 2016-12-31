using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Saver
{
    public interface ICodeSaver
    {
        void SaveCodeToFile(CompilationUnitSyntax cu, string path);
        string SaveCodeAsString(CompilationUnitSyntax cu);
    }
}