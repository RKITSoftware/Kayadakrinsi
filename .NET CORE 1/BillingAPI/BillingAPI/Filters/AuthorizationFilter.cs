using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using BillingAPI.BusinessLogic;
using BillingAPI.Enums;
using BillingAPI.Models.POCO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;

namespace BillingAPI.Filters
{
    /// <summary>
    /// Authorizes user
    /// </summary>
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        #region Public Members

        /// <summary>
        /// Instance of BLTokenManager class
        /// </summary>
        public BLTokenManager objBLTokenManager = new BLTokenManager();

        /// <summary>
        /// Instance of BLLogin class
        /// </summary>
        public BLLogin objBLLogin = new BLLogin();

        /// <summary>
        /// User roles
        /// </summary>
        public string Roles { get; set; }


        #endregion

        #region Constructors

        /// <summary>
        /// Initializes roles
        /// </summary>
        /// <param name="roles">Role of user</param>
        public AuthorizationFilter(string roles)
        {
            Roles = roles;
        }

        #endregion


        /// <summary>
        /// Authenticates user using user's JWT token
        /// </summary>
        /// <param name="actionContext">Information of executing context</param>
        public void OnAuthorization(AuthorizationFilterContext actionContext)
        {
            if (AuthenticationFilter.SkipAuthorization(actionContext)) return;

            // Retrives token from cache
            string token = BLTokenManager.cache.Get("JWTToken_" + AuthenticationFilter.objUSR01.R01F02).ToString();

            // Validates token
            if (token != null && !objBLTokenManager.IsTokenExpired(token) && objBLTokenManager.ValidateToken(token))
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

                USR01 user = objBLLogin.GetUsers().FirstOrDefault(u => u.R01F02 == json["unique_name"].ToString());

                string[] userRoles = Roles.ToString().Split(',');

                if (!userRoles.Contains(user.R01F04.ToString()))
                {
                    actionContext.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                }
                else if(user != null)
                {
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
            }
            else
            {
                actionContext.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }
    }
}
