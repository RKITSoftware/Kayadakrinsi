using System.Net;
using System.Net.Http;
using System.Web.Http;
using QueryString.BusiessLogic;

namespace QueryString.Controllers
{
    /// <summary>
    /// Custom controller for handling request for CMP01
    /// </summary>
    public class CLCompanyV2Controller : ApiController
    {
        #region Public Methods

        /// <summary>
        /// Handles request for version two
        /// </summary>
        /// <returns>List of companies of type CMP02</returns>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, BLCompany.lstCMP02);
        }

        #endregion
    }
}
