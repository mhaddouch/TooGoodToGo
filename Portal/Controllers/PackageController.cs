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

            var newPackage = new Package
            {
                Canteen = null,
                Products = null,
                Price = 23,
                City = City.Breda,
                Name = "EERSTEmeal maaltijd",
                DeadLineRetriveDate = new(2002, 3, 2),
                ReserverdByStudent = null,
                RetrieveDate = new(2002, 3, 2)
            };

            viewModel.Packages = viewModel.Packages.Append(newPackage);


            return View("PackageList", viewModel);
        }
    }
}
