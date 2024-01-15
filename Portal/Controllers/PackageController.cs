using Core.Domain;
using Core.DomainServices;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portal.Models;

namespace Portal.Controllers
{
    public class PackageController : Controller
    {

        private readonly IPackageRepository _packageRepository;
        private readonly IStudentRepository _studentRepository;


        public PackageController(IPackageRepository packageRepository,IStudentRepository studentRepository)
        {
            _packageRepository = packageRepository;
            _studentRepository = studentRepository;
        }

        public IActionResult PackageList()
        {
            var viewModel = new PackageViewModel
            {
                Packages = _packageRepository.GetAll(),
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
        public IActionResult ReservePackage(Package package)
        {
            var returnPackge = _packageRepository.GetPackageById(package.Id);

            return View("PackageList",returnPackge);
        }

        public ViewResult ReservePackage()
        {

            var viewModel = new PackageViewModel
            {
          Packages = _packageRepository.Packages.Where(r => r.ReserverdByStudent.Id != null)
            };
            return View("PackageList", viewModel);

        }
    }
}