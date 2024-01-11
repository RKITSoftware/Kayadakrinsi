using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Country.Models;

namespace Country.Controllers
{
    public class CountryController : ApiController
    {
        public static List<CountryDetails> countries;
        static CountryController()
        {
                countries = new List<CountryDetails> {
                    new CountryDetails(1, "India", 91, 1428),
                    new CountryDetails(2, "Australia", 61, 25.77)
                };
        }

        [HttpGet]
        public IHttpActionResult GetCountries()
        {
            return Ok(countries);
        }
        public IHttpActionResult GetById(int id)
        {
            var country = countries.Find(x => x.Id == id);
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
        public IHttpActionResult AddCountry(int id, CountryDetails newCountry)
        {
            var country = countries.Find(x => x.Id == id);
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
        public IHttpActionResult EditCountry(int id, CountryDetails changedCountry)
        {
            var country = countries.Find(x => x.Id == id);
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
        public IHttpActionResult DeleteCountry(int id)
        {
            var country = countries.Find(x => x.Id == id);
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
    }
}