using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Package
    {
        public int Id { get; set; }
        
        public List<Product> Products { get; set; } = [];
     //   public List<Voorbeeld> Voorbeelds { get; } = [];
        

          
        public string? Name { get; set; }
        public int Price { get; set; }
        public City City { get; set; }
        public Meal Meal { get; set; }
        public Canteen? Canteen { get; set; }
        public int? CanteenId {  get; set; }
        public DateTime RetrieveDate { get; set; }
        public DateTime DeadLineRetriveDate { get; set; }

        public Student? reserverdByStudent;
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
            Products?.Add(product);
        }

        public void IsAvailableToStudent(Product product = null!)
        {
            
                if ((product != null && product.ContainsAlcohol) || (Products?.Any(p => p.ContainsAlcohol) ?? false))
                {
                    if (ReserverdByStudent?.Age < 18)
                    {
                        throw new DomainException("Minderjarigen mogen geen 18+ pakketten bestellen");
                    }
                }
            
         


        }
        
    }
}
