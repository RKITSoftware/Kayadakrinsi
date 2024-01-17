using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Abstract.Models;

namespace Abstract.Controllers
{
    /// <summary>
    /// Custom controller for handling request for CMP01
    /// </summary>
    public class CLCompanyV2Controller : CLAbstractCommon
    {
        #region Public Members

        /// <summary>
        /// List of companies of type CMP02
        /// </summary>
        public List<CMP02> companies = CMP02.GetCompanies();

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles request for version two
        /// </summary>
        /// <returns>List of companies of type CMP02</returns>
        public override HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, companies);
        }

        #endregion
    }
}
