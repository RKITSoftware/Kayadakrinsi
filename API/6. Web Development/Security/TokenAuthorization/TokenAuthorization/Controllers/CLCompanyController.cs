using System.Net;
using System.Net.Http;
using System.Web.Http;
using TokenAuthorization.BusinessLogic;

namespace TokenAuthorization.Controllers
{
    /// <summary>
    /// Custom controller for user requests
    /// </summary>
    public class CLCompanyController : ApiController
    {
        #region Public Methods

        /// <summary>
        /// Handles get request of normal user
        /// </summary>
        /// <param name="id">Defines id of company user want to access</param>
        /// <returns>Company with given id</returns>
        [Route("api/CLCompany/GetCompanyById/{id}")]
        [Authorize(Roles = ("User"))] // Authorize user with user rights
        public HttpResponseMessage GetCompanyById(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, BLCompany.GetCompanyById(id));
        }

        /// <summary>
        /// Handles get request of admin, super admin
        /// </summary>
        /// <returns>List of companies with id less than five</returns>
        [Route("api/CLCompany/GetSomeCompany")]
        [Authorize(Roles = ("Admin,SuperAdmin"))]
        public HttpResponseMessage GetSomeCompany()
        {
            return Request.CreateResponse(HttpStatusCode.OK, BLCompany.GetSomeCompanies());
        }

        /// <summary>
        /// Handles get request of super admin
        /// </summary>
        /// <returns>List of all companies using HttpResponseMessage</returns>
        [Route("api/CLCompany/GetAllCompany")]
        [Authorize(Roles = ("SuperAdmin"))]
        public HttpResponseMessage GetAllCompany()
        {
            return Request.CreateResponse(HttpStatusCode.OK, BLCompany.lstCompanies);
        }

        #endregion
    }
}
