using System;
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
        private readonly string _runtimeDirectory;
        private readonly string[] _referencedAssemblies;
        private readonly IEnumerable<string> _defaultNamespaces;

        public Compiler(string[] referencedAssemblies = null, string runtimeDirectory = null)
        {
            _referencedAssemblies = referencedAssemblies ?? new string[0];
            _runtimeDirectory = runtimeDirectory ?? @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5.1\";
            _defaultNamespaces = new[]
            {
                "System",
                "System.IO",
                "System.Net",
                "System.Linq",
                "System.Text",
                "System.Text.RegularExpressions",
                "System.Collections.Generic"
            };
        }

        public async Task<CompileResult> CompileFilesAsync(string outputPath, params string[] pathsToCsFiles)
        {
            if (outputPath == null)
            {
                throw new ArgumentNullException(nameof(outputPath));
            }

            if (pathsToCsFiles.Length == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(pathsToCsFiles));
            }

            var source = new string[pathsToCsFiles.Length];
            for (int n = 0; n < pathsToCsFiles.Length; n++)
            {
                source[n] = File.ReadAllText(pathsToCsFiles[n]);
            }

            return await CompileSourceAsync(outputPath, source);
        }

        public async Task<CompileResult> CompileSourceAsync(string outputPath, params string[] source)
        {
            if (outputPath == null)
            {
                throw new ArgumentNullException(nameof(outputPath));
            }

            if (source.Length == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(source));
            }

            return await Task.Run(() =>
            {
                var parsedSyntaxTrees = new SyntaxTree[source.Length];
                for (int i = 0; i < source.Length; i++)
                {
                    parsedSyntaxTrees[i] = Parse(
                        source[i],
                        CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.CSharp6));
                }

                var defaultCompilationOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                    .WithOverflowChecks(true)
                    .WithOptimizationLevel(OptimizationLevel.Release)
                    .WithUsings(_defaultNamespaces);

                var compilation = CSharpCompilation.Create(
                    Path.GetFileName(outputPath),
                    parsedSyntaxTrees,
                    ConvertReferenceToMetaDataReferfence(),
                    defaultCompilationOptions);

                var result = compilation.Emit(outputPath);
                var outputRows = ConvertDiagnosticsToOutputRows(result.Diagnostics);
                return new CompileResult(outputPath, result.Success, outputRows);
            });
        }

        private IList<OutputRow> ConvertDiagnosticsToOutputRows(IEnumerable<Diagnostic> diagnostics)
        {
            var outputRows = new List<OutputRow>();
            foreach (var diagnostic in diagnostics)
            {
                if (diagnostic.Severity < DiagnosticSeverity.Error)
                {
                    continue;
                }

                outputRows.Add(new OutputRow { Description = diagnostic.GetMessage(), Severity = diagnostic.Severity.ToString(), ClassName = diagnostic.Id });
            }

            return outputRows;
        }

        private IEnumerable<MetadataReference> ConvertReferenceToMetaDataReferfence()
        {
            List<MetadataReference> metaData = new List<MetadataReference>();
            foreach (var reference in _referencedAssemblies)
            {
                metaData.Add(MetadataReference.CreateFromFile(reference));
            }

            // Add default references
            metaData.Add(MetadataReference.CreateFromFile(Path.Combine(_runtimeDirectory, "mscorlib.dll")));
            metaData.Add(MetadataReference.CreateFromFile(Path.Combine(_runtimeDirectory, "System.dll")));
            metaData.Add(MetadataReference.CreateFromFile(Path.Combine(_runtimeDirectory, "System.Core.dll")));
            return metaData;
        }

        private SyntaxTree Parse(string text, CSharpParseOptions options = null)
        {
            var stringText = SourceText.From(text, Encoding.UTF8);
            return SyntaxFactory.ParseSyntaxTree(stringText, options);
        }
    }
}
