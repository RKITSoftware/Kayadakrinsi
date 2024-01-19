using System.Web.Http;
using Authentication_Authorization.BasicAuth;
using Authentication_Authorization.BusinessLogic;

namespace Authentication_Authorization.Controllers
{
    //[Authorize]
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

        //[Route("PostCompany")]
        //[BasicAuthorizationAttribute(Roles = "Admin")]
        //public HttpResponseMessage PostCompany(int id,Company newCompany){
        //    var company = companies.Find(x => x.Id == id);
        //    try
        //    {
        //        if (company == null)
        //        {
        //            companies.Add(newCompany);
        //            return Request.CreateResponse(HttpStatusCode.OK, companies.Where(c => c.Id <= 3));
        //        }
        //        else
        //        {
        //            return Request.CreateResponse(HttpStatusCode.BadRequest, "Company with given id is already exist!");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.Ambiguous, ex.Message);
        //    }
        //}

        //[Route("PutCompany")]
        //[BasicAuthorizationAttribute(Roles = "Admin")]
        //public HttpResponseMessage PutCompany(int id,Company editedCompany)
        //{
        //    var company = companies.Find(x => x.Id == id);
        //    try
        //    {
        //        if (company != null)
        //        {
        //            companies[id]=editedCompany;
        //            return Request.CreateResponse(HttpStatusCode.OK, companies);
        //        }
        //        else
        //        {
        //            return Request.CreateResponse(HttpStatusCode.NotFound, "Company with given id not found!");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.Ambiguous, ex.Message);
        //    }
        //}

        //[Route("DeleteCompany")]
        //[BasicAuthorizationAttribute(Roles = "Admin")]
        //public HttpResponseMessage DeleteCompany(int id)
        //{
        //    var company = companies.Find(x => x.Id == id);
        //    try
        //    {
        //        if (company != null)
        //        {
        //            companies.RemoveAt(id);
        //            return Request.CreateResponse(HttpStatusCode.OK, companies);
        //        }
        //        else
        //        {
        //            return Request.CreateResponse(HttpStatusCode.NotFound, "Company with given id not found!");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.Ambiguous, ex.Message);
        //    }
        //}

        #endregion
    }
}
