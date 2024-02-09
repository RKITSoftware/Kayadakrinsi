﻿using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using HospitalAPI.BusinesLogic;

namespace HospitalAPI.Auth
{
    /// <summary>
    /// Authenticates user who wants to get JWT token
    /// </summary>
    public class JWTAuthentication : ActionFilterAttribute
    {
        /// <summary>
        /// Authenticates user and creates response accordingly
        /// </summary>
        /// <param name="actionContext">Information about executing cintext</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // Authorization header
            var authHeader = actionContext.Request.Headers.Authorization;

            if (authHeader == null)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,
                                         "invalid headers");
            }
            else if (authHeader.Scheme == "Basic")
            {
                string[] usernamePassword = BLUser.GetUsernamePassword(actionContext.Request);

                //Extracts username and password from the decoded token.
                string username = usernamePassword[0];
                string password = usernamePassword[1];

                var user = BLUser.GetUserDetails(username, password);

                if (user != null)
                {
                    // Generates token
                    var token = BLTokenManager.GenerateToken(username);

                    // Attaches principal to token
                    var principal = BLTokenManager.GetPrincipal(token);

                    if (principal == null)
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,
                                                 "Invalid token");
                    }

                    //Set the current principal for the request
                    Thread.CurrentPrincipal = principal;

                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, token);
                }
                base.OnActionExecuting(actionContext);
            }
        }
    }
}