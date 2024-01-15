using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using URI.Models;

namespace URI.Controllers
{
    public class CompanyV1Controller : ApiController
    {
        public List<CompanyV1> companies = CompanyV1.GetCompanies();
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, companies);
        }
    }
}