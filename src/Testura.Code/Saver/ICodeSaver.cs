using Microsoft.CodeAnalysis.CSharp.Syntax;
#pragma warning disable 1591

namespace Testura.Code.Saver;

public interface ICodeSaver
{
    /// <summary>
    /// Save generated code to a file.
    /// </summary>
    /// <param name="compiledSourceCode">Generated code.</param>
    /// <param name="destinationFileAbsolutePath">Full output path.</param>
    void SaveCodeToFile(CompilationUnitSyntax compiledSourceCode, string destinationFileAbsolutePath);

    /// <summary>
    /// Save generated code as a string.
    /// </summary>
    /// <param name="compiledSourceCode">Generated code.</param>
    /// <returns>Generated code as a string.</returns>
    string SaveCodeAsString(CompilationUnitSyntax compiledSourceCode);

    /// <summary>
    /// Save generated code to a file.
    /// </summary>
    /// <param name="compiledSourceCode">Generated code.</param>
    /// <param name="destinationFileAbsolutePath">Full output path.</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>Saves the generated code to the filesystem.</returns>
    Task SaveCodeToFileAsync(
        CompilationUnitSyntax compiledSourceCode,
        string destinationFileAbsolutePath,
        CancellationToken cancellationToken = default);
}
