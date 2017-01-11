using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace Testura.Code.Compilations
{
    public class Compiler : ICompiler
    {
        private const string RuntimePath = @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5.1\{0}.dll";

        private readonly IEnumerable<string> defaultNamespaces = new[]
        {
                "System",
                "System.IO",
                "System.Net",
                "System.Linq",
                "System.Text",
                "System.Text.RegularExpressions",
                "System.Collections.Generic"
        };

        private readonly string[] referencedAssemblies;

        public Compiler(string[] referencedAssemblies)
        {
            this.referencedAssemblies = referencedAssemblies;
        }

        /// <summary>
        /// Compile code to a dll
        /// </summary>
        /// <param name="outputPath">Path where the dll file should be created</param>
        /// <param name="pathToCsFile">Path to the .cs file</param>
        /// <returns>The result from the compiler</returns>
        public Task<CompileResult> CompileAsync(string outputPath, string pathToCsFile)
        {
            return CompileAsync(outputPath, new[] { pathToCsFile });
        }

        /// <summary>
        /// Compile code to a dll
        /// </summary>
        /// <param name="outputPath">Path where the dll file should be created</param>
        /// <param name="pathsToCsFiles">An array with paths to the .cs files</param>
        /// <returns>Results from the compiler</returns>
        public Task<CompileResult> CompileAsync(string outputPath, string[] pathsToCsFiles)
        {
            return Task.Run(() =>
            {
                var parsedSyntaxTrees = new SyntaxTree[pathsToCsFiles.Length];
                for (int n = 0; n < pathsToCsFiles.Length; n++)
                {
                    var pathToCsFile = pathsToCsFiles[n];
                    var source = File.ReadAllText(pathToCsFile);
                    parsedSyntaxTrees[n] = Parse(source,
                        CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.CSharp6));
                }
                var defaultCompilationOptions = new CSharpCompilationOptions(
                    OutputKind.DynamicallyLinkedLibrary).WithOverflowChecks(true)
                    .WithOptimizationLevel(OptimizationLevel.Release)
                    .WithUsings(defaultNamespaces);
                var compilation
                    = CSharpCompilation.Create(Path.GetFileName(outputPath), parsedSyntaxTrees,
                        ConvertReferenceToMetaDataReferfence(), defaultCompilationOptions);
                var result = compilation.Emit(outputPath);
                var outputRows = ConvertDiagnosticsToOutputRows(result.Diagnostics);
                return new CompileResult(outputPath, result.Success, outputRows);
            });
        }

        private IList<OutputRow> ConvertDiagnosticsToOutputRows(IList<Diagnostic> diagnostics)
        {
            var outputRows = new List<OutputRow>();
            foreach (var diagnostic in diagnostics)
            {
                if (diagnostic.Severity < DiagnosticSeverity.Error)
                    continue;
                outputRows.Add(new OutputRow { Description = diagnostic.GetMessage(), Severity = diagnostic.Severity.ToString(), ClassName = diagnostic.Id });
            }
            return outputRows;
        }

        /// <summary>
        /// Convert all our string references to correct meta data references
        /// </summary>
        /// <returns>Created list of meta data references</returns>
        private IEnumerable<MetadataReference> ConvertReferenceToMetaDataReferfence()
        {
            List<MetadataReference> metaData = new List<MetadataReference>();
            foreach (var reference in referencedAssemblies)
            {
                metaData.Add(MetadataReference.CreateFromFile(reference));
            }

            //Add default references 
            metaData.Add(MetadataReference.CreateFromFile(string.Format(RuntimePath, "mscorlib")));
            metaData.Add(MetadataReference.CreateFromFile(string.Format(RuntimePath, "System")));
            metaData.Add(MetadataReference.CreateFromFile(string.Format(RuntimePath, "System.Core")));
            return metaData;
        }

        /// <summary>
        /// Parse the code from a string and produce a syntax tree.
        /// </summary>
        /// <param name="text">The text code</param>
        /// <param name="options">The parse options</param>
        /// <returns>The parsed syntax tree</returns>
        private SyntaxTree Parse(string text, CSharpParseOptions options = null)
        {
            var stringText = SourceText.From(text, Encoding.UTF8);
            return SyntaxFactory.ParseSyntaxTree(stringText, options);
        }
    }
}
