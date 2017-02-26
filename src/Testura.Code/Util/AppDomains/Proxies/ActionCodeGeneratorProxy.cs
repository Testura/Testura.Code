using System;
using System.Collections.Generic;
using System.Reflection;

namespace Testura.Code.Util.AppDomains.Proxies
{
    public class ActionCodeGeneratorProxy : MarshalByRefObject
    {
        /// <summary>
        /// Load an external assembly and generate code
        /// </summary>
        /// <param name="assemblyPath">Path to the assembly</param>
        /// <param name="generateCode">Action to invoke to generate code</param>
        /// <param name="extraData">Extra data</param>
        public void GenerateCode(string assemblyPath, Action<Assembly, IDictionary<string, object>> generateCode, IDictionary<string, object> extraData = null)
        {
            var assembly = Assembly.LoadFrom(assemblyPath);
            generateCode.Invoke(assembly, extraData);
        }
    }
}
