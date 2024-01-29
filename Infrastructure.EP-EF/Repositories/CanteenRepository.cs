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
    public class CanteenRepository : ICanteenRepository
    {

        private readonly PackageDbContext _context;

        public CanteenRepository(PackageDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Canteen> GetAll()
        {
             return _context.Canteens.ToList();
        }

        public IEnumerable<Package> GetPackagesByLocation(string locationName)
        {
            return _context.Canteens
                .Where(c => c.LocationName == locationName)
                .SelectMany(c => c.Packages)
                .ToList();
        }


    }
}
