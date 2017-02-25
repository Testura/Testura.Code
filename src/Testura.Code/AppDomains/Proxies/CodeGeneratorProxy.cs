using System;
using System.Reflection;

namespace Testura.Code.AppDomains.Proxies
{
    public abstract class CodeGeneratorProxy : MarshalByRefObject
    {
        /// <summary>
        /// Load an external assembly and generate code
        /// </summary>
        /// <param name="assemblyPath">Path to external assembly</param>
        public void GenerateCode(string assemblyPath)
        {
            var assembly = Assembly.LoadFrom(assemblyPath);
            GenerateCode(assembly);
        }

        protected abstract void GenerateCode(Assembly assembly);
    }
}
