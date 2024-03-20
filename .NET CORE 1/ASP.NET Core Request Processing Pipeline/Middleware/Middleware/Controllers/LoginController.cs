using Microsoft.AspNetCore.Mvc;

namespace Middleware.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Here you would typically validate the username and password
            // against your database or any other authentication source.
            // For this example, let's just check if the username and password are correct.

            if (username == "admin" && password == "password")
            {
                // Authentication successful, redirect the user to a dashboard or home page.
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Authentication failed, return the user to the login page with an error message.
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View("Error");
            }
        }
    }
}
