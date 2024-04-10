using System.Linq;
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
		/// Declares object of class BLUser
		/// </summary>
		public BLUSR01 objBLUser;

		/// <summary>
		/// Initializes objects
		/// </summary>
        public CLLoginController()
        {
			objBLUser = new BLUSR01();
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
			string[] usernamepassword = objBLUser.GetUsernamePassword(Request);
			string username = usernamepassword[0];
			string password = usernamepassword[1];
			var userDetails = objBLUser.Select().FirstOrDefault(user => user.R01F02.Equals(username) && user.R01F03 == password);

			return Ok(BLTokenManager.GenerateToken(userDetails));

		}
	}
}