using Core.Domain;
using Infrastructure;

namespace Portal.Models
{
    public class PackageViewModel
    {
        public IEnumerable<Package> Packages { get; set; } = Enumerable.Empty<Package>();

    }
}
