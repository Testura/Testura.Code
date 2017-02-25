using System;
using System.Reflection;
using Testura.Code.AppDomains.Proxies;

namespace Testura.Code.AppDomains
{
    public class AppDomainCodeGenerator
    {
        /// <summary>
        /// Load an external assembly and generate code inside a different app domain. Will unload
        /// app domain after finish generating code.
        /// </summary>
        /// <typeparam name="T">Type of custom generator proxy to use</typeparam>
        /// <param name="assembly">Path to the external assembly</param>
        /// <param name="customCodeGeneratorProxy">The custom code generator proxy to use</param>
        public void GenerateCode<T>(string assembly, T customCodeGeneratorProxy)
            where T : CodeGeneratorProxy
        {
            var domain = CreateDomain();
            var proxy = CreateProxy<T>(domain);
            proxy.GenerateCode(assembly);
            AppDomain.Unload(domain);
        }

        /// <summary>
        /// Load an external assembly and generate code inside a different app domain. Will unload
        /// app domain after finish generating code.
        /// </summary>
        /// <param name="assembly">Path to the external assembly</param>
        /// <param name="generateCode">Action to invoke inside the new app domain</param>
        public void GenerateCode(string assembly, Action<Assembly> generateCode)
        {
            var domain = CreateDomain();
            var proxy = CreateProxy<ActionCodeGeneratorProxy>(domain);
            proxy.GenerateCode(assembly, generateCode);
            AppDomain.Unload(domain);
        }

        private AppDomain CreateDomain()
        {
            return AppDomain.CreateDomain(
                    "Testura external assembly generator domain",
                    null,
                    new AppDomainSetup { ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase, ApplicationName = "Testura" });
        }

        private T CreateProxy<T>(AppDomain domain)
            where T : class
        {
            var activator = typeof(T);
            return domain.CreateInstanceAndUnwrap(
                        activator.Assembly.FullName,
                        activator.FullName) as T;
        }
    }
}
