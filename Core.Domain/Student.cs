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
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public int StudentNumber {  get; set; }
        public DateOnly BirthDate { get; set;}
        //methode dat birthdate niet in de toekomst ligt en als je wilt aanmelden moet je minimaal 16 jaar zijn.


        public City StudyCity { get; set; }

        public Package? Package { get; set; }
    }
}
