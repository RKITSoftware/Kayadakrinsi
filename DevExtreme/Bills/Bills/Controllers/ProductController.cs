using Bills.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bills.Controllers
{
    [Route("api/[controller]/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // GET: api/products
        [HttpGet]
        public IActionResult GetProducts()
        {
            var list = Data.Data.productList;

            return Ok(list);
        }

        // GET: api/products/1
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = Data.Data.productList.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public IActionResult CreateProduct([FromBody] Products product)
        {
            int nextId = Data.Data.productList.Max(p=>p.ProductId)+1;
            product.ProductId = nextId;

            Data.Data.productList.Add(product);

            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
        }

        // PUT: api/products/1
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Products product)
        {
            var existingProduct = Data.Data.productList.FirstOrDefault(p => p.ProductId == id); // Updated reference
            if (existingProduct == null)
            {
                return NotFound();
            }

            // Update only the non-null fields from the incoming product object
            existingProduct.Name = !string.IsNullOrEmpty(product.Name) ? product.Name : existingProduct.Name;
            existingProduct.Price = product.Price != default ? product.Price : existingProduct.Price;

            return NoContent();
        }


        // DELETE: api/products/1
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = Data.Data.productList.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            Data.Data.productList.Remove(product);
            return NoContent();
        }
    }
}
