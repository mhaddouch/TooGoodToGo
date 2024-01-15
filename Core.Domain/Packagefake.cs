using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class PackageFake
    {
        private Student? reserverdByStudent;
        private List<Product>? products = new List<Product>();

        public int Id { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public City City { get; set; }
        public Meal Meal { get; set; }
        public Canteen? Canteen { get; set; }
        public int CanteenId {  get; set; }
        public DateTime RetrieveDate { get; set; }
        public DateTime DeadLineRetriveDate { get; set; }
        public Student? ReserverdByStudent
        {
            get => reserverdByStudent; 
            set
            {
                reserverdByStudent = value;
                IsAvailableToStudent();
            }
        }

        public void AddProduct(Product product)
        {
            IsAvailableToStudent(product);
            products?.Add(product);
        }

        public void IsAvailableToStudent(Product product = null!)
        {
            if(product != null && product.ContainsAlcohol 
                || (products?.Any(p => p.ContainsAlcohol) ?? false))
            {
                if (ReserverdByStudent?.Age < 18)
                {
                    throw new DomainException("Minderjarigen mogen geen alcoholische dranken bestellen");
                }
            }
        }
    }
}
