using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace HospitalAPI.Auth
{
	/// <summary>
	/// Authorization filter attribute
	/// </summary>
	public class BasicAuthorizationAttribute : AuthorizeAttribute
    {
        #region Protected Methods

        /// <summary>
        /// Authorizes user
        /// </summary>
        /// <param name="actionContext">Information of excuting context</param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }

        #endregion
    }
}