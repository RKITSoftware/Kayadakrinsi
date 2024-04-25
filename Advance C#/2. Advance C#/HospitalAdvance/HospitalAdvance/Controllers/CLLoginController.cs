using System.Web.Http;
using HospitalAdvance.Auth;
using HospitalAdvance.BusinessLogic;

namespace HospitalAdvance.Controllers
{
    /// <summary>
    /// Logs in user after authenticate
    /// </summary>
    public class CLLoginController : ApiController
	{
		/// <summary>
		/// Declares object of class BLUSR01Handler
		/// </summary>
		public BLUSR01Handler objBLUSR01Handler;

		/// <summary>
		/// Initializes objects
		/// </summary>
        public CLLoginController()
        {
			objBLUSR01Handler = new BLUSR01Handler();
        }

        /// <summary>
        /// Generates token for valid user
        /// </summary>
        /// <returns>Custom JWT Token</returns>
        [HttpPost]
		[AllowAnonymous]
        [BasicAuthentication]
        [Route("api/CLLogin/token")]
		public IHttpActionResult GetToken()
		{
			string[] usernamepassword = objBLUSR01Handler.GetUsernamePassword(Request);
			string username = usernamepassword[0];
			string password = usernamepassword[1];
			var userDetails = objBLUSR01Handler.GetUser(username, password);
			
			return Ok(BLTokenHandler.GenerateToken(userDetails));

		}
	}
}