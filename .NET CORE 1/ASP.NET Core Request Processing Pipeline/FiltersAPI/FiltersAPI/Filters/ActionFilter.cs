using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersAPI.Filters
{
    /// <summary>
    /// Custom action filter
    /// </summary>
    public class ActionFilter : IActionFilter
    {
        /// <summary>
        /// Declares logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes logger
        /// </summary>
        /// <param name="logger"></param>
        public ActionFilter(ILogger<ActionFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Checks if body is not null 
        /// </summary>
        /// <param name="context">Context of action filter</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var body = context.HttpContext.Request.Body;

            if (body != null)
            {
                return;
            }
            else
            {
                context.Result = new BadRequestObjectResult("Null data found!");
            }

            _logger.LogInformation($"Action executing: {context.ActionDescriptor.DisplayName}");
        }

        /// <summary>
        /// Assigns result equal to result of action method
        /// </summary>
        /// <param name="context">Context of action filter</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"Action executed: {context.ActionDescriptor.DisplayName}");

            if (context.Result is ObjectResult objectResult)
            {
                context.Result = new ObjectResult(objectResult);
            }
        }
    }
}
