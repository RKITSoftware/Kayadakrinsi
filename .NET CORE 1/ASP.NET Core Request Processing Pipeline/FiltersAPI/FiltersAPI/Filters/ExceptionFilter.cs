using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using NLog.Web;

namespace FiltersAPI.Filters
{
    /// <summary>
    /// Custom Exception filter
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Declares object of Logger
        /// </summary>
        private readonly Logger _logger;

        /// <summary>
        /// Initializes logger from nlog.config file
        /// </summary>
        public ExceptionFilter()
        {
            _logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        }

        /// <summary>
        /// Logs exception into file 
        /// </summary>
        /// <param name="context">Context of exception filter</param>
        public void OnException(ExceptionContext context)
        {
            Exception exception = context.Exception;

            StackTrace s = new StackTrace(exception);
            Assembly thisAssembly = Assembly.GetExecutingAssembly();
            string methodname = s.GetFrames().Select(f => f.GetMethod()).First(m => m.Module.Assembly == thisAssembly).Name;

            string message = string.Format($"{exception.GetType()} | {methodname} | {exception.Message} \n{exception.StackTrace}\n");

            _logger.Error(message);

            throw exception;
        }
    }
}
