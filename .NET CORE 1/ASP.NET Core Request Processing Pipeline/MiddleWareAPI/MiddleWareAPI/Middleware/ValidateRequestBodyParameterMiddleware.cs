using System.Text.RegularExpressions;
using MiddleWareAPI.Models;
using Newtonsoft.Json;

namespace MiddleWareAPI.Middleware
{
    /// <summary>
    /// Custom middleware class
    /// </summary>
    public class ValidateRequestBodyParameterMiddleware : IMiddleware
    {

        /// <summary>
        /// Checks if data is not null while add user request encountred
        /// </summary>
        /// <param name="httpContext">HTTP request context</param>
        /// <returns>Appropriate data or message</returns>
        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            var pattern = @".*MiddleWareAPI/CLUser/AddUserBody.*";
            var path = httpContext.Request.Path;

            if (Regex.IsMatch(path, pattern))
            {
                httpContext.Request.EnableBuffering(); // Enable request body buffering

                // Read the request body
                string requestBody = await new StreamReader(httpContext.Request.Body).ReadToEndAsync();

                // Reset stream position before leaving the middleware
                httpContext.Request.Body.Position = 0;

                // Deserialize the request body into USR01 object
                USR01 objUSR01 = JsonConvert.DeserializeObject<USR01>(requestBody);

                // Check if required fields are null
                if (objUSR01?.R01F02 == null || objUSR01?.R01F03 == null)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await httpContext.Response.WriteAsJsonAsync(new
                    {
                        Message = "Null Data Found!"
                    });
                    return;
                }
            }
            await next(httpContext);
        }
    }
}
