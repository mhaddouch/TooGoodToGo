using Core.Domain;
using Core.DomainServices;

using Microsoft.AspNetCore.Mvc;
using Portal.Models;

namespace Portal.Controllers
{
    public class PackageController : Controller
    {

        private readonly IPackageRepository packageRepo;


        public PackageController(IPackageRepository packageRepo)
        {
            this.packageRepo = packageRepo;
        }

        public IActionResult PackageList()
        {
            
            var viewModel = new PackageViewModel
            {
                Packages = this.packageRepo.Packages,
            };

            
            return View("PackageList", viewModel);
        }
    }
}
