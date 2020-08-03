using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Testura.Code.Compilations
{
    public static class AdvancedCompiler
    {
        public static CompileResult CompileSourceStrings(Stream outputStream, CompilerSettings settings = default, params string[] sources)
        {
            if (settings == null)
            {
                settings = new CompilerSettings();
            }

            // add references
            var metaDataRef = new List<MetadataReference>();
            foreach (var s in settings.ReferenceAssemblyStreams)
            {
                metaDataRef.Add(MetadataReference.CreateFromStream(s));
            }

            foreach (var s in settings.ReferenceAssemblyFilePaths)
            {
                metaDataRef.Add(MetadataReference.CreateFromFile(s));
            }
            
            var parseOptions = new CSharpParseOptions(settings.LanguageVersion);
            var parsedSyntaxTrees = new SyntaxTree[sources.Length];
            for (int i = 0; i < sources.Length; i++)
            {
                var stringText = SourceText.From(sources[i], Encoding.UTF8);
                parsedSyntaxTrees[i] = SyntaxFactory.ParseSyntaxTree(stringText, parseOptions);
            }

            var defaultCompilationOptions = new CSharpCompilationOptions(settings.OutputKind)
                .WithPlatform(settings.Platform)
                .WithOverflowChecks(settings.EnableOverflowChecks)
                .WithOptimizationLevel(settings.OptimizationLevel)
                .WithUsings(settings.Usings);

            var compilation = CSharpCompilation.Create(
                settings.AssemblyName,
                parsedSyntaxTrees,
                metaDataRef,
                defaultCompilationOptions);

            var result = compilation.Emit(outputStream);
            var outputRows = ConvertDiagnosticsToOutputRows(result.Diagnostics);
            return new CompileResult(result.Success, outputRows);
        }

        private static IList<OutputRow> ConvertDiagnosticsToOutputRows(IEnumerable<Diagnostic> diagnostics)
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
    }
}
