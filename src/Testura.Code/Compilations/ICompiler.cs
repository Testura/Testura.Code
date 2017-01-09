using System.Threading.Tasks;

namespace Testura.Code.Compilations
{
    public interface ICompiler
    {
        Task<CompileResult> CompileAsync(string outputPath, string pathToCsFile);
        Task<CompileResult> CompileAsync(string outputPath, string[] pathsToCsFiles);
    }
}