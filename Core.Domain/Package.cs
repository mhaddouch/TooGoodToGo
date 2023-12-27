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
        public string? Name { get; set; }
        public int Price { get; set; }
        public City City { get; set; }
        public List<Product>? Products { get; set; }
        public Canteen? Canteen { get; set; }
        public DateTime RetrieveDate { get; set; }
        public DateTime DeadLineRetriveDate { get; set; }
        public Student? ReserverdByStudent { get; set; }

    }
}
