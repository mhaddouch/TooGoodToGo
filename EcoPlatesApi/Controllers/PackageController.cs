using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcoPlatesApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
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

        // GET: PackageController
        [HttpGet]
        [Route("")]
        
        public IEnumerable<Package> GetAll()
        {
            return _packageRepository.GetAll();
        }

        // GET: PackageController/Details/5

        [HttpGet]
        [Route("{id}")]

        public ActionResult<Package> GetPackageById([FromRoute] int id)
        {
            var package =  _packageRepository.GetPackageById(id);
            if (package == null)
            {
                return NotFound(new { error = "pakket niet gevonden" });
            }
            return package;
        }
        
        // GET: PackageController/Create
       

        // POST: PackageController/Create
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<ActionResult<Package>> CreatePackage(Package package)
        {
            try
            {
                await _packageRepository.AddPackage(package);
                return StatusCode(200, new { message = "Package successfully added", statusCode = 200 });
            }
            catch (Exception e)
            {
                return StatusCode((400), e.Message);
            }
        }
        /*
        // GET: PackageControlller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PackageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PackageController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PackageControlller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
