using System.Diagnostics.Contracts;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using System.Text;
using BillingAPI.BusinessLogic;
using BillingAPI.Models.POCO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

using Microsoft.AspNetCore.Mvc.Filters;

namespace BillingAPI.Filters
{
    /// <summary>
    /// Custom basic authentication filter
    /// </summary>
    public class AuthenticationFilter : Attribute, IAuthorizationFilter
    {

        #region Public Members

        /// <summary>
        /// Declares object of class BLUser
        /// </summary>
        public BLLogin objBLLogin = new BLLogin();

        /// <summary>
        /// Instance of BLTokenManager class
        /// </summary>
        public BLTokenManager objBLTokenManager = new BLTokenManager();

        /// <summary>
        /// Instance of USR01 class
        /// </summary>
        public static USR01 objUSR01;

        /// <summary>
        /// Cache prefix to store tokens
        /// </summary>
        public const string CachePrefix = "tokenGenerated_";

        /// <summary>
        /// Flag defines whether token is generated or not
        /// </summary>
        public static MemoryCache tokenGenerated = MemoryCache.Default;

        #endregion

        #region Public Methods

        /// <summary>
        /// Authenticates user
        /// </summary>
        /// <param name="context">Context of authorization filter</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Retrieve the endpoint information
            var endpoint = context.HttpContext.GetEndpoint();

            // Check if the endpoint is excluded from authentication
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                return; // Skip authentication for this endpoint
            }

            string authHeader = context.HttpContext.Request.Headers["Authorization"];

            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

                try
                {
                    string credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeaderVal.Parameter));

                    string[] userInfo = credentials.Split(':');
                    string username = userInfo[0];
                    string password = userInfo[1];

                    var user = objBLLogin.ValidateUser(username, password);

                    if (user != null)
                    {
                        // Generates token
                        var token = objBLTokenManager.GenerateToken(user);

                        // Attaches principal to token
                        var principal = objBLTokenManager.GetPrincipal(token);

                        if (principal == null)
                        {
                            context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                            return;
                        }

                        //Set the current principal for the request
                        Thread.CurrentPrincipal = principal;

                        // Set the token as the result (if needed)
                        context.Result = new ContentResult
                        {
                            Content = token,
                            ContentType = "text/plain",
                            StatusCode = StatusCodes.Status200OK
                        };
                        return;
                    }
                    else
                    {
                        // User validation failed
                        context.Result = new ContentResult
                        {
                            Content = "Invalid username or password.",
                            ContentType = "text/plain",
                            StatusCode = StatusCodes.Status401Unauthorized
                        };
                        return;
                    }
                }
                catch (FormatException)
                {
                    // Credentials were not formatted correctly.
                    context.Result = new ContentResult
                    {
                        Content = "Invalid Authorization header format.",
                        ContentType = "text/plain",
                        StatusCode = StatusCodes.Status401Unauthorized
                    };
                    return;
                }
            }
            else
            {
                // No or invalid authorization header
                context.Result = new ContentResult
                {
                    Content = "Authorization header is missing or not in the correct format.",
                    ContentType = "text/plain",
                    StatusCode = StatusCodes.Status400BadRequest
                };
                return;
            }
        }

        /// <summary>
        /// Skips authentication process when AllowAnonymous is applied
        /// </summary>
        /// <param name="context">Authorization filter context</param>
        /// <returns>True or false</returns>
        public static bool SkipAuthorization(AuthorizationFilterContext context)
        {
            Contract.Assert(context != null);
            if (context.ActionDescriptor is ControllerActionDescriptor descriptor)
            {
                var actionAttributes =
                   descriptor.MethodInfo.GetCustomAttributes(inherit: true);
                if (actionAttributes.FirstOrDefault(a => a.GetType() == typeof(AllowAnonymousAttribute)) != null)
                    return true;
            }
            return false;
        }

        #endregion
    }
}
