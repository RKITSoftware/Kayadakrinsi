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
    public class CLCompanyV1Controller : CLAbstractCommon
    {
        #region Public Members

        /// <summary>
        /// List of companies of type CMP01
        /// </summary>
        public List<CMP01> companies = CMP01.GetCompanies();

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles request of version one
        /// </summary>
        /// <returns>List of companies of type CMP01</returns>
        public override HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, companies);
        }

        #endregion
    }
}
