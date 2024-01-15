using Core.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Portal.Models
{
    public class EmployeeViewModel
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int EmployeeNumber { get; set; }
        public int SelectedCanteenId { get; set; }
        public Canteen? Canteen { get; set; }

     
        public IEnumerable<Canteen> CanteenList { get; set; } = Enumerable.Empty<Canteen>();
        public string? Password { get; set; }
    }
}
