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
        private readonly IEmployeeRepository _employeeRepository;

        public AccountController(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager,
            IStudentRepository studentRepository, 
            IEmployeeRepository employeeRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _studentRepository = studentRepository;
            _employeeRepository = employeeRepository;
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
            var user = await _userManager.FindByNameAsync(loginViewModel.RegistrationNumber ?? "");
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

            // bekende gebruiker, bepaal of gebruiker een student of medewerker is
            bool registrationNumberFilled = int.TryParse(loginViewModel.RegistrationNumber, out int registrationNumber);
            if(_employeeRepository.Exists(registrationNumber))
            {
                // ingelogde gebruiker is medewerker -->
                return RedirectToAction("packageList", "package");
            }
            else if (_studentRepository.Exists(registrationNumber))
            {
                // ingelogde gebruiker is student -->
                return RedirectToAction("packageList", "package");
            }
            else
            {
                ModelState.AddModelError("LoginError", "Geen student of medewerker gevonden");
                return View();
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> RegisterStudent(StudentViewModel studentViewModel)
        {
            Student student = null!;
            try
            {
                student = new Student
                {
                    StudentNumber = studentViewModel.StudentNumber,
                    EmailAddress = studentViewModel.EmailAddress,
                    BirthDate = studentViewModel.BirthDate,
                    Name = studentViewModel.Name,
                    PhoneNumber = studentViewModel.PhoneNumber,
                    StudyCity = studentViewModel.StudyCity,
                };
            }
            catch (DomainException de)
            {
                ModelState.AddModelError("StudentValidatie", de.Message);
            }           

            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var user = new IdentityUser { UserName = studentViewModel.StudentNumber.ToString() };
                var result = await _userManager.CreateAsync(user, studentViewModel.Password!);

                if (!result.Succeeded)
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("IdentityFramework", err.Description);
                    }

                    Console.WriteLine("user niet opgeslagen");

                    return View();
                }
                await _studentRepository.Add(student);
                await _signInManager.SignInAsync(user, true);

            }
            catch (Exception ex)
            {
                await _studentRepository.Remove(student);
                RedirectToAction("/home/error");
            }

            return RedirectToAction("packageList", "package");
        }

        public IActionResult EmployeeRegister()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EmployeeRegister(EmployeeViewModel employeeViewModel)
        {
            Employee employee = null!;
            try
            {
                employee = new Employee
                {
                    EmployeeNumber = employeeViewModel.EmployeeNumber,
                    EmailAddress = employeeViewModel.EmailAddress,
                    Canteen = employeeViewModel.Canteen,
                    Name = employeeViewModel.Name
                };
            }
            catch (DomainException de)
            {
                ModelState.AddModelError("EmployeePresentatie", de.Message);
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var user = new IdentityUser { UserName = employeeViewModel.EmployeeNumber.ToString() };
                var result = await _userManager.CreateAsync(user, employeeViewModel.Password!);

                if (!result.Succeeded)
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("IdentityFramework", err.Description);
                    }

                    Console.WriteLine("user niet opgeslagen");

                    return View();
                }
                await _employeeRepository.Add(employee);
                await _signInManager.SignInAsync(user, true);

            }
            catch (Exception ex)
            {
                await _employeeRepository.Remove(employee);
                RedirectToAction("/home/error");
            }

            return RedirectToAction("packageList", "package");
        }


    }
}
