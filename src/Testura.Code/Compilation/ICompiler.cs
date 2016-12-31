using System.Threading.Tasks;

namespace Testura.Code.Compilation
{
    public interface ICompiler
    {
        Task<CompileResult> CompileAsync(string outputPath, string pathToCsFile);
        Task<CompileResult> CompileAsync(string outputPath, string[] pathsToCsFiles);
    }
}