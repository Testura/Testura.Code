namespace Testura.Code.Compilations;

/// <summary>
/// Represent the result after a compilation
/// </summary>
[Serializable]
public class CompileResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CompileResult"/> class.
    /// </summary>
    /// <param name="pathToDll">Path to the dll.</param>
    /// <param name="success">If the compilation was successful or not.</param>
    /// <param name="outputRows">Output from the compilation.</param>
    public CompileResult(string pathToDll, bool success, IList<OutputRow> outputRows)
    {
        PathToDll = pathToDll;
        Success = success;
        OutputRows = outputRows;
    }

    /// <summary>
    /// Gets or sets path to the generated dlls.
    /// </summary>
    public string PathToDll { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the test are successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the output rows.
    /// </summary>
    public IList<OutputRow> OutputRows { get; set; }
}