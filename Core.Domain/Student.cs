using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? EmailAddress { get; set; }
        public int StudentNumber {  get; set; }
        public int PhoneNumber {  get; set; }
        public DateOnly BirthDate { get; set;}
        //methode dat birthdate niet in de toekomst ligt en als je wilt aanmelden moet je minimaal 16 jaar zijn.

        private DateOnly localDate = DateOnly.FromDateTime(DateTime.Now);
        
        public void CorrectDate()
        {
            
            if(this.BirthDate > this.localDate)
            {
               throw new DomainException("verjaardag kan niet later zijn dan vandaag");
            }
            if(this.BirthDate >= (this.localDate.AddYears(-16))){
                throw new DomainException("Voor aanmelden moet je 16 en ouder zijn");
            }
        }
         

        public City StudyCity { get; set; }

        public List<Package>? Package { get; set; }
    }
}
