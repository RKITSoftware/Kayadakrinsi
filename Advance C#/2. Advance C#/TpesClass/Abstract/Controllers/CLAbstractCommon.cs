using System.Net.Http;
using System.Web.Http;

namespace Abstract.Controllers
{
    /// <summary>
    /// Abstract controller class 
    /// </summary>
    public abstract class CLAbstractCommon : ApiController
    {
        #region Abstract Methods

        /// <summary>
        /// Abstract method get
        /// </summary>
        /// <returns>HttpResponseMessage</returns>
        public abstract HttpResponseMessage Get();

        #endregion
    }
}