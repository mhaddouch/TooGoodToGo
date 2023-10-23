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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("HomeScreen","Home");
            }
            else { return View(); }

        }



    }
}
