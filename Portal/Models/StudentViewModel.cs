using Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace Portal.Models
{
    public class StudentViewModel
    {
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress]
        public string? EmailAddress { get; set; }
     
        public int PhoneNumber { get; set; }
        public DateOnly BirthDate { get; set; }
        public int StudentNumber {  get; set; }
        public City StudyCity { get; set; }
        

        public string? Password { get; set; }
    }
}
