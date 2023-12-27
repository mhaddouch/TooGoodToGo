using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Canteen
    {
        public int Id { get; set; }

        public string? LocationName { get; set; }

        public City City { get; set; }

        public Boolean OfferHotMeals { get; set; }
    }
}
