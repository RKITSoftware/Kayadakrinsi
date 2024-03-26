using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersAPI.Filters
{
    /// <summary>
    /// Custom result filter
    /// </summary>
    public class ResultFilter : Attribute,IResultFilter
    {
        /// <summary>
        /// Declares that result has been executed
        /// </summary>
        /// <param name="context">Context of result filter</param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            if (context.Exception == null)
            {
                Console.WriteLine("Result executed");
            }
        }

        /// <summary>
        /// Adds response header while executing result
        /// </summary>
        /// <param name="context">Context of result filter</param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            var headerName = "OnResultExecuting";
            context.HttpContext.Response.Headers.Add(headerName, new string[] { "MyPageHeader" });
        }
    }
}
