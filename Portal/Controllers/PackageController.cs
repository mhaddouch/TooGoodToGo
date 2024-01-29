using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portal.Models;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Infrastructure.EP_EF.Repositories;

namespace Portal.Controllers
{
    public class PackageController : Controller
    {

        private readonly IPackageRepository _packageRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICanteenRepository _canteenRepository;
        private readonly IProductRepository _productRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly UserManager<IdentityUser> _userManager;


        public PackageController(IPackageRepository packageRepository, IStudentRepository studentRepository
            , UserManager<IdentityUser> userManager, ICanteenRepository canteenRepository, IProductRepository productRepository, IEmployeeRepository employeeRepository)
        {
            _packageRepository = packageRepository;
            _studentRepository = studentRepository;
            _userManager = userManager;
            _canteenRepository = canteenRepository;
            _productRepository = productRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<IActionResult> PackageList()
        {

            var viewModel = new PackageViewModel
            {
                Packages = _packageRepository.GetNonReservePackage(),
                Canteens = _canteenRepository.GetAll(),
            };
            if (User.HasClaim("Employee", "true")){
                var user = User.FindFirst(ClaimTypes.Name);
               // var employee = await _employeeRepository.GetEmployeeByEmployeeNumber(int.Parse(user.Value));
               viewModel.Employee = await _employeeRepository.GetEmployeeByEmployeeNumber(int.Parse(user.Value));
            }
           
            viewModel.ErrorMessage = TempData["ErrorMessage"] as string;
            TempData.Clear();
            return View("PackageList", viewModel);
        }
        public IActionResult CanteenList(int canteenId)
        {
            
            var viewModel = new PackageViewModel
            {
                Packages = _packageRepository.GetNonReservePackage()
                .Where(r => r.CanteenId == canteenId)
                .OrderBy(r => r.RetrieveDate).ToList(),

                Canteens = _canteenRepository.GetAll(),
                //Packages = _packageRepository.GetReservedPackagesByLocation(canteenId),
            };
            viewModel.ErrorMessage = TempData["ErrorMessage"] as string;
            TempData.Clear();
            return View("PackageList", viewModel);
        }

        /*
                [HttpPost]

                public async Task<IActionResult> ReservePackage(Package package,Student student)
                {

                   // var reservationStudent =  _packageRepository.ReservePackage(package, student);
                   await _packageRepository.ReservePackage(package, student);
                    System.Diagnostics.Debug.WriteLine(package.Id);
                    return Redirect("PackageList");

                }*/

        [HttpPost]
        [Authorize(Policy = "StudentPolicy")]
        public async Task<IActionResult> ReservationPackages(int packageId)
        {
            try
            {
                var user = User.FindFirst(ClaimTypes.Name);
                var student = await _studentRepository.GetStudentByStudentNumber(int.Parse(user.Value));
                var package = _packageRepository.GetPackageById(packageId);

                // package.ReservePackage = student;
            

                await _packageRepository.ReservePackage(package, student);
                
                // Save changes to the database asynchronously


                return RedirectToAction("ReservationPackages");
            }catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("PackageList");
            }
            
        }
        [Authorize(Policy = "StudentPolicy")]
        public IActionResult ReservationPackages()
        {
            
                var user = User.FindFirst(ClaimTypes.Name);

                var viewModel = new PackageViewModel
                {
                    Packages = _packageRepository.GetReservePackage().Where(r => r.reserverdByStudent.StudentNumber == int.Parse(user.Value))

                };
                return View("ReservationPackages", viewModel);

            
            
            
        }
        [HttpGet]
        [Authorize(Policy = "EmployeePolicy")]
        public IActionResult CreatePackage()
        {
            var user = User.FindFirst(ClaimTypes.Name);
            var allProducts = _productRepository.GetAll();

            var viewModel = new CreatePackageViewModel
            {
                Products = new MultiSelectList(allProducts, nameof(Product.Id), nameof(Product.Name))
            };

            return View(viewModel);
        }
        [HttpPost]
        [Authorize(Policy = "EmployeePolicy")]
        public async Task<IActionResult> CreatePackage(CreatePackageViewModel createPackageViewModel)
        {
            var user = User.FindFirst(ClaimTypes.Name);
            var employee =await _employeeRepository.GetEmployeeByEmployeeNumber(int.Parse(user.Value));

            var correspondingProducts = _productRepository.GetAll().Where(r=>createPackageViewModel.SelectedProductIds.Contains(r.Id));
            await _packageRepository.AddPackage(new Package
            {

                Name = createPackageViewModel.Name,
                Price = createPackageViewModel.Price,
                Canteen = employee.Canteen,
                Meal = createPackageViewModel.Meal,
                RetrieveDate = createPackageViewModel.RetrieveDate,
                DeadLineRetriveDate = createPackageViewModel.DeadLineRetriveDate,
                Products = correspondingProducts.ToList(),
            }) ;

            return RedirectToAction("Packagelist");
        }
        /*[HttpPost]
       public IActionResult PackageDetails(int packageId)
        {
            var package = _packageRepository.GetPackageById(packageId);

            var packageViewDetailsModel = new PackageDetailsViewModel
            {
                // Copy the existing packages
                Package = package,
                Products = package.Products
                // Assign the loaded products to the Products property
   
            };

            return View(packageViewDetailsModel);

        }*/
        public IActionResult PackageDetails(int packageId)
        {
            var package = _packageRepository.GetPackageById(packageId);

            var packageViewDetailsModel = new PackageDetailsViewModel
            {
                // Copy the existing packages
                Package = package,
                Products = package.Products
                // Assign the loaded products to the Products property

            };

            return View(packageViewDetailsModel);
        }

        [HttpPost]
        [Authorize(Policy = "EmployeePolicy")]
        public async Task<IActionResult> DeletePackage(int packageId)
        {
            var user = User.FindFirst(ClaimTypes.Name);
            var employee = await _employeeRepository.GetEmployeeByEmployeeNumber(int.Parse(user.Value));
            var package = _packageRepository.GetPackageById(packageId);

            await _packageRepository.RemovePackage(package);

            return RedirectToAction("Packagelist");

        }

        [HttpPost]
        [Authorize(Policy = "EmployeePolicy")]
        public async Task<IActionResult> UpdatePackage(CreatePackageViewModel updatePackageViewModel)
        {
            var user = User.FindFirst(ClaimTypes.Name);
            var employee = await _employeeRepository.GetEmployeeByEmployeeNumber(int.Parse(user.Value));

            var correspondingProducts = _productRepository.GetAll().Where(r => updatePackageViewModel.SelectedProductIds.Contains(r.Id));

            //var package = _packageRepository.GetPackageById(packageId);

        //    await _packageRepository.EditPackage(updatePackageViewModel);

            return RedirectToAction("Packagelist");
        }

        [HttpGet]
        [Authorize(Policy = "EmployeePolicy")]
        public IActionResult UpdatePackage()
        {
            var user = User.FindFirst(ClaimTypes.Name);
            var allProducts = _productRepository.GetAll();

            var viewModel = new CreatePackageViewModel
            {
                Products = new MultiSelectList(allProducts, nameof(Product.Id), nameof(Product.Name))
            };

            return View(viewModel);
        }



    }
}