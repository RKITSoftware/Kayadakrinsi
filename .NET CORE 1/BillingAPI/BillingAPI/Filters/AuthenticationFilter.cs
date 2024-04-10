using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
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
        /// <summary>
        /// Declares object of class BLUser
        /// </summary>
        public BLUSR01 objBLUser = new BLUSR01();

        public BLTokenManager objBLTokenManager = new BLTokenManager();

        public BLConvertor objBLConvertor = new BLConvertor();

        public static USR01 objUSR01;

        public static bool isTokenGenerated = false;

        /// <summary>
        /// Validates user
        /// </summary>
        /// <param name="username">Username of user</param>
        /// <param name="password">Password of user</param>
        /// <returns>True if user is valid, false otherwise</returns>
        private USR01? ValidateUser(string username, string password)
        {
            List<USR01> lstUSR01 = objBLConvertor.DataTableToList<USR01>(objBLUser.Select().response);
            var user = lstUSR01.FirstOrDefault(u => u.R01F02 == username && u.R01F03 == password);

            if (user != null)
            {
                return user;
            }

            return null;
        }

        ///// <summary>
        ///// Returns user with current credential
        ///// </summary>
        ///// <param name="username">Username of user</param>
        ///// <param name="password">Password of user</param>
        ///// <returns>Object of class USR01</returns>
        //private USR01 UserDetails(string username, string password)
        //{
        //    List<USR01> lstUSR01 = objBLConvertor.DataTableToList<USR01>(objBLUser.Select().response);
        //    var user = lstUSR01.FirstOrDefault(u => u.R01F02 == username && u.R01F03 == password);

        //    return user;
        //}

        /// <summary>
        /// Authenticates user
        /// </summary>
        /// <param name="context">Context of authorization filter</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (SkipAuthorization(context)) return;

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
                    var user = ValidateUser(username, password);
                    if (user!=null)
                    {
                        objUSR01 = user;

                        var identity = new GenericIdentity(username);
                        identity.AddClaim(new Claim(ClaimTypes.Name, objUSR01.R01F02));
                        identity.AddClaim(new Claim("Id", Convert.ToString(objUSR01.R01F01)));

                        IPrincipal principal = new GenericPrincipal(identity, objUSR01.R01F04.ToString().Split(','));

                        Thread.CurrentPrincipal = principal;

                        context.HttpContext.User = (ClaimsPrincipal)principal;

                        if (isTokenGenerated == false)
                        {
                            objBLTokenManager.GenerateToken(objUSR01);
                            isTokenGenerated = true;
                            return;
                        }

                        var token = BLTokenManager.cache.Get("JWTToken_" + objUSR01.R01F02);

                        if (token != null)
                        {
                            token = objBLTokenManager.RefreshToken(objUSR01);

                            if (token == null)
                            {
                                context.Result = new UnauthorizedResult();
                            }
                            return;
                        }
                        else
                        {
                            context.Result = new UnauthorizedResult();
                            isTokenGenerated = false;
                        }
                    }
                    else
                    {
                        context.Result = new UnauthorizedResult();
                    }
                }
                catch (FormatException)
                {
                    // Credentials were not formatted correctly.
                    context.HttpContext.Response.StatusCode = 401;
                }
            }
            else
            {
                context.Result = new UnauthorizedResult();
            }
        }

        public static bool SkipAuthorization(AuthorizationFilterContext context)
        {
            Contract.Assert(context != null);
            if (context.ActionDescriptor is ControllerActionDescriptor descriptor)
            {
                var actionAttributes =
                   descriptor.MethodInfo.GetCustomAttributes(inherit: true);
                if (actionAttributes.FirstOrDefault(a => a.GetType() == typeof(AllowAnonymousAttribute)) != null) return true;
            }
            return false;
        }
    }
}
