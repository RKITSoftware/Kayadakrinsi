using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersAPI.Filters
{
    /// <summary>
    /// Custom asynchronous result filter
    /// </summary>
    public class AsyncResultFilter : Attribute,IAsyncResultFilter
    {
        /// <summary>
        /// Adds response header asynchoronously 
        /// </summary>
        /// <param name="context">Context of result filter</param>
        /// <param name="next">Next filter in pipeline</param>
        /// <returns>Task</returns>
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var headerName = "OnResultExecutionAsync";

            context.HttpContext.Response.Headers.Add(headerName, new string[] { "MyPageHeader" });

            ResultExecutedContext resultContext = await next();

            if (resultContext.Exception != null)
            {
                resultContext.ExceptionHandled = true;
                resultContext.Canceled = true;
            }
        }
    }
}
