using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using System.Diagnostics;

namespace Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(StudentViewModel studentViewModel)
        {
           if(ModelState.IsValid)
            {
                return View("HomeScreen");
            }
            else { return View(); } 
            
        }

        public IActionResult HomeScreen()
        {
            return View();
        }


    }
}