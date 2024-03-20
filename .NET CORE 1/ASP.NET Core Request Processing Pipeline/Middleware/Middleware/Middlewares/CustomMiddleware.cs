namespace Middleware.Middlewares
{
    /// <summary>
    /// Custom middleware class
    /// </summary>
    public class CustomMiddleware : IMiddleware
    {
        /// <summary>
        /// Implements InvokeAsync method of IMiddleware
        /// </summary>
        /// <param name="context">Current request context</param>
        /// <param name="next">Next middleware in pipeline</param>
        /// <returns>Message</returns>
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            //  context.Response.WriteAsync("\nHello from custom middleware");
            context.Response.Headers.Add("CustomMiddleware", "Hello from custom middleware");

            return next(context);
        }
    }
}
