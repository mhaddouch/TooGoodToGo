using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DummyServices
{
    public interface DummyPackageRepo
    {
        public IEnumerable<Package> Packages { get; }

        void AddPackage(Package newPackage);
    }
}
