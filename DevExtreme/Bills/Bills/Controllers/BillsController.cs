using Bills.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bills.Controllers
{
    [Route("api/[controller]/bill")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        // GET: api/bill
        [HttpGet]
        public IActionResult GetBills()
        {
            var list = Data.Data.billList;

            return Ok(list);
        }

        // GET: api/bill/1
        [HttpGet("{id}")]
        public IActionResult GetBillById(int id)
        {
            var bill = Data.Data.billList.FirstOrDefault(b => b.Id == id); // Updated reference
            if (bill == null)
            {
                return NotFound();
            }
            return Ok(bill);
        }

        // POST: api/bill
        [HttpPost]
        public IActionResult CreateBill([FromBody] Bill bill)
        {

            int nextId = Data.Data.billList.Max(b => b.Id) + 1;
            bill.Id= nextId;

            Data.Data.billList.Add(bill); // Updated reference

            return CreatedAtAction(nameof(GetBillById), new { id = bill.Id }, bill);
        }

        // PUT: api/bill/1
        // PUT: api/bill/1
        [HttpPut("{id}")]
        public IActionResult UpdateBill(int id, [FromBody] Bill bill)
        {
            var existingBill = Data.Data.billList.FirstOrDefault(b => b.Id == id); // Updated reference
            if (existingBill == null)
            {
                return NotFound();
            }

            // Update only the non-null fields from the incoming bill object
            existingBill.Date = bill.Date != default ? bill.Date : existingBill.Date;
            existingBill.SellerId = bill.SellerId != default ? bill.SellerId : existingBill.SellerId;
            existingBill.PurchaserId = bill.PurchaserId != default ? bill.PurchaserId : existingBill.PurchaserId;
            existingBill.Products = bill.Products != null && bill.Products.Any() ? bill.Products : existingBill.Products;
            existingBill.TotalAmount = bill.TotalAmount != default ? bill.TotalAmount : existingBill.TotalAmount;

            return Ok();
        }


        // DELETE: api/bill/1
        [HttpDelete("{id}")]
        public IActionResult DeleteBill(int id)
        {
            var bill = Data.Data.billList.FirstOrDefault(b => b.Id == id); // Updated reference
            if (bill == null)
            {
                return NotFound();
            }

            Data.Data.billList.Remove(bill); // Updated reference
            return NoContent();
        }
    }
}
