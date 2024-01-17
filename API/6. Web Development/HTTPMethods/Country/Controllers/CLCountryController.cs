using System.Web.Http;
using Country.BusinessLogic;
using Country.Models;

namespace Country.Controllers
{
    /// <summary>
    /// Custom controller to handle http requests
    /// </summary>
    public class CLCountryController : ApiController
    {

        #region Public Methods

        /// <summary>
        /// Handles get request 
        /// </summary>
        /// <returns>list of countries</returns>
        [HttpGet]
        [Route("api/CLCountry/GetALlCountries")]
        public IHttpActionResult GetAllCountries()
        {
            var lstCON01 = BLCountry.GetCountries();
            return Ok(lstCON01);
        }

        /// <summary>
        /// Handles get request for given id
        /// </summary>
        /// <param name="id">For searching particular country with given id</param>
        /// <returns>returns country with given id if exist</returns>
        [Route("api/CLCountry/GetCountryById")]
        public IHttpActionResult GetCountryById(int id)
        {
            var objCON01 = BLCountry.GetById(id);
            if(objCON01 == null)
            {
                return BadRequest();
            }
            return Ok(objCON01);
        }

        /// <summary>
        /// Handles post request
        /// </summary>
        /// <param name="objCON01">new country which is object of type Country</param>
        /// <returns>List of counties if request is ok</returns>
        [HttpPost]
        [Route("api/CLCountry/AddCountry")]
        public IHttpActionResult AddCountry(CON01 objCON01)
        {
            var lstCON01 = BLCountry.AddCountry(objCON01);
            if (lstCON01 == null)
            {
                return BadRequest();
            }
            return Ok(lstCON01);
        }

        /// <summary>
        /// Handles put request
        /// </summary>
        /// <param name="objCON01">edited country which is also object of class Country</param>
        /// <returns>List of counties if request is ok</returns>
        [HttpPut]
        [Route("api/CLCountry/EditCountry")]
        public IHttpActionResult EditCountry(CON01 objCON01)
        {
            var lstCON01 = BLCountry.EditCountry(objCON01);
            if (lstCON01 == null)
            {
                return BadRequest();
            }
            return Ok(lstCON01);
        }

        /// <summary>
        /// Handles delete request
        /// </summary>
        /// <param name="id">id of element to be delete</param>
        /// <returns>List of counties if request is ok</returns>
        [HttpDelete]
        [Route("api/CLCountry/DeleteCountry")]
        public IHttpActionResult DeleteCountry(int id)
        {
            var lstCON01 = BLCountry.DeleteCountry(id);
            if (lstCON01 == null)
            {
                return BadRequest();
            }
            return Ok(lstCON01);
        }

        #endregion
    }
}