using System.Net.Http;
using System.Web.Http;
using MusicCompany.BasicAuth;
using MusicCompany.BusinessLogic;
using MusicCompany.Models;

namespace MusicCompany.Controllers
{
    /// <summary>
    /// Handles methods to predorm operations related to producer
    /// </summary>
    [BasicAuthenticationAttribute]
    public class CLProducerController : ApiController
    {
        /// <summary>
        /// Declares object of BLProducer class
        /// </summary>
        public BLProducer objBLProducer;

        /// <summary>
        /// Initializes object of BLProducer class
        /// </summary>
        public CLProducerController()
        {
            objBLProducer = new BLProducer();
        }

        /// <summary>
        /// Handles get reuest for getting producer data
        /// </summary>
        /// <returns>All data from table PRO01</returns>
        [HttpGet]
        [BasicAuthorizationAttribute(Roles = "Producer,Admin")]
        [Route("api/CLProducer/GetProducer")]
        public IHttpActionResult GetProducer()
        {
            return Ok(objBLProducer.Select());
        }

        /// <summary>
        /// Downloads file of producer data
        /// </summary>
        /// <returns>Downloaded text file</returns>
        [HttpGet]
        [BasicAuthorizationAttribute(Roles = "Producer,Admin")]
        [Route("api/CLProducer/GetProducerFile")]
        public HttpResponseMessage GetProducerFile()
        {
            return objBLProducer.Download();
        }

        /// <summary>
        /// Adds producer
        /// </summary>
        /// <param name="objPRO01">object of PRO01 class to be added</param>
        /// <returns>Appropriate Message</returns>
        [HttpPost]
        [BasicAuthorizationAttribute(Roles = "Producer,Admin")]
        [Route("api/CLProducer/AddProducer")]
        public IHttpActionResult AddProducer(PRO01 objPRO01)
        {
            return Ok(objBLProducer.Insert(objPRO01));
        }

        /// <summary>
        /// Write data into file
        /// </summary>
        [HttpPost]
        [BasicAuthorizationAttribute(Roles = "Producer,Admin")]
        [Route("api/CLProducer/WriteFile")]
        public IHttpActionResult WriteFile()
        {
            return Ok(objBLProducer.WriteData());
        }

        /// <summary>
        /// Eedits producer
        /// </summary>
        /// <param name="objPRO01">object of PRO01 class to be edit</param>
        /// <returns>Appropriate Message</returns>
        [HttpPut]
        [BasicAuthorizationAttribute(Roles = "Producer,Admin")]
        [Route("api/CLProducer/EditProducer")]
        public IHttpActionResult EditProducer(PRO01 objPRO01)
        {
            return Ok(objBLProducer.Update(objPRO01));
        }

        /// <summary>
        /// Deletes producer
        /// </summary>
        /// <param name="id">id of producer to be deleted</param>
        /// <returns>Appropriate Message</returns>
        [HttpDelete]
        [BasicAuthorizationAttribute(Roles = "Admin")]
        [Route("api/CLProducer/DeleteProducer")]
        public IHttpActionResult DeleteProducer(int id)
        {
            return Ok(objBLProducer.Delete(id));
        }
    }
}