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

            string tokenValue = actionContext.Request.Headers.Authorization.Parameter;

            // check jwt token's validity
            var isValid = BLTokenManager.ValidateToken(tokenValue);

            // if jwt is invalid (corrupted-jwt or jwt-expired) then return unauthorized
            if (!isValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Enter valid token");
            }
            else
            {
                // get jwt payload
                string jwtEncodedPayload = tokenValue.Split('.')[1];

                // pad jwtEncodedPayload
                jwtEncodedPayload = jwtEncodedPayload + new string('=', (4 - (jwtEncodedPayload.Length % 4)));

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
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Authorization denied");
                }
            }
        }
    }
}