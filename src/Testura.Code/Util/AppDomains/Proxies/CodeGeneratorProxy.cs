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

        protected abstract void GenerateCode(Assembly assembly, IDictionary<string, object> extraData);
    }
}
