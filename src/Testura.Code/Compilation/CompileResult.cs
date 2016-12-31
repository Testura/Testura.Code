using System;
using System.Collections.Generic;

namespace Testura.Code.Compilation
{
    /// <summary>
    /// This class contains the result after we compiled our code
    /// </summary>
    [Serializable]
    public class CompileResult
    {
        /// <summary>
        /// Gets or sets path to the generated dlls.
        /// </summary>
        public string PathToDll { get; set; }

        /// <summary>
        /// Gets or sets if the test are successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the output rows
        /// </summary>
        public IList<OutputRow> OutputRows { get; set; }

        public CompileResult(string pathToDll, bool success, IList<OutputRow> outputRows)
        {
            PathToDll = pathToDll;
            Success = success;
            OutputRows = outputRows;
        } 

       
    }
}
