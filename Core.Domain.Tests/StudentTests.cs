using Xunit;

using System.Numerics;

namespace Core.Domain.Tests
{
    public class StudentTests
    {
        [Fact]
        public void ThrowErrorWhenUserisUnder16()
        {
            //Arrage
            var student = new Student();
            var birhtdate = new DateOnly(2009, 6, 4);
            //Act
            Assert.Throws<DomainException>(()=>student.BirthDate = birhtdate);
            //Assert
            Assert.Equal(student.BirthDate, birhtdate);
        }
        //testWhen 15 year old
        //succes a

        [Fact]
        public void GiveSuccesWhenStudentIsAbove16()
        {
            //Arrage
            var student = new Student();
            var birhtdate = new DateOnly(2000, 6, 4);
            //Act
            student.BirthDate = birhtdate;
            //Assert
             Assert.Equal(student.BirthDate,birhtdate);
        }
        
    }
}