using System.Text.RegularExpressions;

namespace MiddleWareAPI.Middleware
{
    /// <summary>
    /// Custom middleware class
    /// </summary>
    public class ValidateQueryParameterMiddleware
    {
        /// <summary>
        /// Declares next delegate
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Assigns next delegate to next middleware in HTTP request pipeline
        /// </summary>
        /// <param name="next">Next middleware in HTTP request pipeline</param>
        public ValidateQueryParameterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Checks if data is not null while add user request encountred
        /// </summary>
        /// <param name="httpContext">HTTP request context</param>
        /// <returns>Appropriate data or message</returns>
        public Task Invoke(HttpContext httpContext)
        {

            var pattern = @".*MiddleWareAPI/CLUser/AddUserURL.*";
            var path = httpContext.Request.Path;
            if (Regex.IsMatch(path, pattern))
            {
                Microsoft.Extensions.Primitives.StringValues username, password;
                httpContext.Request.Query.TryGetValue("username", out username);
                httpContext.Request.Query.TryGetValue("password", out password);
                if (username.FirstOrDefault() == null || password.FirstOrDefault() == null)
                {
                    return httpContext.Response.WriteAsync("Null data found!");
                }

            }
            return _next(httpContext);
        }
    }

    /// <summary>
    /// Adds the middleware to the HTTP request pipeline.
    /// </summary>
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseValidateQueryParameterMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidateQueryParameterMiddleware>();
        }
    }
}
