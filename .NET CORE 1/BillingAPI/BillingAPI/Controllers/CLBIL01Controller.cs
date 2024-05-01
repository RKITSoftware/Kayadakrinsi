using BillingAPI.BusinessLogic;
using BillingAPI.Filters;
using BillingAPI.Interfaces;
using BillingAPI.Models;
using BillingAPI.Models.POCO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace BillingAPI.Controllers
{
    /// <summary>
    /// Handles http requests for bill operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLBIL01Controller : ControllerBase
    {
        /// <summary>
        /// Instance of BLBIL01 class
        /// </summary>
        public BLBIL01 objBLBIL01;

        /// <summary>
        /// Resolves dependencies and performs initialization
        /// </summary>
        /// <param name="objCRUDBIL01">Instance of ICRUDService<BIL01> interface</param>
        /// <param name="objBIL01Service">Instance of IBIL01Service interface</param>
        public CLBIL01Controller(ICRUDService<BIL01> objCRUDBIL01, IBIL01Service objBIL01Service)
        {
            objBLBIL01 = new BLBIL01(objCRUDBIL01, objBIL01Service);
        }

        /// <summary>
        /// Retrives all bills
        /// </summary>
        /// <returns>Response containing all bills</returns>
        [HttpGet]
        [AuthorizationFilter("AD")]
        //[ServiceFilter(typeof(ResourceFilter))]
        [Route("GetBills")]
        public IActionResult GetBills()
        {
            Response response = new Response();

            response = objBLBIL01.objCRUDBIL01.Select();

            return Ok(response);
        }

        /// <summary>
        /// Retrives single bill with given id
        /// </summary>
        /// <param name="id">Id of bill to be retrive</param>
        /// <returns>Bill with given id</returns>
        [HttpGet]
        [AllowAnonymous]
        [AuthorizationFilter("AD,AC,GU")]
        [Route("GetBill")]
        public IActionResult GetBill(int id)
        {
            Response response = objBLBIL01.FinalBill(id);

            if (!response.isError)
            {
                string path = response.message;

                if (!System.IO.File.Exists(path))
                {
                    return NotFound("PDF file not found.");
                }

                // Read the PDF file into a byte array
                byte[] pdfBytes = System.IO.File.ReadAllBytes(path);

                // Return the file content as a FileStreamResult
                return File(pdfBytes, "application/pdf", Path.GetFileName(path));
            }
            else
            {
                return BadRequest("Invalid bill Id");
            }
        }

        /// <summary>
        /// Shares bill's pdf to WhatsApp
        /// </summary>
        /// <param name="id">Id of bill</param>
        /// <returns>Appropriate message with message's sid</returns>
        [HttpPost]
        [AuthorizationFilter("AD,AC,GU")]
        [Route("ShareBill")]
        public async Task<IActionResult> ShareBill(int id)
        {
            try
            {
                var accountSid = "Account_sid";
                var authToken = "Auth_Token";
                TwilioClient.Init(accountSid, authToken);

                Response response = objBLBIL01.FinalBill(id);

                // Media sharing using Twilio doesn't support local hosting
                var mediaUrl = new[] {
                new Uri($"https://127.0.0.1:7082/Bills/{Path.GetFileName(response.message)}")}.ToList();

                var message = MessageResource.Create(
                              //mediaUrl: mediaUrl,
                              body:"Hi there",
                              from: new Twilio.Types.PhoneNumber("whatsapp:+Twilio_number_withCountryCode"),
                              to: new Twilio.Types.PhoneNumber("whatsapp:+Recipient_number_withCountryCode")
                              );

                return Ok("Message sent successfully." + message.Sid);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to send message: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds bill
        /// </summary>
        /// <param name="objDTOBIL01">Instance of DTOBIL01 class</param>
        /// <returns>Appropriate message</returns>
        [HttpPost]
        [Route("AddBill")]
        public IActionResult AddBill(DTOBIL01 objDTOBIL01)
        {
            objBLBIL01.objCRUDBIL01.Operations = Enums.enmOperations.I;

            objBLBIL01.PreSave(objDTOBIL01);

            Response response = objBLBIL01.Validation();

            if (!response.isError)
            {
                response = objBLBIL01.objCRUDBIL01.Save();
            }

            return Ok(response);
        }

        /// <summary>
        /// Updates bill
        /// </summary>
        /// <param name="objDTOBIL01">Instance of DTOBIL01 class</param>
        /// <returns>Appropriate message</returns>
        [HttpPut]
        [Route("EditBill")]
        public IActionResult EditBill(DTOBIL01 objDTOBIL01)
        {
            objBLBIL01.objCRUDBIL01.Operations = Enums.enmOperations.U;

            objBLBIL01.PreSave(objDTOBIL01);

            Response response = objBLBIL01.Validation();

            if (!response.isError)
            {
                response = objBLBIL01.objCRUDBIL01.Save();
            }

            return Ok(response);
        }

        /// <summary>
        /// Deletes bill with given id if exist
        /// </summary>
        /// <param name="id">Id of bill to be delete</param>
        /// <returns>Appropriate message</returns>
        [HttpDelete]
        [Route("DeleteBill")]
        public IActionResult DeleteBill(int id)
        {
            Response response = objBLBIL01.ValidationDelete(id);

            if (!response.isError)
            {
                response = objBLBIL01.objCRUDBIL01.Delete(id);
            }

            return Ok(response);
        }

    }
}
