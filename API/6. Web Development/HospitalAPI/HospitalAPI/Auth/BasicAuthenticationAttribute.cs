using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace HospitalAPI.Auth
{ /// <summary>
  /// Custom authentication filter attribute
  /// </summary>
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        #region Public Methods

        /// <summary>
        /// Authenticates user with username, password and authorizes further
        /// </summary>
        /// <param name="actionContext">Information of excuting context</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                if (actionContext.Request.Headers.Authorization == null)
                {
                    actionContext.Response = actionContext.Request
                        .CreateErrorResponse(HttpStatusCode.Unauthorized, "Login failed");
                }
                else
                {
                    string authToken = actionContext.Request.Headers.Authorization.Parameter;
                    // username:password in base64 encoded

                    string decodedAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                    string[] usernamepassword = decodedAuthToken.Split(':');
                    string username = usernamepassword[0];
                    string password = usernamepassword[1];

                    if (BLUser.Login(username, password))
                    {
                        var userDetails = BLUser.GetUserDetails(username, password);
                        var identity = new GenericIdentity(username);
                        identity.AddClaim(new Claim(ClaimTypes.Name, userDetails.R01F02));
                        identity.AddClaim(new Claim("Id", Convert.ToString(userDetails.R01F01)));

                        IPrincipal principal = new GenericPrincipal(identity, userDetails.R01F04.Split(','));

                        Thread.CurrentPrincipal = principal;
                        if (HttpContext.Current != null)
                        {
                            HttpContext.Current.User = principal;
                        }
                        else
                        {
                            actionContext.Response = actionContext.Request
                            .CreateErrorResponse(HttpStatusCode.Unauthorized, "Authorization Denied");
                        }
                    }
                    else
                    {
                        actionContext.Response = actionContext.Request
                            .CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid Creditentials");
                    }
                }
            }
            catch (Exception)
            {
                actionContext.Response = actionContext.Request
                            .CreateErrorResponse(HttpStatusCode.InternalServerError, 
                            "Login failed due to Internal Server Error");
            }

        }

        #endregion
    }
}