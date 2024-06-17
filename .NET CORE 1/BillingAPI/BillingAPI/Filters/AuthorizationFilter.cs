using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using BillingAPI.BusinessLogic;
using BillingAPI.Models.POCO;
using Microsoft.AspNetCore.Authorization;
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
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Retrieve the endpoint information
            var endpoint = context.HttpContext.GetEndpoint();

            // Check if the endpoint is excluded from authentication
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                return; // Skip authentication for this endpoint
            }

            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                // Extract token from Authorization header
                var token = authorizationHeader.Substring("Bearer ".Length);

                if (objBLTokenManager.IsTokenExpired(token))
                {
                    context.Result = new ContentResult
                    {
                        Content = "Token has expired",
                        StatusCode = (int)HttpStatusCode.Unauthorized
                    };
                    return;
                }

                // Validate JWT token
                if (token != null && objBLTokenManager.ValidateToken(token))
                {
                    string jwtEncodedPayload = token.Split('.')[1];

                    // Pad jwtEncodedPayload
                    jwtEncodedPayload = jwtEncodedPayload.Replace('-', '+').Replace('_', '/');
                    int padding = 4 - (jwtEncodedPayload.Length % 4);
                    if (padding != 4)
                    {
                        jwtEncodedPayload += new string('=', padding);
                    }

                    // Decode the jwt payload
                    byte[] decodedPayloadBytes = Convert.FromBase64String(jwtEncodedPayload);
                    string decodedPayload = Encoding.UTF8.GetString(decodedPayloadBytes);
                    JObject json = JObject.Parse(decodedPayload);

                    USR01 user = objBLLogin.GetUsers().FirstOrDefault(u => u.R01F02 == json["unique_name"].ToString());

                    if (user == null)
                    {
                        context.Result = new ContentResult
                        {
                            Content = "User not found",
                            StatusCode = (int)HttpStatusCode.Unauthorized
                        };
                        return;
                    }

                    string[] userRoles = Roles.Split(',');

                    if (userRoles.Contains(user.R01F04.ToString()))
                    {
                        
                        // Create an identity => i.e., attach username which is used to identify the user
                        GenericIdentity identity = new GenericIdentity(user.R01F02);

                        // Add claims for the identity => a claim has (claim_type, value)
                        identity.AddClaim(new Claim("Id", Convert.ToString(user.R01F01)));

                        // Create a principal that represents a user => it has an (identity object + roles)
                        IPrincipal principal = new GenericPrincipal(identity, user.R01F04.ToString().Split(','));

                        // Now associate the user/principal with the thread
                        Thread.CurrentPrincipal = principal;

                        context.HttpContext.User = (ClaimsPrincipal)principal;
                    }
                    else
                    {
                        context.Result = new ContentResult
                        {
                            Content = "Authorization denied",
                            StatusCode = (int)HttpStatusCode.Unauthorized
                        };
                        return;
                    }
                }
                else
                {
                    context.Result = new ContentResult
                    {
                        Content = "Authorization denied",
                        StatusCode = (int)HttpStatusCode.Unauthorized
                    };
                    return;
                }
            }
            else
            {
                context.Result = new ContentResult
                {
                    Content = "Authorization header is missing or not in the correct format",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }
    }
}
