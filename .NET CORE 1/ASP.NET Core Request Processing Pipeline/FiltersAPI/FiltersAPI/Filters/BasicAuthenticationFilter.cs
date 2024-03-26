using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using FiltersAPI.BusinessLogic;
using FiltersAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersAPI.Filters
{
    /// <summary>
    /// Custom basic authentication filter
    /// </summary>
    public class BasicAuthenticationFilter : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// Validates user
        /// </summary>
        /// <param name="username">Username of user</param>
        /// <param name="password">Password of user</param>
        /// <returns>True if user is valid, false otherwise</returns>
        private bool ValidateUser(string username, string password)
        {
            var user = BLUser.lstUSR01.FirstOrDefault(u => u.R01F02 == username && u.R01F03 == password);

            if (user != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns user with current credential
        /// </summary>
        /// <param name="username">Username of user</param>
        /// <param name="password">Password of user</param>
        /// <returns>Object of class USR01</returns>
        private USR01 UserDetails(string username, string password)
        {
            var user = BLUser.lstUSR01.FirstOrDefault(u => u.R01F02 == username && u.R01F03 == password);

            return user;
        }

        /// <summary>
        /// Authenticates user
        /// </summary>
        /// <param name="context">Context of authorization filter</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string authHeader = context.HttpContext.Request.Headers["Authorization"];

            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

                if (authHeaderVal.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) &&
                    authHeaderVal.Parameter != null)
                {
                    try
                    {
                        string credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeaderVal.Parameter));

                        string[] userInfo = credentials.Split(':');
                        string username = userInfo[0];
                        string password = userInfo[1];

                        if (ValidateUser(username, password))
                        {
                            var user = UserDetails(username, password);

                            var identity = new GenericIdentity(username);
                            identity.AddClaim(new Claim(ClaimTypes.Name, user.R01F02));
                            identity.AddClaim(new Claim("Id", Convert.ToString(user.R01F01)));


                            IPrincipal principal = new GenericPrincipal(identity, user.R01F04.ToString().Split(','));

                            Thread.CurrentPrincipal = principal;

                            context.HttpContext.User = (ClaimsPrincipal)principal;
                            return;
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
            else
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
