using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Country.Models;

namespace Country.Controllers
{
    /// <summary>
    /// Custom controller to handle http requests
    /// </summary>
    public class CLCountryController : ApiController
    {
        #region Public Members

        /// <summary>
        /// Maintains list of countries
        /// </summary>
        public static List<CON01> countries;

        #endregion

        #region Constructors 

        /// <summary>
        /// Initializes list of countries statically
        /// </summary>
        static CLCountryController()
        {
                countries = new List<CON01> {
                    new CON01(1, "India", 91, 1428),
                    new CON01(2, "Australia", 61, 25.77)
                };
        }

        #endregion

        #region Public Methods

        [HttpGet]
        /// <summary>
        /// Handles get request 
        /// </summary>
        /// <returns>all countries with ok response</returns>
        public IHttpActionResult GetCountries()
        {
            return Ok(countries);
        }

        /// <summary>
        /// Handles get request for given id
        /// </summary>
        /// <param name="id">For searching particular country with given id</param>
        /// <returns>returns country with given id if exist</returns>
        public IHttpActionResult GetById(int id)
        {
            var country = countries.Find(x => x.N01F01 == id);
            try
            {
                if (country == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(country);
        }

        [HttpPost]
        /// <summary>
        /// Handles post request
        /// </summary>
        /// <param name="id">id of new element</param>
        /// <param name="newCountry">new country which is object of type Country</param>
        /// <returns>List of counties if request is ok</returns>
        public IHttpActionResult AddCountry(int id, CON01 newCountry)
        {
            var country = countries.Find(x => x.N01F01 == id);
            try
            {
                if (country == null)
                {
                    countries.Add(newCountry);
                    return Ok(countries);
                }
                else
                {
                    return Ok("Id is already exist ; try again with different id");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        /// <summary>
        /// Handles put request
        /// </summary>
        /// <param name="id">id of country which is being edited</param>
        /// <param name="changedCountry">edited country which is also object of class Country</param>
        /// <returns>List of counties if request is ok</returns>
        public IHttpActionResult EditCountry(int id, CON01 changedCountry)
        {
            var country = countries.Find(x => x.N01F01 == id);
            try
            {
                if (country == null)
                {
                    return NotFound();
                }
                else
                {
                    country = changedCountry;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(countries);
        }

        [HttpDelete]
        /// <summary>
        /// Handles delete request
        /// </summary>
        /// <param name="id">id of element to be delete</param>
        /// <returns>List of counties if request is ok</returns>
        public IHttpActionResult DeleteCountry(int id)
        {
            var country = countries.Find(x => x.N01F01 == id);
            try
            {
                if (country == null)
                {
                    return NotFound();
                }
                else
                {
                    countries.Remove(country);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(countries);
        }

        #endregion
    }
}