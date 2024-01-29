using Core.Domain;
using Infrastructure;

namespace Portal.Models
{
    public class PackageViewModel
    {
        public IEnumerable<Package> Packages { get; set; } = Enumerable.Empty<Package>();
        public string ErrorMessage { get; set; }
        public IEnumerable<Canteen> Canteens { get; set; } = Enumerable.Empty<Canteen>();

        public Employee? Employee { get; set; }
    }
}
