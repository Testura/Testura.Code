using System.Threading.Tasks;

namespace Testura.Code.Compilations
{
    public interface ICompiler
    {
        Task<CompileResult> CompileFilesAsync(string outputPath, params string[] pathToCsFile);

        Task<CompileResult> CompileSourceAsync(string outputPath, params string[] source);
    }
}