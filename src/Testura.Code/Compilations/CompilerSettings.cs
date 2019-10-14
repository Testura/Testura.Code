using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Testura.Code.Compilations
{
    public class CompilerSettings
    {
        public CompilerSettings()
        {
            Platform = Platform.AnyCpu;
            OutputKind = OutputKind.DynamicallyLinkedLibrary;
            EnableOverflowChecks = false;
            OptimizationLevel = OptimizationLevel.Release;
            AssemblyName = "Temporary";
            LanguageVersion = LanguageVersion.Latest;
            var loadadAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            var defaultAssemblyNames = new[] { "mscorlib", "System", "System.Core" };
            
            // make sure above default assemblies are available to load from file, if they aren't (e.g. we run on WASM) don't load them
            var defaultAssemblies = loadadAssemblies.Where(x =>
                defaultAssemblyNames.Contains(x.GetName().Name) &&
                !string.IsNullOrEmpty(x.Location) &&
                File.Exists(x.Location)).Select(x => x.Location);
            ReferenceAssemblyFilePaths = new List<string>(defaultAssemblies);

            ReferenceAssemblyStreams = new List<Stream>();
            Usings = new List<string>
            {
                "System",
                "System.IO",
                "System.Net",
                "System.Linq",
                "System.Text",
                "System.Text.RegularExpressions",
                "System.Collections.Generic",
            };
        }

        public Platform Platform { get; set; }

        public OutputKind OutputKind { get; set; }

        public bool EnableOverflowChecks { get; set; }

        public OptimizationLevel OptimizationLevel { get; set; }

        public string AssemblyName { get; set; }

        public LanguageVersion LanguageVersion { get; set; }

        public List<string> ReferenceAssemblyFilePaths { get; set; }

        public List<Stream> ReferenceAssemblyStreams { get; set; }

        public List<string> Usings { get; set; }
    }
}
