//using Core.DomainServices;

using Core.Domain;
using Core.DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EP_EF.Repositories
{
    public class PackageRepository : IPackageRepository
    {

        private readonly PackageDbContext _context;

        public PackageRepository(PackageDbContext context)
        {
            _context = context;
        }

        private List<Package> packages = new List<Package>();

        public IEnumerable<Package> Packages => packages;


        public async Task AddPackage(Package newPackage)
        {
            if(newPackage.Meal == Meal.avondmaaltijd && !newPackage.Canteen.OfferHotMeals)
            {
                throw new Exception("hier kan je geen warme maaltijden maken");
            }
           await _context.Packages.AddAsync(newPackage);
            await _context.SaveChangesAsync();
        }
        public async Task RemovePackage(Package package)
        {

            _context.Packages.Remove(package);
            await _context.SaveChangesAsync();
           

        }

        public IEnumerable<Package> GetAll()
        {
            return _context.Packages;
        }
         public async Task ReservePackage(Package package, Student student)
         {
            
            var packagesOnDay = _context.Packages
          .Where(x => x.RetrieveDate.Date == package.RetrieveDate.Date&& x.Id !=package.Id )
          .ToList();  // Retrieve the data to perform the subsequent check in-memory

            // Perform logic in-memory
            bool hasPackageOnDay = packagesOnDay.Any(x => x.ReserverdByStudent != null && x.ReserverdByStudent.Id == student.Id);

            if (hasPackageOnDay == true)
            {
                throw new Exception("je hebt al een package op die dag gereserveerd");
            }
            else if (package.ReserverdByStudent != null)
            {

                throw new Exception("sorry de pakket is al geserveerd");
            }
            else
            {
                package.ReserverdByStudent = student;
                _context.Packages.Update(package);
                await _context.SaveChangesAsync();
            }
           
         }

     

            public Package GetPackageById(int packageId)
        {
            return _context.Packages.Include(p => p.Products).Include(v => v.ReserverdByStudent).FirstOrDefault(p => p.Id == packageId);
        }

        public IEnumerable<Package> GetReservePackage() {
            return _context.Packages.Include(x => x.ReserverdByStudent).Where(r =>r.ReserverdByStudent != null).Include(p => p.Products);
        }

        public IEnumerable<Package> GetNonReservePackage()
        {
            return _context.Packages.Include(x => x.ReserverdByStudent).Where(r => r.ReserverdByStudent == null).Include(p => p.Products);
        }
        public IEnumerable<Package> GetReservedPackagesByLocation(int canteenId)
        {
            var reservedPackages = _context.Packages
        .Include(e => e.Canteen) // Include the associated canteen
        .Where(p => p.CanteenId == canteenId && p.ReserverdByStudent == null);
       


            // Debugging: Print some information to the console
            Console.WriteLine($"CanteenId: {canteenId}");
            return  reservedPackages;
            
        }

        public async Task EditPackage(Package updatePackage)
        {
            if (updatePackage.Meal == Meal.avondmaaltijd && !updatePackage.Canteen.OfferHotMeals)
            {
                throw new Exception("hier kan je geen warme maaltijden maken");
            }

            _context.Update(updatePackage);
            await _context.SaveChangesAsync();
        }
        public IQueryable<Package> Query()
        {
            return _context.Packages;
        }

    }
}

