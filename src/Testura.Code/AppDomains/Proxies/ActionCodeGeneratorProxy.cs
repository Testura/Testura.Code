using System;
using System.Reflection;

namespace Testura.Code.AppDomains.Proxies
{
    public class ActionCodeGeneratorProxy : MarshalByRefObject
    {
        /// <summary>
        /// Load an external assembly and generate code
        /// </summary>
        /// <param name="assemblyPath">Path to the assembly</param>
        /// <param name="generateCode">Action to invoke to generate code</param>
        public void GenerateCode(string assemblyPath, Action<Assembly> generateCode)
        {
            var assembly = Assembly.LoadFrom(assemblyPath);
            generateCode.Invoke(assembly);
        }
    }
}
