using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainServices
{
    public interface IPackageRepository
    {
        IEnumerable<Package> Packages { get; }
        IEnumerable<Package> GetAll();
        Task AddPackage(Package newPackage);
        Task RemovePackage(Package package);
        Task ReservePackage(Package package, Student student);
        Package GetPackageById(int packageId);
        IEnumerable<Package> GetReservePackage();
        IEnumerable<Package> GetNonReservePackage();
        IEnumerable<Package> GetReservedPackagesByLocation(int canteenId);

        Task EditPackage(Package updatePackage);

        IQueryable<Package> Query();

    }
}
