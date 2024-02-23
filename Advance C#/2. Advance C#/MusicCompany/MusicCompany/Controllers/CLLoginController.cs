using System.Linq;
using System.Web.Http;
using MusicCompany.Auth;
using MusicCompany.BusinessLogic;
using MusicCompany.Filter;

namespace MusicCompany.Controllers
{
	/// <summary>
	/// Logs in user after authenticate
	/// </summary>
	[CustomExceptionFilter]
	public class CLLoginController : ApiController
    {
		/// <summary>
		/// Generates token for valid user
		/// </summary>
		/// <returns>Custom JWT Token</returns>
		[HttpPost]
		[Route("api/CLCompany/token")]
		[TokenAuthentication]
		public IHttpActionResult GetToken()
		{
			string[] usernamepassword = BLUser.GetUsernamePassword(Request);
			string username = usernamepassword[0];
			string password = usernamepassword[1];
			var userDetails = BLUser.Select().FirstOrDefault(user => user.R01F02.Equals(username) && user.R01F03 == password);
			if (userDetails != null)
			{
				return Ok(BLTokenManager.GenerateToken(username));
			}
			return BadRequest("Enter valid user details");
		}
	}
}
