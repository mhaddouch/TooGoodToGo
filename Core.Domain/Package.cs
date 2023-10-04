using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Package
    {

        public string Name { get; set; }
        public City City { get; set; }
        public List<Product> Products { get; set; }
        public Canteen Canteen { get; set; }
        public DateOnly RetrieveDate { get; set; }
        public DateOnly DeadLineRetriveDate { get; set; }
        public Student? ReserverdByStudent { get; set; }

    }
}
