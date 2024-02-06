using System.Web.Http;
using Authentication_Authorization.BasicAuth;
using Authentication_Authorization.BusinessLogic;

namespace Authentication_Authorization.Controllers
{
    [RoutePrefix("api/Company")]
    [BasicAuthenticationAttribute]

    /// <summary>
    /// Custom cntroller for handling requests
    /// </summary>
    public class CLCompanyController : ApiController
    {

        #region Public Methods

        [Route("GetFewCompanies")]
        [BasicAuthorizationAttribute(Roles = "User")] // Custom filter attribute for authorization with role of user
        /// <summary>
        /// Handles get request of normal user
        /// </summary>
        /// <returns>List of compaines which can be access by normal user</returns>
        public IHttpActionResult GetFewCompanies()
        {
            return Ok(BLCompany.GetFewCompanies());
        }

        [Route("GetMoreCompanies")]
        [BasicAuthorizationAttribute(Roles = "Admin")]
        /// <summary>
        /// Handles get request of admin
        /// </summary>
        /// <returns>List of compaines which can be access by admin</returns>
        public IHttpActionResult GetMoreCompanies()
        {
            return Ok(BLCompany.GetMoreCompanies());
        }

        [Route("GetAllCompanies")]
        [BasicAuthorizationAttribute(Roles = "SuperAdmin")]
        /// <summary>
        /// Handles get request of super admin
        /// </summary>
        /// <returns>List of compaines which can be access by super admin</returns>
        public IHttpActionResult GetAllCompanies()
        {
            return Ok(BLCompany.lstCompanies);
        }

        #endregion
    }
}
