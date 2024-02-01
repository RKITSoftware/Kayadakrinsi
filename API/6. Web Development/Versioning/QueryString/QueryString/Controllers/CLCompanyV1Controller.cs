using System.Net;
using System.Net.Http;
using System.Web.Http;
using QueryString.BusiessLogic;

namespace QueryString.Controllers
{
    /// <summary>
    /// Custom controller for handling request for CMP01
    /// </summary>
    public class CLCompanyV1Controller : ApiController
    {
        #region Public Methods

        /// <summary>
        /// Handles request of version one
        /// </summary>
        /// <returns>List of companies of type CMP01</returns>
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, BLCompany.lstCMP01);
        }

        #endregion
    }
}
