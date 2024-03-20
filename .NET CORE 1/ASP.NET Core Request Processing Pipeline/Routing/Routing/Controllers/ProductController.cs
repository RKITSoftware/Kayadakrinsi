using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Routing.BusinessLogic;

namespace Routing.Controllers
{
    public class ProductController : Controller
    {
        public static int idCount = 0;

        [HttpGet]
        public IActionResult Details(int id)
        {
            var product = BLProduct.lstProducts.FirstOrDefault(p => p.R01F01 == id);
            ViewBag.Message = product;

            return View();
        }

        public IActionResult AddProduct(string name)
        {
            var product = BLProduct.lstProducts.FirstOrDefault(p => p.R01F02 == name);
            if (product == null)
            {
                idCount++;
                BLProduct.lstProducts.Add(new Models.PRO01 { R01F01 = idCount, R01F02 = name });
                ViewBag.Message = BLProduct.lstProducts;
                return View("Details");
            }
            else
            {
                ViewBag.Message = BLProduct.lstProducts;
                return View("Details");
            }
        }
    }
}
