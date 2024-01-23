using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace ExceptionFilter.BusinessLogic
{
    /// <summary>
    /// Filter for handling not implemented methods
    /// </summary>
    public class NotImplExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is NotImplementedException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented)
                {
                    Content = new StringContent("Not Implemented Method")
                    //Content = new StringContent(context.Exception.Message)
                };

            }
        }
    }

}