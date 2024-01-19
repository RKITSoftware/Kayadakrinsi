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

        [Authorize(Roles = ("User"))] // Authorize user with user rights
        [Route("api/Company/GetCompanyById")]
        /// <summary>
        /// Handles get request of normal user
        /// </summary>
        /// <param name="id">Defines id of company user want to access</param>
        /// <returns>Company with given id</returns>
        public HttpResponseMessage GetCompanyById(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, BLCompany.GetCompanyById(id));
        }

        [Authorize(Roles = ("Admin,SuperAdmin"))]
        [Route("api/Company/GetSomeCompany")]
        /// <summary>
        /// Handles get request of admin, super admin
        /// </summary>
        /// <returns>List of companies with id less than five</returns>
        public HttpResponseMessage GetSomeCompany()
        {
            return Request.CreateResponse(HttpStatusCode.OK, BLCompany.GetSomeCompanies());
        }

        [Authorize(Roles = ("SuperAdmin"))]
        [Route("api/Company/GetAllCompany")]
        /// <summary>
        /// Handles get request of super admin
        /// </summary>
        /// <returns>List of all companies using HttpResponseMessage</returns>
        public HttpResponseMessage GetAllCompany()
        {
            return Request.CreateResponse(HttpStatusCode.OK, BLCompany.lstCompanies);
        }

        #endregion
    }
}
