#pragma warning disable 1591

namespace Testura.Code.Compilations;

public interface ICompiler
{
    /// <summary>
    /// Compile code from a file without creating a dll.
    /// </summary>
    /// <param name="pathsToCsFiles">Paths to cs files.</param>
    /// <returns>The result from the compilation.</returns>
    Task<CompileResult> CompileFilesInMemoryAsync(params string[] pathsToCsFiles);

    /// <summary>
    /// Compile code from a string source without creating a dll.
    /// </summary>
    /// <param name="source">Source strings to compile.</param>
    /// <returns>The result from the compilation.</returns>
    Task<CompileResult> CompileSourceInMemoryAsync(params string[] source);

    /// <summary>
    /// Compile code from a file into a dll.
    /// </summary>
    /// <param name="outputPath">Where we should save the dlls.</param>
    /// <param name="pathsToCsFiles">Path to the cs files.</param>
    /// <returns>The result from the compilation.</returns>
    Task<CompileResult> CompileFilesAsync(string outputPath, params string[] pathsToCsFiles);

    /// <summary>
    /// Compile code from a string source into a dll.
    /// </summary>
    /// <param name="outputPath">Where we should save the dlls.</param>
    /// <param name="source">Source string to compile.</param>
    /// <returns>The result of the compilation.</returns>
    Task<CompileResult> CompileSourceAsync(string outputPath, params string[] source);
}