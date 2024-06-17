using BillingAPI.Interfaces;
using BillingAPI.Models.POCO;
using BillingAPI.Repositaries;
using NLog;
using NLog.Targets;

namespace BillingAPI.Extentions
{
    /// <summary>
    /// Contains extension methods for various functionalities.
    /// </summary>
    public static class Extentions
    {
        /// <summary>
        /// Changes the file path for a specific NLog target
        /// </summary>
        /// <param name="logFactory">The NLog log factory</param>
        /// <param name="targetName">The name of the target to modify</param>
        /// <param name="newFilePath">The new file path to set for the target</param>
        public static void ChangeTargetFilePath(this LogFactory logFactory, string targetName, string newFilePath)
        {
            var config = logFactory.Configuration;
            var fileTarget = config.FindTargetByName<FileTarget>(targetName);

            if (fileTarget != null)
            {
                fileTarget.FileName = new NLog.Layouts.SimpleLayout(newFilePath);
                logFactory.ReconfigExistingLoggers();
            }
            else
            {
                logFactory.GetCurrentClassLogger().Error($"Target '{targetName}' not found.");
            }
        }

        /// <summary>
        /// Registers custom services to the dependency injection container
        /// </summary>
        /// <param name="services">The collection of services to add custom services to</param>
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<ICRUDService<PRO01>, CRUDImplementation<PRO01>>();
            services.AddTransient<ICRUDService<CMP01>, CRUDImplementation<CMP01>>();
            services.AddTransient<ICRUDService<BIL01>, CRUDImplementation<BIL01>>();
            services.AddTransient<IBIL01Service, DbBIL01Context>();
            // services.AddSingleton<ResourceFilter>
            // services.AddTransient<ICRUD<USR01>, CRUDImplementation<USR01>>();
            // services.AddTransient<IActionFilter, ActionExecutedFilter>();
        }
    }
}
