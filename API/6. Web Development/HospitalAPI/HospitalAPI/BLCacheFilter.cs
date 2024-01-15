using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Filters;

namespace HospitalAPI
{
    /// <summary>
    /// Defines custom cache filter
    /// </summary>
    public class BLCacheFilter : ActionFilterAttribute
    {
        #region Public Members

        /// <summary>
        /// TimeDuration is used to specify maximum timespan of cache
        /// </summary>
        public int TimeDuration { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Defines header parameters for caching
        /// </summary>
        /// <param name="actionExecutedContext">Represents the action of HTTP executed context</param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue
            {
                // Specifies activation duration of caching in seconds
                MaxAge = TimeSpan.FromSeconds(TimeDuration),

                // Indicates that cache can be reused while fresh and if stale, it must be validated with the origin server before reuse.
                MustRevalidate = true,

                // Specifies weather cache is public cache or not
                Public = true
            };
        }

        #endregion
    }
}