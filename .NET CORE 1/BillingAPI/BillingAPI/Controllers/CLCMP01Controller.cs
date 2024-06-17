using BillingAPI.BusinessLogic;
using BillingAPI.Filters;
using BillingAPI.Interfaces;
using BillingAPI.Models;
using BillingAPI.Models.POCO;
using Microsoft.AspNetCore.Mvc;

namespace BillingAPI.Controllers
{
    /// <summary>
    /// Handles http request for company operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLCMP01Controller : ControllerBase
    {
        /// <summary>
        /// Instance of BLCMP01 class
        /// </summary>
        public BLCMP01 objBLCMP01;

        /// <summary>
        /// Resolves dependencies and performs initializes
        /// </summary>
        /// <param name="objCRUD"></param>
        public CLCMP01Controller(ICRUDService<CMP01> objCRUD)
        {
            objBLCMP01 = new BLCMP01(objCRUD);
        }

        /// <summary>
        /// Retrives all companies data
        /// </summary>
        /// <returns>Response containing all companies data</returns>
        [HttpGet]
        [Route("GetCompanies")]
        [AuthorizationFilter("GU,AC,AD")]
        public IActionResult GetCompanies()
        {
            Response response = objBLCMP01.objCRUDCMP01.Select();

            return Ok(response);
        }

        /// <summary>
        /// Adds company
        /// </summary>
        /// <param name="objDTOCMP01">Instance of DTOCMP01 class</param>
        /// <returns>Appropriate message</returns>
        [HttpPost]
        //[ServiceFilter(typeof(ResourceFilter))]
        [ActionExecutedFilter]
        [Route("AddCompany")]
        [BillingAPI.Filters.AuthorizationFilter(roles: "AD,AC")]
        public IActionResult AddCompany(DTOCMP01 objDTOCMP01)
        {
            objBLCMP01.objCRUDCMP01.Operations = Enums.enmOperations.I;

            objBLCMP01.PreSave(objDTOCMP01);

            Response response = objBLCMP01.Validation();
            if (!response.isError)
            {
                response = objBLCMP01.objCRUDCMP01.Save();
            }

            return Ok(response);
        }

        /// <summary>
        /// Updates company
        /// </summary>
        /// <param name="objDTOCMP01">Instance of DTOCMP01 class</param>
        /// <returns>Appropriate message</returns>
        [HttpPut]
        //[ServiceFilter(typeof(ResourceFilter))]
        [ActionExecutedFilter]
        [Route("EditCompany")]
        [BillingAPI.Filters.AuthorizationFilter(roles: "AD,AC")]
        public IActionResult EditCompany(DTOCMP01 objDTOCMP01)
        {
            objBLCMP01.objCRUDCMP01.Operations = Enums.enmOperations.U;

            objBLCMP01.PreSave(objDTOCMP01);

            Response response = objBLCMP01.Validation();
            if (!response.isError)
            {
                response = objBLCMP01.objCRUDCMP01.Save();
            }

            return Ok(response);
        }
        
        /// <summary>
        /// Sets current company manually
        /// </summary>
        /// <param name="id">Id of company to be set</param>
        /// <returns>Appropriate message</returns>
        [HttpPut]
        [Route("SetCurrentCompany")]
        [BillingAPI.Filters.AuthorizationFilter(roles: "AD,AC")]
        public IActionResult SetCurrentCompany(int id)
        {
            Response response = objBLCMP01.ValidationDelete(id);
            if (!response.isError)
            {
                response = objBLCMP01.SetCurrentCompany(id);
            }
           
            return Ok(response);
        }

        /// <summary>
        /// Deletes company whoose id is given
        /// </summary>
        /// <param name="id">Id of company to be delete</param>
        /// <returns>Appropriate message</returns>
        [HttpDelete]
        [Route("DeleteCompany")]
        [BillingAPI.Filters.AuthorizationFilter(roles: "AD")]
        public IActionResult DeleteCompany(int id)
        {
            Response response = objBLCMP01.ValidationDelete(id);

            if (!response.isError)
            {
                response = objBLCMP01.objCRUDCMP01.Delete(id);
            }
            return Ok(response);
        }
    }
}
