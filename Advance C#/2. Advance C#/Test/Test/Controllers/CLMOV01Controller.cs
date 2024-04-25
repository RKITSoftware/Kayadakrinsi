using System.Web.Http;
using Test.BusinessLogic;
using Test.Models;

namespace Test.Controllers
{
    /// <summary>
    /// Controller for handling MOV01 operations via HTTP requests.
    /// </summary>
    [RoutePrefix("api/CLMOV01Controller")]
    public class CLMOV01Controller : ApiController
    {
        /// <summary>
        /// Business Logic object for MOV01 operations.
        /// </summary>
        public BLMOV01 objBLMOV01;

        /// <summary>
        /// Constructor initializing BLMOV01 object.
        /// </summary>
        public CLMOV01Controller()
        {
            objBLMOV01 = new BLMOV01();
        }

        /// <summary>
        /// Retrieves MOV01 object by ID.
        /// </summary>
        /// <param name="id">The ID of the MOV01 object to retrieve.</param>
        /// <returns>HTTP response containing the retrieved MOV01 object.</returns>
        [HttpGet]
        [Route("GetById")]
        public IHttpActionResult GetById(int id)
        {
            RES01 response = objBLMOV01.GetById(id);

            return Ok(response);
        }

        /// <summary>
        /// Retrieves all MOV01 objects.
        /// </summary>
        /// <returns>HTTP response containing all MOV01 objects.</returns>
        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            RES01 response = objBLMOV01.GetAll();

            return Ok(response);
        }

        /// <summary>
        /// Inserts a new MOV01 object.
        /// </summary>
        /// <param name="objDTOMOV01">The DTOMOV01 object containing data for the new MOV01 object.</param>
        /// <returns>HTTP response indicating the result of the insertion operation.</returns>
        [HttpPost]
        [Route("InsertMovie")]
        public IHttpActionResult InsertMovie(DTOMOV01 objDTOMOV01)
        {
            objBLMOV01.objOperation = Models.Enums.enmOperations.I;

            objBLMOV01.PreSave(objDTOMOV01);

            RES01 response = objBLMOV01.Validation();

            if (!response.isError)
            {
                response = objBLMOV01.Save();
            }

            return Ok(response);
        }

        /// <summary>
        /// Updates an existing MOV01 object.
        /// </summary>
        /// <param name="objDTOMOV01">The DTOMOV01 object containing updated data for the MOV01 object.</param>
        /// <returns>HTTP response indicating the result of the update operation.</returns>
        [HttpPut]
        [Route("UpdateMovie")]
        public IHttpActionResult UpdateMovie(DTOMOV01 objDTOMOV01)
        {
            objBLMOV01.objOperation = Models.Enums.enmOperations.U;

            objBLMOV01.PreSave(objDTOMOV01);

            RES01 response = objBLMOV01.Validation();

            if (!response.isError)
            {
                response = objBLMOV01.Save();
            }
            
            return Ok(response);
        }

        /// <summary>
        /// Deletes a MOV01 object by ID.
        /// </summary>
        /// <param name="id">The ID of the MOV01 object to delete.</param>
        /// <returns>HTTP response indicating the result of the deletion operation.</returns>
        [HttpDelete]
        [Route("DeleteMovie")]
        public IHttpActionResult DeleteMovie(int id)
        {

            RES01 response = objBLMOV01.ValidationDelete(id);

            if (!response.isError)
            {
                response = objBLMOV01.Delete(id);
            }
            
            return Ok(response);
        }
    }
}
