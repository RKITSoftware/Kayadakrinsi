using System.Diagnostics;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using HospitalAdvance.Auth;
using HospitalAdvance.BusinessLogic;

namespace HospitalAdvance.Controllers
{

	/// <summary>
	/// Handles methods to predorm operations related to user
	/// </summary>
	//[BasicAuthenticationAttribute]
	public class CLUserController : ApiController
	{
		#region Public Members

		/// <summary>
		/// Declares object of BLUser class
		/// </summary>
		public BLUser objBLUser;

		/// <summary>
		/// Declares object of Stopwatch class
		/// </summary>
		public static Stopwatch stopwatch;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes objects
		/// </summary>
		public CLUserController()
		{
			objBLUser = new BLUser();
			stopwatch = Stopwatch.StartNew();
		}

		#endregion

		/// <summary>
		/// Handles get reuest for getting user data
		/// </summary>
		/// <returns>All data from table USR01</returns>
		[HttpGet]
		[BearerAuthentication]
		[Authorize(Roles = "admin")]
		[Route("api/CLUser/GetUsers")]
		public IHttpActionResult GetUsers()
		{
			var data = BLUser.Select();

			stopwatch.Stop();
			long responseTime = stopwatch.ElapsedTicks;

			HttpContext.Current.Response.AddHeader("Response-time", responseTime.ToString());

			return Ok(data);
		}

		/// <summary>
		/// Handles get reuest for getting user data
		/// </summary>
		/// <returns>All data from table USR01</returns>
		[HttpGet]
		[BearerAuthentication]
		[Authorize(Roles = "admin")]
		[Route("api/CLUser/GetCache")]
		public IHttpActionResult GetCache()
		{
			return Ok(objBLUser.GetCache());
		}

		/// <summary>
		/// Adds users 
		/// </summary>
		/// <param name="model">object of USR01,object of their respect role's model</param>
		/// <returns>Appropriate message</returns>
		[HttpPost]
		[BearerAuthentication]
		[Authorize(Roles = "admin")]
		[Route("api/CLUser/AddUser")]
		public HttpResponseMessage AddUser([FromBody] UserAddModel model)
		{
			return Request.CreateResponse(objBLUser.InsertData(model));
		}

		/// <summary>
		/// Updates user
		/// </summary>
		/// <param name="objUSR01">object of USR01 class to be edit</param>
		/// <returns>Appropriate Message</returns>
		[HttpPut]
		[BearerAuthentication]
		[Authorize(Roles = "Admin")]
		[Route("api/CLUser/EditUser")]
		public HttpResponseMessage EditUsers([FromBody] UserAddModel model)
		{
			return Request.CreateResponse(objBLUser.UpdateData(model));
		}

		/// <summary>
		/// Deletes user
		/// </summary>
		/// <param name="id">id of user to be delete</param>
		/// <returns>Appropriate Message</returns>
		[HttpDelete]
		[BearerAuthentication]
		[Authorize(Roles = "Admin")]
		[Route("api/CLUser/DeleteUser")]
		public HttpResponseMessage DeleteUser(int id)
		{
			return Request.CreateResponse(objBLUser.DeleteData(id));
		}

	}

}
