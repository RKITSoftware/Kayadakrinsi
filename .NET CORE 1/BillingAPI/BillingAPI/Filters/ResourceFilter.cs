using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BillingAPI.Filters
{
    /// <summary>
    /// Custom resource filter
    /// </summary>
    public class ResourceFilter : IResourceFilter, IAsyncResourceFilter
    {
        /// <summary>
        /// Declares object of local in-memory cache
        /// </summary>
        private readonly IMemoryCache _objCache;

        /// <summary>
        /// Initializes object of local in-memory cache
        /// </summary>
        /// <param name="memoryCache">Instance of IMemoryCache interface</param>
        public ResourceFilter(IMemoryCache memoryCache)
        {
            _objCache = memoryCache;
        }

        /// <summary>
        /// Gets value from memory cache
        /// </summary>
        /// <param name="context">Context of resource filter</param>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            try
            {
                var result = _objCache.Get("Result");
                context.Result = new ObjectResult(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Sets value in memory cache
        /// </summary>
        /// <param name="context">Context of resource filter</param>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            try
            {
                var result = context.HttpContext.Response.Body;
                _objCache.Set("Result", "Cached response", DateTime.Now.AddMinutes(5));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Sets and gets value using memory cache
        /// </summary>
        /// <param name="context">Context of resource filter</param>
        /// <param name="next">Next filter in pipeline</param>
        /// <returns>Task</returns>
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            try
            {
                //var cacheItem = context.HttpContext.Response.Body;
                _objCache.Set("ResultAsync", "Cached response asynchoronous", DateTime.Now.AddMinutes(5));

                await next();
                var result = _objCache.Get("ResultAsync");
                Console.WriteLine(result);
                context.Result = new ObjectResult(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
    }
}
