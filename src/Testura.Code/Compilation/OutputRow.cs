namespace Testura.Code.Compilation
{
    /// <summary>
    /// This class contains information about a compiler/output row
    /// </summary>
    public class OutputRow
    {
        /// <summary>
        /// The severity of the error/warning
        /// </summary>
        public string Severity { get; set; }

        /// <summary>
        /// The description of the error/warning
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Name of the test that gave the warning
        /// </summary>
        public string ClassName { get; set; }
    }
}
