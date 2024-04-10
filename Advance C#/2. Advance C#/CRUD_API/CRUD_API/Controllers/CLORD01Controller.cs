using System.Web.Http;
using CRUD_API.BusinessLogic;
using CRUD_API.Models;

namespace CRUD_API.Controllers
{
    /// <summary>
    /// Handles HTTP requests for orders
    /// </summary>
    [RoutePrefix("api/CLORD01Controller")]
    public class CLORD01Controller : ApiController
    {
        private BLORD01 _objORD01 = new BLORD01();

        /// <summary>
        /// Retrives details of all orders
        /// </summary>
        /// <returns>Instance of RES01</returns>
        [HttpGet]
        [Route("ViewOrders")]
        public IHttpActionResult ViewOrders()
        {
            return Ok(_objORD01.Select());
        }

        /// <summary>
        /// Creates new order
        /// </summary>
        /// <param name="objORD01">Instance of class ORD01</param>
        /// <returns>Instance of RES01</returns>
        [HttpPost]
        [Route("PlaceOrder")]
        public IHttpActionResult PlaceOrder(DTOORD01 objORD01)
        {
            _objORD01.PreSave(objORD01);
            if (_objORD01.Validation().isError == false)
            {
                return Ok(_objORD01.Insert());
            }
            return Ok(_objORD01.Validation());
        }

        /// <summary>
        /// Updates order
        /// </summary>
        /// <param name="objORD01">Instance of class ORD01</param>
        /// <returns>Instance of RES01</returns>
        [HttpPut]
        [Route("EditOrder")]
        public IHttpActionResult EditOrder(int id, DTOORD01 objORD01)
        {
            _objORD01.PreSave(objORD01);
            if (_objORD01.Validation().isError == false)
            {
                return Ok(_objORD01.Update(id));
            }
            return Ok(_objORD01.Validation());
        }

        /// <summary>
        /// Deletes order whoose id is given
        /// </summary>
        /// <param name="id">Id of order to be delete</param>
        /// <returns>Instance of RES01</returns>
        [HttpDelete]
        [Route("CancleOrder")]
        public IHttpActionResult CancleOrder(int id)
        {
            return Ok(_objORD01.Delete(id));
        }

        /// <summary>
        /// Drops table ORD01
        /// </summary>
        /// <returns>Instance of RES01</returns>
        [HttpDelete]
        [Route("Drop")]
        public IHttpActionResult Drop()
        {
            return Ok(_objORD01.Drop());
        }
    }
}
