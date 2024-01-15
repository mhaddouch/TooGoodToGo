using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Core.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public List<Package> Packages { get;} = [];
        //public List<Mealbox> Mealboxes { get; } = [];
        public string? Name { get; set; }
        public bool ContainsAlcohol { get; set; }
        public string? PhotoPath { get; set; } //pathpicture hier een string
    //    public List<Voorbeeld> Voorbeelds { get; set; } = [];


    }
}
