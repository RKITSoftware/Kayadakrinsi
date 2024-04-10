using System.Runtime.Caching;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using BillingAPI.BusinessLogic;
using BillingAPI.Models.POCO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;

namespace BillingAPI.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// Declares object of class BLUser
        /// </summary>
        public BLUSR01 objBLUser = new BLUSR01();

        public BLTokenManager objBLTokenManager = new BLTokenManager();

        public BLConvertor objBLConvertor = new BLConvertor();

        /// <summary>
        /// Authenticates user using user's JWT token
        /// </summary>
        /// <param name="actionContext">Information of executing context</param>
        public void OnAuthorization(AuthorizationFilterContext actionContext)
        {
            if (AuthenticationFilter.SkipAuthorization(actionContext)) return;

            var token = BLTokenManager.cache.Get("JWTToken_" + AuthenticationFilter.objUSR01.R01F02).ToString();

            // Validate JWT token
            if (token != null && objBLTokenManager.ValidateToken(token))
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

                //List<USR01> lstUSR01 = objBLConvertor.DataTableToList<USR01>(objBLUser.Select().response);
                //USR01 user = lstUSR01.FirstOrDefault(u => u.R01F02 == json["unique_name"].ToString());

                USR01 user = AuthenticationFilter.objUSR01;


                // create an identity => i.e., attach username which is used to identify the user
                GenericIdentity identity = new GenericIdentity(user.R01F02);

                // add claims for the identity => a claim has (claim_type, value)
                identity.AddClaim(new Claim("Id", Convert.ToString(user.R01F01)));

                string[] roles = user.R01F04.ToString().Split(',');

                // create a principal that represent a user => it has an (identity object + roles)
                IPrincipal principal = new GenericPrincipal(identity, roles);

                // now associate the user/principal with the thread
                Thread.CurrentPrincipal = principal;


                actionContext.HttpContext.User = (ClaimsPrincipal)principal;

                return;
            }
            else
            {
                actionContext.Result = new UnauthorizedResult();
            }

        }
        
    }
}
