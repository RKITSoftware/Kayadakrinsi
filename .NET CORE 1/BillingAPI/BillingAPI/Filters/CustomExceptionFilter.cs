using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace BillingAPI.Filters
{
    /// <summary>
    /// Custom Exception filter
    /// </summary>
    public class CustomExceptionFilter : IAsyncExceptionFilter
    {
        /// <summary>
        /// Instance of Nlog Logger
        /// </summary>
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets a value indicating whether multiple instances of this filter can be applied.
        /// </summary>
        public bool AllowMultiple => throw new NotImplementedException();

        /// <summary>
        /// Logs exception into file 
        /// </summary>
        /// <param name="context">Context of exception filter</param>
        public Task OnExceptionAsync(Microsoft.AspNetCore.Mvc.Filters.ExceptionContext context)
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
