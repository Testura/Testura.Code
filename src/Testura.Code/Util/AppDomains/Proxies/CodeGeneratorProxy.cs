using System;
using System.Collections.Generic;
using System.Reflection;

namespace Testura.Code.Util.AppDomains.Proxies
{
    public abstract class CodeGeneratorProxy : MarshalByRefObject
    {
        /// <summary>
        /// Load an external assembly and generate code
        /// </summary>
        /// <param name="assemblyPath">Path to external assembly</param>
        /// <param name="extraData">Extra data</param>
        public void GenerateCode(string assemblyPath, IDictionary<string, object> extraData)
        {
            var assembly = Assembly.LoadFrom(assemblyPath);
            GenerateCode(assembly, extraData);
        }

        /// <summary>
        /// Generate code
        /// </summary>
        /// <param name="assembly">Assembly that we loaded inside the app domain</param>
        /// <param name="extraData">Extra data sent to the proxy</param>
        protected abstract void GenerateCode(Assembly assembly, IDictionary<string, object> extraData);
    }
}
