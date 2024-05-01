using BillingAPI.BusinessLogic;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BillingAPI.Filters
{
    /// <summary>
    /// Custom action filter
    /// </summary>
    public class ActionExecutedFilter : Attribute, IActionFilter
    {
        /// <summary>
        /// Sets currents company after insert or update company
        /// </summary>
        /// <param name="context">Action filter context</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            BLCMP01.SetCurrentCompany();
        }

        /// <summary>
        /// Perform task before action executes (Here Empty)
        /// </summary>
        /// <param name="context">Action filter context</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
