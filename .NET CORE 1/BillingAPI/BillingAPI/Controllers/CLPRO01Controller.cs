using BillingAPI.BusinessLogic;
using BillingAPI.Interfaces;
using BillingAPI.Models;
using BillingAPI.Models.POCO;
using Microsoft.AspNetCore.Mvc;

namespace BillingAPI.Controllers
{
    /// <summary>
    /// Handles http request for product operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLPRO01Controller : ControllerBase
    {
        /// <summary>
        /// Instance of BLPRO01 class
        /// </summary>
        public BLPRO01 objBLPRO01;

        /// <summary>
        /// Resolves dependencies and performs initialization
        /// </summary>
        /// <param name="objCRUDPRO01">Instance of ICRUDService<PRO01> interface</param>
        public CLPRO01Controller(ICRUDService<PRO01> objCRUDPRO01)
        {
            objBLPRO01 = new BLPRO01(objCRUDPRO01);

        }

        /// <summary>
        /// Retrives all products data
        /// </summary>
        /// <returns>Response containing all products data</returns>
        [HttpGet]
        [Route("GetProducts")]
        public IActionResult GetProducts()
        {
            Response response = new Response();

            response = objBLPRO01.objCRUDPRO01.Select();

            return Ok(response);
        }

        /// <summary>
        /// Adds product
        /// </summary>
        /// <param name="objDTOPRO01">Instance of DTOPRO01 class</param>
        /// <returns>Appropriate message</returns>
        [HttpPost]
        [Route("AddProduct")]
        public IActionResult AddProduct(DTOPRO01 objDTOPRO01)
        {
            objBLPRO01.objCRUDPRO01.Operations = Enums.enmOperations.I;

            objBLPRO01.PreSave(objDTOPRO01);

            Response response = objBLPRO01.Validation();

            if (!response.isError)
            {
                response = objBLPRO01.objCRUDPRO01.Save();
            }

            return Ok(response);
        }

        /// <summary>
        /// Updates product
        /// </summary>
        /// <param name="objDTOPRO01">Instance of DTOPRO01 class</param>
        /// <returns>Appropriate message</returns>
        [HttpPut]
        [Route("EditProduct")]
        public IActionResult EditProduct(DTOPRO01 objDTOPRO01)
        {
            objBLPRO01.objCRUDPRO01.Operations = Enums.enmOperations.U;

            objBLPRO01.PreSave(objDTOPRO01);

            Response response = objBLPRO01.Validation();

            if (!response.isError)
            {
                response = objBLPRO01.objCRUDPRO01.Save();
            }

            return Ok(response);
        }

        /// <summary>
        /// Deletes product whoose id is given
        /// </summary>
        /// <param name="id">Id of product to be delete</param>
        /// <returns>Appropriate message</returns>
        [HttpDelete]
        [Route("DeleteProduct")]
        public IActionResult DeleteProduct(int id)
        {
            Response response = new Response();

            response = objBLPRO01.ValidationDelete(id);

            if (!response.isError)
            {
                response = objBLPRO01.objCRUDPRO01.Delete(id);
            }

            return Ok(response);
        }
    }
}
