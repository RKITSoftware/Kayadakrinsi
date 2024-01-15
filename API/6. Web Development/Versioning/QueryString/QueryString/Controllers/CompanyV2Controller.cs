using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QueryString.Models;

namespace QueryString.Controllers
{
    public class CompanyV2Controller : ApiController
    {
        public List<CompanyV2> companies = CompanyV2.GetCompanies();
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, companies);
        }
    }
}
