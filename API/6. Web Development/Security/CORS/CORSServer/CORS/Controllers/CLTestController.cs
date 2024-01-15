using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebService.Controllers
{
    // To enable croos origin server request 
    [EnableCors(origins: "https://www.google.com", headers: "*", methods: "*")]

    /// <summary>
    /// Custom controller crated for CORS
    /// </summary>
    public class CLTestController : ApiController
    {
        #region Public Methods

        /// <summary>
        /// Handles get request
        /// </summary>
        /// <returns>Sample string message using HttpResponseMessage</returns>
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent("GET: Test message from web services")
            };
        }

        /// <summary>
        /// Handles post request
        /// </summary>
        /// <returns>Sample string message using HttpResponseMessage</returns>
        public HttpResponseMessage Post()
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent("POST: Test message")
            };
        }

        /// <summary>
        /// Handles put request
        /// </summary>
        /// <returns>Sample string message using HttpResponseMessage</returns>
        public HttpResponseMessage Put()
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent("PUT: Test message")
            };
        }

        #endregion
    }
}