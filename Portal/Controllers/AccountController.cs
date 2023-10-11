using Microsoft.AspNetCore.Mvc;
using Portal.Models;

namespace Portal.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                return View("HomeScreen");
            }
            else { return View(); }
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
