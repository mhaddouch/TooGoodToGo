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


        public void AddPackage(Package newPackage)
        {
            packages.Add(newPackage);
        }

        public IEnumerable<Package> GetAll()
        {
            return _context.Packages;
        }
        public async Task ReservePackage(Package package, Student student)
        {
            package.ReserverdByStudent = student;
            
            await _context.SaveChangesAsync();
        }

       
        public Package GetPackageById(int packageId)
        {
            return _context.Packages.FirstOrDefault(p => p.Id == packageId);
        }


    }
}

