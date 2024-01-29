using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Core.DomainServices;
using Core.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Infrastructure.EP_EF;
using System.Security.Claims;

namespace Portal.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IStudentRepository _studentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICanteenRepository _canteenRepository;

        public AccountController(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager,
            IStudentRepository studentRepository, 
            IEmployeeRepository employeeRepository, ICanteenRepository canteenRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _studentRepository = studentRepository;
            _employeeRepository = employeeRepository;
            _canteenRepository = canteenRepository;
           
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
                await _userManager.AddClaimAsync(user, new Claim("Employee", "true"));
                return RedirectToAction("packageList", "package");
            }
            else if (_studentRepository.Exists(registrationNumber))
            {
                // ingelogde gebruiker is student -->
                await _userManager.AddClaimAsync(user, new Claim("Student", "true"));
                return RedirectToAction("ReservationPackages", "package");
            }
            else
            {
                ModelState.AddModelError("LoginError", "Geen student of medewerker gevonden");
                return View();
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Register(StudentViewModel studentViewModel)
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

            return RedirectToAction("login", "account");
        }

        public IActionResult EmployeeRegister()
        {
            var canteens = _canteenRepository.GetAll(); // Replace with your actual service or data retrieval logic

            ViewBag.Canteens = canteens;
            return View();
        }
        /*  [HttpPost]
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
        */

        [HttpPost]
        public async Task<IActionResult> EmployeeRegister(EmployeeViewModel employeeViewModel)
        {
            if (!ModelState.IsValid)
            {
                var canteens = _canteenRepository.GetAll(); // Replace with your actual service or data retrieval logic

                ViewBag.Canteens = canteens;

                return View();
            }

            var user = new IdentityUser { UserName = employeeViewModel.EmployeeNumber.ToString() };
            var result = await _userManager.CreateAsync(user, employeeViewModel.Password!);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("IdentityFramework", err.Description);
                }

                var canteens = _canteenRepository.GetAll(); // Replace with your actual service or data retrieval logic

                ViewBag.Canteens = canteens;
                Console.WriteLine("user niet opgeslagen");

                return View();
            }
            Console.WriteLine("hij komt tot hier");
            
            //stap 1 canteen ophalen aan de hand van de id die in mijn employeeVM zit
            //sla op in variable naam die net is ingezet
            var correspondingCanteen = _canteenRepository.GetAll();
            await _employeeRepository.Add(new Employee
            {

                Name = employeeViewModel.Name,
                EmployeeNumber = employeeViewModel.EmployeeNumber,
                Email = employeeViewModel.Email,
                CanteenId = employeeViewModel.SelectedCanteenId,
            }) ;

            await _signInManager.SignInAsync(user, true);
           
            


            return RedirectToAction("login", "account");
        }

      


    }
}
