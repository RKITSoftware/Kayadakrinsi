﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QueryString.Models;

namespace QueryString.Controllers
{
    /// <summary>
    /// Custom controller for handling request for CMP01
    /// </summary>
    public class CLCompanyV2Controller : ApiController
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
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, companies);
        }

        #endregion
    }
}