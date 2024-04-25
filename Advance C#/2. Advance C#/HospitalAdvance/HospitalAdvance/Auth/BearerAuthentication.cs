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
using HospitalAdvance.BusinessLogic;
using HospitalAdvance.Models;
using Newtonsoft.Json.Linq;

namespace HospitalAdvance.Auth
{
    /// <summary>
    /// Authenticates user using user's JWT token
    /// </summary>
    public class BearerAuthentication : AuthorizationFilterAttribute
    {

        /// <summary>
        /// Authenticates user using user's JWT token and refreshes token if user is authenticated
        /// </summary>
        /// <param name="actionContext">Information of executing context</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (BasicAuthentication.SkipAuthorization(actionContext)) return;

            var token = BLTokenHandler.cache.Get("JWTToken_" + BasicAuthentication.objUSR01.R01F02).ToString();

            // Validate JWT token
            if (token != null && BLTokenHandler.ValidateToken(token))
            {

                // get jwt payload
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

                //List<USR01> lstUSR01 = objBLCommon.DataTableToList<USR01>(objBLUser.Select().response);
                //USR01 user = lstUSR01.FirstOrDefault(u => u.R01F02 == json["unique_name"].ToString());

                USR01 user = BasicAuthentication.objUSR01;


                // create an identity => i.e., attach username which is used to identify the user
                GenericIdentity identity = new GenericIdentity(user.R01F02);

                // add claims for the identity => a claim has (claim_type, value)
                identity.AddClaim(new Claim("Id", Convert.ToString(user.R01F01)));

                string[] roles = user.R01F04.ToString().Split(',');

                // create a principal that represent a user => it has an (identity object + roles)
                IPrincipal principal = new GenericPrincipal(identity, roles);

                // now associate the user/principal with the thread
                Thread.CurrentPrincipal = principal;


                HttpContext.Current.User = (ClaimsPrincipal)principal;

                return;
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Token");
            }
        }
    }

}