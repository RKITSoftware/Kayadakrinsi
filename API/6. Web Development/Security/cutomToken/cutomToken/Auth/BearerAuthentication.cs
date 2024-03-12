using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using cutomToken.BusinessLogic;
using cutomToken.Models;
using Newtonsoft.Json.Linq;

namespace cutomToken.Auth
{

    /// <summary>
    /// Authenticate the reuqest with jwt (bearer) as authentication type
    /// </summary>
    public class BearerAuthentication : AuthorizationFilterAttribute 
    {
        /// <summary>
        /// Authenticates user using user's JWT token
        /// </summary>
        /// <param name="actionContext">Information of executing context</param>
        public override void OnAuthorization(HttpActionContext actionContext)
         {
            if (BasicAuthentication.SkipAuthorization(actionContext)) return;

            // Check if the request has Authorization header
            if (!actionContext.Request.Headers.Contains("Authorization"))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return;
            }

            // Extract token from Authorization header
            var token = actionContext.Request.Headers.GetValues("Authorization").FirstOrDefault()?.Replace("Bearer ", "");
            
            if (BLTokenManager.IsTokenExpired(token))
            {
                // Token is expired, refresh it
                token = BLTokenManager.RefreshToken(token);

                if (token == null)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    return;
                }
            }
            // Validate JWT token
            if (token != null && BLTokenManager.ValidateToken(token))
            {
                
                string jwtEncodedPayload = token.Split('.')[1];

                // pad jwtEncodedPayload
                jwtEncodedPayload = jwtEncodedPayload.Replace('+', '-')
                                                     .Replace('/', '_')
                                                     .Replace("=", "");

                int padding = jwtEncodedPayload.Length % 4;
                if (padding != 0)
                {
                    jwtEncodedPayload += new string('=', 4 - padding);
                }

                // decode the jwt payload
                byte[] decodedPayloadBytes = Convert.FromBase64String(jwtEncodedPayload);

                string decodedPayload = Encoding.UTF8.GetString(decodedPayloadBytes);

                JObject json = JObject.Parse(decodedPayload);

                USR01 user = BLUser.lstUser.FirstOrDefault(u => u.R01F02 == json["unique_name"].ToString());


                // create an identity => i.e., attach username which is used to identify the user
                GenericIdentity identity = new GenericIdentity(user.R01F02);

                // add claims for the identity => a claim has (claim_type, value)
                identity.AddClaim(new Claim("Id", Convert.ToString(user.R01F01)));

                // create a principal that represent a user => it has an (identity object + roles)
                IPrincipal principal = new GenericPrincipal(identity, user.R01F04.Split(','));

                // now associate the user/principal with the thread
                Thread.CurrentPrincipal = principal;

                if (HttpContext.Current != null)
                {
                    // HttpContext is responsible for rq and res.
                    HttpContext.Current.User = principal;
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,
                                             "Authorization denied");
                }

            }
        }

    }
}