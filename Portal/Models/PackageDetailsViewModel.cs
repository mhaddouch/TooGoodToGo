using Core.Domain;
using Infrastructure;

namespace Portal.Models
{
    public class PackageDetailsViewModel
    {
        public Package? Package { get; set; }

        public List<Product> Products { get; set; }
    }
}
