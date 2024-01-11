using Authentication_Authorization.BasicAuth;
using Authentication_Authorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Authentication_Authorization.Controllers
{
    //[Authorize]
    [RoutePrefix("api/Company")]
    [BasicAuthenticationAttribute]
    public class CompanyController : ApiController
    {
        // Get few compnies
        [Route("GetFewCompanies")]
        [BasicAuthorizationAttribute(Roles="User")]
        public HttpResponseMessage GetFewCompanies()
        {
            return Request.CreateResponse(HttpStatusCode.OK,Company.GetCompnies().Where(c => c.Id <= 2));
        }

        // Get more compnies
        [Route("GetMoreCompanies")]
        [BasicAuthorizationAttribute(Roles = "Admin")]
        public HttpResponseMessage GetMoreCompanies()
        {
            return Request.CreateResponse(HttpStatusCode.OK, Company.GetCompnies().Where(c => c.Id <= 3));
        }

        // Get all compnies
        [Route("GetAllCompanies")]
        [BasicAuthorizationAttribute(Roles = "SuperAdmin")]
        public HttpResponseMessage GetAllCompanies()
        {
            return Request.CreateResponse(HttpStatusCode.OK, Company.GetCompnies());
        }
    }
}
