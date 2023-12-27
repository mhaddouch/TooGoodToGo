using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Core.DomainServices;
using Core.Domain;

namespace Portal.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IStudentRepository _studentRepository;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,IStudentRepository studentRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _studentRepository = studentRepository;
        }
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
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var user = await _userManager.FindByNameAsync(loginViewModel.EmailAddress ?? "");
            if (user == null)
            {
                ModelState.AddModelError("LoginError", "Ongeldige login gegevens");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, true, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("LoginError", "Ongeldige login gegevens");
                return View();
            }

            return RedirectToAction("package", "packageList");
        }

        [HttpPost]
        public async Task<IActionResult> Register(StudentViewModel studentViewModel)
        {
            if (!ModelState.IsValid)
            {
                
                return View();
            }

            var user = new IdentityUser { UserName = studentViewModel.EmailAddress};
            var result = await _userManager.CreateAsync(user, studentViewModel.Password);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("IdentityFramework", err.Description);
                }

                Console.WriteLine("user niet opgeslagen");
  
                return View();
            }
            await _studentRepository.Add(new Student
            {
                
                BirthDate = studentViewModel.BirthDate,
                Name = studentViewModel.Name,
                PhoneNumber = studentViewModel.PhoneNumber,
                StudyCity = studentViewModel.StudyCity,
            });

            await _signInManager.SignInAsync(user, true);
            return RedirectToAction("packageList", "package");
        }



    }
}
