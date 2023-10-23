using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyDomainService
{
    public interface IPackageRepository
    {
        public IEnumerable<Package> Packages { get; }

        void AddPackage(Package newPackage);
    }
}
