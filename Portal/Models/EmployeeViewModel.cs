using Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace Portal.Models
{
    public class EmployeeViewModel
    {
        public string? Name { get; set; }
        public string? EmailAddress { get; set; }
        public int EmployeeNumber { get; set; }
        public Canteen? Canteen { get; set; }
        public string? Password { get; set; }
    }
}
