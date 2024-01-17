using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portal.Models;
using System.Security.Claims;

namespace Portal.Controllers
{
    public class PackageController : Controller
    {

        private readonly IPackageRepository _packageRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly UserManager<IdentityUser> _userManager;


        public PackageController(IPackageRepository packageRepository,IStudentRepository studentRepository
            , UserManager<IdentityUser> userManager)
        {
            _packageRepository = packageRepository;
            _studentRepository = studentRepository;
            _userManager = userManager;
        }

        public IActionResult PackageList()
        {
            var viewModel = new PackageViewModel
            {
                Packages = _packageRepository.GetNonReservePackage(),
            };

            
          
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
        public async Task<IActionResult> ReservationPackages(int packageId)
        {
            var user =  User.FindFirst(ClaimTypes.Name);
           

            
            var student = await _studentRepository.GetStudentByStudentNumber(int.Parse(user.Value));
            var package = _packageRepository.GetPackageById(packageId);
           
          // package.ReservePackage = student;
         
         await _packageRepository.ReservePackage(package, student);

            // Save changes to the database asynchronously

           
            return RedirectToAction("ReservationPackages");
        }

        public IActionResult ReservationPackages()
        {
            var user = User.FindFirst(ClaimTypes.Name);

            var viewModel = new PackageViewModel
            {
                Packages = _packageRepository.GetReservePackage().Where(r => r.reserverdByStudent.StudentNumber == int.Parse(user.Value))

        };
            return View("ReservationPackages", viewModel);

        }
    }
}