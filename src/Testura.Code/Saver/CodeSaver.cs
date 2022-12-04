using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Models.Options;
using Formatter = Microsoft.CodeAnalysis.Formatting.Formatter;

namespace Testura.Code.Saver;

/// <summary>
/// Provides the functionality to save code to file or string.
/// </summary>
public class CodeSaver : ICodeSaver
{
    private readonly IList<OptionKeyValue> _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="CodeSaver"/> class.
    /// </summary>
    public CodeSaver()
    {
        _options = new List<OptionKeyValue>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodeSaver"/> class.
    /// </summary>
    /// <param name="options">A list with formatting options</param>
    public CodeSaver(IEnumerable<OptionKeyValue> options)
    {
        _options = new List<OptionKeyValue>(options);
    }

    /// <summary>
    /// Save generated code to a file.
    /// </summary>
    /// <param name="compiledSourceCode">Generated code.</param>
    /// <param name="destinationFileAbsolutePath">Full output destinationFileAbsolutePath.</param>
    public void SaveCodeToFile(CompilationUnitSyntax compiledSourceCode, string destinationFileAbsolutePath)
    {
        if (compiledSourceCode == null)
        {
            throw new ArgumentNullException(nameof(compiledSourceCode));
        }

        if (string.IsNullOrEmpty(destinationFileAbsolutePath))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(destinationFileAbsolutePath));
        }

        EnsurePathExists(destinationFileAbsolutePath);
        using var createdWorkspaceForCodeGen = CreateWorkspace();
        var formattedCode = Formatter.Format(compiledSourceCode, createdWorkspaceForCodeGen);
        createdWorkspaceForCodeGen.Dispose();
        using var sourceWriter = new StreamWriter(destinationFileAbsolutePath);
        formattedCode.WriteTo(sourceWriter);
    }

    /// <summary>
    /// Save generated code to a file asynchronously
    /// </summary>
    /// <param name="compiledSourceCode">Generated code.</param>
    /// <param name="destinationFileAbsolutePath">Full output destinationFileAbsolutePath.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Writes the generated code to the <paramref name="destinationFileAbsolutePath"/> supplied by the user.</returns>
    public async Task SaveCodeToFileAsync(CompilationUnitSyntax compiledSourceCode, string destinationFileAbsolutePath, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (compiledSourceCode == null)
        {
            throw new ArgumentNullException(nameof(compiledSourceCode));
        }

        if (string.IsNullOrEmpty(destinationFileAbsolutePath))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(destinationFileAbsolutePath));
        }

        EnsurePathExists(destinationFileAbsolutePath);
        await using var fileStream = File.Open(destinationFileAbsolutePath, FileMode.Create, FileAccess.Write);
        await using var sourceWriter = new StreamWriter(fileStream);
        using var createdWorkspaceForCodeGen = CreateWorkspace();
        await sourceWriter.WriteAsync(Formatter.Format(compiledSourceCode, createdWorkspaceForCodeGen, cancellationToken: cancellationToken).ToFullString());
        createdWorkspaceForCodeGen.Dispose();
    }

    /// <summary>
    /// Save generated code as a string.
    /// </summary>
    /// <param name="compiledSourceCode">Generated code.</param>
    /// <returns>Generated code as a string.</returns>
    public string SaveCodeAsString(CompilationUnitSyntax compiledSourceCode)
    {
        if (compiledSourceCode == null)
        {
            throw new ArgumentNullException(nameof(compiledSourceCode));
        }

        using var createdWorkspaceForCodeGen = CreateWorkspace();
        var formattedCode = Formatter.Format(compiledSourceCode, createdWorkspaceForCodeGen);
        createdWorkspaceForCodeGen.Dispose();
        return formattedCode.ToFullString();
    }

    private AdhocWorkspace CreateWorkspace()
    {
        var createdWorkspaceForCodeGen = new AdhocWorkspace();
        createdWorkspaceForCodeGen.Options.WithChangedOption(CSharpFormattingOptions.IndentBraces, true);
        foreach (var optionKeyValue in _options)
        {
            createdWorkspaceForCodeGen.TryApplyChanges(createdWorkspaceForCodeGen.CurrentSolution.WithOptions(createdWorkspaceForCodeGen.Options.WithChangedOption(optionKeyValue.FormattingOption, optionKeyValue.Value)));
        }

        return createdWorkspaceForCodeGen;
    }

    private void EnsurePathExists(string destinationFileAbsolutePath)
    {
        var fileInfo = new FileInfo(destinationFileAbsolutePath);
        if (fileInfo.Directory == null)
        {
            throw new DirectoryNotFoundException(
                $"The parent directory of the target destination file cannot be null.  Target destination file full path: {
                    fileInfo.FullName}");
        }

        if (!Directory.Exists(fileInfo.Directory.FullName))
        {
            Directory.CreateDirectory(fileInfo.Directory.FullName);
        }
    }
}
