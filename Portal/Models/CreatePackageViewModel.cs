using Core.Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Portal.Models
{
    public class CreatePackageViewModel
    {
        public string? Name { get; set; }
        public int Price { get; set; }
        public City City { get; set; }
        public Meal Meal { get; set; }
        public DateTime RetrieveDate { get; set; }
        public DateTime DeadLineRetriveDate { get; set; }

        //public List<Product> Products { get; } = [];

        public List<int> SelectedProductIds { get; set; }

        public MultiSelectList Products { get; set; }
    }
}
