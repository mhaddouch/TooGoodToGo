using Core.Domain;

namespace Portal.Models
{
    public class StudentViewModel
    {
        public string? Name { get; set; }
        public string? EmailAddress { get; set; }

        public int PhoneNumber { get; set; }
        public DateOnly BirthDate { get; set; }

        public City StudyCity { get; set; }
        public string? Password { get; set; }
    }
}
