using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using HospitalAdvance.BusinessLogic;
using HospitalAdvance.Models;

namespace HospitalAdvance.Auth
{
    /// <summary>
    /// Authenticates user 
    /// </summary>
    public class BasicAuthentication : AuthorizationFilterAttribute
    {
        #region Public Members

        /// <summary>
        /// Instance of BLLogin class
        /// </summary>
        public BLLogin objBLLogin;

        /// <summary>
        /// Instance of USR01
        /// </summary>
        public static USR01 objUSR01;

        /// <summary>
        /// Flag defines whether token is generated or not
        /// </summary>
        public static bool isTokenGenerated = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes instances
        /// </summary>
        public BasicAuthentication()
        {
            objBLLogin = new BLLogin();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Authenticates user and generates token if user is authentiocated
        /// </summary>
        /// <param name="context">Context of authorization filter</param>
        public override void OnAuthorization(HttpActionContext context)
        {
            if (SkipAuthorization(context)) return;

            var authHeader = context.Request.Headers.Authorization;

            if (authHeader != null && authHeader.Scheme == "Basic")
            {
                string[] usernamePassword = objBLLogin.GetUsernamePassword(context.Request);

                //Extracts username and password from the decoded token.
                string username = usernamePassword[0];
                string password = usernamePassword[1];

                USR01 user = objBLLogin.ValidateUser(username, password);
                if (user != null)
                {
                    objUSR01 = user;

                    var identity = new GenericIdentity(username);
                    identity.AddClaim(new Claim(ClaimTypes.Name, objUSR01.R01F02));
                    identity.AddClaim(new Claim("Id", Convert.ToString(objUSR01.R01F01)));

                    IPrincipal principal = new GenericPrincipal(identity, objUSR01.R01F04.ToString().Split(','));

                    Thread.CurrentPrincipal = principal;

                    HttpContext.Current.User = (ClaimsPrincipal)principal;

                    if (isTokenGenerated == false)
                    {
                        BLTokenHandler.GenerateToken(objUSR01);
                        isTokenGenerated = true;
                        return;
                    }

                    var token = BLTokenHandler.cache.Get("JWTToken_" + objUSR01.R01F02);

                    if (token != null)
                    {
                        token = BLTokenHandler.RefreshToken(objUSR01);

                        if (token == null)
                        {
                            context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, "Error while producing token");
                        }
                        return;
                    }
                    else
                    {
                        context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, "Error while producing token");
                        isTokenGenerated = false;
                    }
                }
                else
                {
                    context.Response = context.Request.CreateResponse(HttpStatusCode.BadRequest, "User Not Found");
                }
            }
            else
            {
                context.Response = context.Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Headers");
            }
        }

        /// <summary>
        /// Skips authentication process when AllowAnonymous is applied
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        public static bool SkipAuthorization(HttpActionContext actionContext)
        {
            // Use Contract.Assert to ensure that the actionContext is not null.
            Contract.Assert(actionContext != null);

            // Check if the action or controller has the AllowAnonymousAttribute.
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                       || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }

        #endregion

    }
}