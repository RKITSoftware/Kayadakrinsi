using Bills.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Bills.Controllers
{
    [Route("api/[controller]/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        // GET: api/company
        [HttpGet]
        public IActionResult GetCompanies()
        {
            var list = Data.Data.companyList; // Updated referencecd

            return Ok(list);
        }

        // GET: api/company/1
        [HttpGet("{id}")]
        public IActionResult GetCompanyById(int id)
        {
            var company = Data.Data.companyList.FirstOrDefault(c => c.CompanyId == id); // Updated reference
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        // POST: api/company
        [HttpPost]
        public IActionResult CreateCompany([FromBody] Company company)
        {
            if (company == null || string.IsNullOrEmpty(company.Name) || string.IsNullOrEmpty(company.GSTNumber))
            {
                return BadRequest("Company name and GST number are required.");
            }

            int nextId = Data.Data.companyList.Max(c => c.CompanyId) + 1;
            company.CompanyId = nextId;

            Data.Data.companyList.Add(company); // Updated reference

            return CreatedAtAction(nameof(GetCompanyById), new { id = company.CompanyId }, company);
        }

        // PUT: api/company/1
        [HttpPut("{id}")]
        public IActionResult UpdateCompany(int id, [FromBody] Company company)
        {
            if (company == null)
            {
                return BadRequest("Company object is required.");
            }

            var existingCompany = Data.Data.companyList.FirstOrDefault(c => c.CompanyId == id); // Updated reference
            if (existingCompany == null)
            {
                return NotFound();
            }

            // Update only the non-null fields from the incoming company object
            existingCompany.Name = !string.IsNullOrEmpty(company.Name) ? company.Name : existingCompany.Name;
            existingCompany.GSTNumber = !string.IsNullOrEmpty(company.GSTNumber) ? company.GSTNumber : existingCompany.GSTNumber;

            return NoContent();
        }

        // DELETE: api/company/1
        [HttpDelete("{id}")]
        public IActionResult DeleteCompany(int id)
        {
            var company = Data.Data.companyList.FirstOrDefault(c => c.CompanyId == id); // Updated reference
            if (company == null)
            {
                return NotFound();
            }

            Data.Data.companyList.Remove(company); // Updated reference
            return NoContent();
        }
    }
}
