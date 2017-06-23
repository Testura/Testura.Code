using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using Testura.Code.Util.AppDomains.Proxies;

namespace Testura.Code.Util.AppDomains
{
    /// <summary>
    /// Provides the functionality to generate code in a different app domain.
    /// </summary>
    public class AppDomainCodeGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDomainCodeGenerator"/> class.
        /// </summary>
        public AppDomainCodeGenerator()
        {
            ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            Evidence = null;
            Permissions = new PermissionSet(PermissionState.Unrestricted);
        }

        /// <summary>
        /// Gets or sets the application base for the new app domain.
        /// </summary>
        public string ApplicationBase { get; set; }

        /// <summary>
        /// Gets or sets the app domain permissions.
        /// </summary>
        public PermissionSet Permissions { get; set; }

        /// <summary>
        /// Gets or sets the app domain evidence.
        /// </summary>
        public Evidence Evidence { get; set; }

        /// <summary>
        /// Load an external assembly and generate code inside a different app domain. Will unload
        /// app domain after finish generating code.
        /// </summary>
        /// <typeparam name="T">Type of custom generator proxy to use.</typeparam>
        /// <param name="assembly">Path to the external assembly.</param>
        /// <param name="customCodeGeneratorProxy">The custom code generator proxy to use.</param>
        /// <param name="extraData">Extra data that we send with the proxy.</param>
        public void GenerateCode<T>(string assembly, T customCodeGeneratorProxy, IDictionary<string, object> extraData = null)
            where T : CodeGeneratorProxy
        {
            var domain = CreateDomain();
            var proxy = CreateProxy<T>(domain);
            proxy.GenerateCode(assembly, extraData);
            AppDomain.Unload(domain);
        }

        /// <summary>
        /// Load an external assembly and generate code inside a different app domain. Will unload
        /// app domain after finish generating code.
        /// </summary>
        /// <param name="assembly">Path to the external assembly.</param>
        /// <param name="generateCode">Action to invoke inside the new app domain.</param>
        /// <param name="extraData">Extra data that we send with the proxy.</param>
        public void GenerateCode(string assembly, Action<Assembly, IDictionary<string, object>> generateCode, IDictionary<string, object> extraData = null)
        {
            var domain = CreateDomain();
            var proxy = CreateProxy<ActionCodeGeneratorProxy>(domain);
            try
            {
                proxy.GenerateCode(assembly, generateCode, extraData);
            }
            catch (Exception)
            {
                AppDomain.Unload(domain);
                throw;
            }

            AppDomain.Unload(domain);
        }

        private AppDomain CreateDomain()
        {
            return AppDomain.CreateDomain(
                "Testura external assembly generator domain",
                Evidence,
                new AppDomainSetup { ApplicationBase = ApplicationBase, ApplicationName = "Testura" },
                Permissions);
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