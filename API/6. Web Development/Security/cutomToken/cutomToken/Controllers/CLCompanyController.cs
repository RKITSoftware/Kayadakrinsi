using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cutomToken.Auth;
using cutomToken.BusinessLogic;

namespace cutomToken.Controllers
{
    /// <summary>
    /// Custom controller for user requests
    /// </summary>
    public class CLCompanyController : ApiController
    {
        public BLCompany objBLCompany;

        public CLCompanyController()
        {
            objBLCompany = new BLCompany();
        }

        #region Public Methods

        [HttpPost]
        [Route("api/CLCompany/token")]
        [BasicAuthentication]
        public IHttpActionResult GetToken()
        {
            string authToken = Request.Headers.Authorization.Parameter;

            string[] usernamepassword = authToken.Split(':');
            string username = usernamepassword[0];
            string password = usernamepassword[1];
            var userDetails = BLUser.lstUser.FirstOrDefault(u => u.R01F02 == username && u.R01F03 == password);
            if (userDetails != null)
            {
                return Ok(BLTokenManager.GenerateToken(username));
            }
            return BadRequest("Enter valid user details");
        }

        /// <summary>
        /// Handles get request of normal user
        /// </summary>
        /// <param name="id">Defines id of company user want to access</param>
        /// <returns>Company with given id</returns>
        [HttpGet]
        [Route("api/CLCompany/GetCompanyById/{id}")]
        [BearerAuthentication]
        [Authorize(Roles = ("User"))] // Authorize user with user rights
        public HttpResponseMessage GetCompanyById(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, objBLCompany.GetCompanyById(id));
        }
        

        /// <summary>
        /// Handles get request of admin, super admin
        /// </summary>
        /// <returns>List of companies with id less than five</returns>
        [HttpGet]
        [Route("api/CLCompany/GetSomeCompany")]
        [BearerAuthentication]
        [Authorize(Roles = ("Admin"))]
        public HttpResponseMessage GetSomeCompany()
        {
            return Request.CreateResponse(HttpStatusCode.OK, objBLCompany.GetSomeCompanies());
        }


        /// <summary>
        /// Handles get request of super admin
        /// </summary>
        /// <returns>List of all companies using HttpResponseMessage</returns>
        [HttpGet]
        [Route("api/CLCompany/GetAllCompany")]
        [BearerAuthentication]
        [Authorize(Roles = ("admin"))]
        public HttpResponseMessage GetAllCompany()
        {
            return Request.CreateResponse(HttpStatusCode.OK, BLCompany.lstCompanies);
        }

        #endregion
    }
}