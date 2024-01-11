using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Authentication_Authorization.BasicAuth
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                if (actionContext.Request.Headers.Authorization==null)
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

                    if(ValidateUser.Login(username, password))
                    {
                        var userDetails = ValidateUser.GetUserDetails(username, password);
                    }
                    else
                    {
                        actionContext.Response = actionContext.Request
                            .CreateErrorResponse(HttpStatusCode.Unauthorized, "Login failed");
                    }
                }
            }
            catch(Exception)
            {
                actionContext.Response = actionContext.Request
                            .CreateErrorResponse(HttpStatusCode.InternalServerError, "Login failed due to Internal Server Error");
            }
                
        }
    }
}