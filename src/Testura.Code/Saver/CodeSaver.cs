using System;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Formatter = Microsoft.CodeAnalysis.Formatting.Formatter;

namespace Testura.Code.Saver
{
    public class CodeSaver : ICodeSaver
    {
        /// <summary>
        /// Save generated code to a file
        /// </summary>
        /// <param name="cu">Generated code</param>
        /// <param name="path">Full output path</param>
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

            var cw = new AdhocWorkspace();
            cw.Options.WithChangedOption(CSharpFormattingOptions.IndentBraces, true);
            var formattedCode = Formatter.Format(cu, cw);
            using (var sourceWriter = new StreamWriter(path))
            {
                formattedCode.WriteTo(sourceWriter);
            }
        }

        /// <summary>
        /// Save generated code as a string
        /// </summary>
        /// <param name="cu">Generated code</param>
        /// <returns>Generated code as a string</returns>
        public string SaveCodeAsString(CompilationUnitSyntax cu)
        {
            if (cu == null)
            {
                throw new ArgumentNullException(nameof(cu));
            }

            var cw = new AdhocWorkspace();
            cw.Options.WithChangedOption(CSharpFormattingOptions.IndentBraces, true);
            var formattedCode = Formatter.Format(cu, cw);
            return formattedCode.ToFullString();
        }
    }
}
