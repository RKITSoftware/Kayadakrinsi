using Microsoft.AspNetCore.Mvc;
using Temp.Data;
using Temp.Models;

namespace Temp.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categories = _context.Category;
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
