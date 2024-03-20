using Microsoft.AspNetCore.Mvc;
using RoutingAPI.BusinessLogic;
using RoutingAPI.Model;

namespace RoutingAPI.Controllers
{
    /// <summary>
    /// Product controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLProductController : ControllerBase
    {
        /// <summary>
        /// Declares object of class BLProduct
        /// </summary>
        public BLProduct objBLProduct;

        /// <summary>
        /// Initializes objects
        /// </summary>
        public CLProductController()
        {
            objBLProduct = new BLProduct();
        }

        /// <summary>
        /// Gets all products
        /// </summary>
        /// <returns>List of products</returns>
        [HttpGet]
        [Route("GetProducts")]
        public IActionResult GetProducts()
        {
            return Ok(BLProduct.lstPRO01);
        }

        /// <summary>
        /// Gets single product by id
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Product if id is valid, message otherwise</returns>
        [HttpGet]
        [Route("GetProductById/{id:int}")]
        public IActionResult GetProductById(int id)
        {
            PRO01 objPRO01 = objBLProduct.GetProductById(id);
            if (objPRO01 != null)
            {
                return Ok(objPRO01);
            }
            return BadRequest("Not found");
        }

        /// <summary>
        /// Adds product to list
        /// </summary>
        /// <param name="objPRO01">Object of class PRO01</param>
        /// <returns>Appropriate message</returns>
        [HttpPost]
        [Route("AddProduct")]
        public IActionResult AddProduct(PRO01 objPRO01)
        {
            if (objBLProduct.Validation(objPRO01))
            {
                return Ok(objBLProduct.AddProduct(objPRO01));
            }
            else
            {
                return BadRequest("Invalid data");
            }
        }

        /// <summary>
        /// Updates product in to the list
        /// </summary>
        /// <param name="objPRO01">Object of class PRO01</param>
        /// <returns>Appropriate message</returns>
        [HttpPut]
        [Route("EditProduct")]
        public IActionResult EditProduct(PRO01 objPRO01)
        {
            if (objBLProduct.validationUpdate(objPRO01))
            {
                return Ok(objBLProduct.EditProduct(objPRO01));
            }
            else
            {
                return BadRequest("Invalid data");
            }
        }

        /// <summary>
        /// Deletes product from list
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Appropriate message</returns>
        [HttpDelete]
        [Route("DeleteProduct")]
        public IActionResult DeleteProduct(int id) 
        {
            return Ok(objBLProduct.DeleteProduct(id));
        }
    }
}
