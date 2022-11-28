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
    /// <param name="cu">Generated code.</param>
    /// <param name="path">Full output path.</param>
    public void SaveCodeToFile(CompilationUnitSyntax cu, string path)
    {
        if (cu == null)
        {
            throw new ArgumentNullException(nameof(cu));
        }

        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(path));
        }

        EnsurePathExists(path);
        var workspace = CreateWorkspace();
        var formattedCode = Formatter.Format(cu, workspace);
        using var sourceWriter = new StreamWriter(path);
        formattedCode.WriteTo(sourceWriter);
    }

    /// <summary>
    /// Save generated code to a file asynchronously
    /// </summary>
    /// <param name="cu">Generated code.</param>
    /// <param name="path">Full output path.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Writes the generated code to the <paramref name="path"/> supplied by the user.</returns>
    public async Task SaveCodeToFileAsync(CompilationUnitSyntax cu, string path, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (cu == null)
        {
            throw new ArgumentNullException(nameof(cu));
        }

        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(path));
        }

        EnsurePathExists(path);
        await using var fileStream = File.Open(path, FileMode.Create, FileAccess.Write);
        await using var sourceWriter = new StreamWriter(fileStream);
        await sourceWriter.WriteAsync(Formatter.Format(cu, CreateWorkspace()).ToFullString());
    }

    /// <summary>
    /// Save generated code as a string.
    /// </summary>
    /// <param name="cu">Generated code.</param>
    /// <returns>Generated code as a string.</returns>
    public string SaveCodeAsString(CompilationUnitSyntax cu)
    {
        if (cu == null)
        {
            throw new ArgumentNullException(nameof(cu));
        }

        var workspace = CreateWorkspace();
        var formattedCode = Formatter.Format(cu, workspace);
        return formattedCode.ToFullString();
    }

    private AdhocWorkspace CreateWorkspace()
    {
        var cw = new AdhocWorkspace();
        cw.Options.WithChangedOption(CSharpFormattingOptions.IndentBraces, true);
        foreach (var optionKeyValue in _options)
        {
            cw.TryApplyChanges(cw.CurrentSolution.WithOptions(cw.Options.WithChangedOption(optionKeyValue.FormattingOption, optionKeyValue.Value)));
        }

        return cw;
    }

    private void EnsurePathExists(string filePath)
    {
        var fi = new FileInfo(filePath);
        if (!Directory.Exists(fi.Directory.FullName))
        {
            Directory.CreateDirectory(fi.Directory.FullName);
        }
    }
}
