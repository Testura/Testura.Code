namespace Testura.Code.Compilations
{
    /// <summary>
    /// This class contains information about a compiler/output row
    /// </summary>
    public class OutputRow
    {
        /// <summary>
        /// Gets or sets the severity of the error/warning
        /// </summary>
        public string Severity { get; set; }

        /// <summary>
        /// Gets or sets the description of the error/warning
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name of the test that gave the warning
        /// </summary>
        public string ClassName { get; set; }
    }
}
