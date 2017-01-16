using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Formatter = Microsoft.CodeAnalysis.Formatting.Formatter;

namespace Testura.Code.Saver
{
    /// <summary>
    /// This class contains help functions to generate code
    /// </summary>
    public class CodeSaver : ICodeSaver
    {
        /// <summary>
        /// Save the code
        /// </summary>
        /// <param name="cu">The complete code</param>
        /// <param name="path">Path to save too</param>
        public void SaveCodeToFile(CompilationUnitSyntax cu, string path)
        {
            var cw = new AdhocWorkspace();
            cw.Options.WithChangedOption(CSharpFormattingOptions.IndentBraces, true);
            var formattedCode = Formatter.Format(cu, cw);
            using (var sourceWriter = new StreamWriter(path))
            {
                formattedCode.WriteTo(sourceWriter);
            }
        }

        /// <summary>
        /// Save the code
        /// </summary>
        /// <param name="cu">The complete code</param>
        public string SaveCodeAsString(CompilationUnitSyntax cu)
        {
            var cw = new AdhocWorkspace();
            cw.Options.WithChangedOption(CSharpFormattingOptions.IndentBraces, true);
            var formattedCode = Formatter.Format(cu, cw);
            return formattedCode.ToFullString();
        }
    }
}
