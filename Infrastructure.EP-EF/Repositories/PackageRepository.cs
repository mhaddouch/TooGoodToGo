//using Core.DomainServices;

using Core.Domain;
using Core.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EP_EF.Repositories
{
    public class PackageRepository : IPackageRepository
    {

        private List<Package> packages = new List<Package>();

        public IEnumerable<Package> Packages => packages;


        public void AddPackage(Package newPackage)
        {
            packages.Add(newPackage);
        }

    }
}

