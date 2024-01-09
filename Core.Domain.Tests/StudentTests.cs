using Xunit;

using System.Numerics;

namespace Core.Domain.Tests
{
    public class StudentTests
    {
        [Fact]
        public void AgeMustBeHigherThan16AndLowerThanToday()
        {
            //Arrage
            var Student = new Student { BirthDate = new DateOnly(2000,6,4)};
            var Student1 = new Student { BirthDate = new DateOnly(2008, 6, 4) };
            bool ExectionThrown = false;
            //Act
            try{
                Student1.CorrectDate();
            }
            catch (DomainException) {  ExectionThrown = true; }

            //Assert
            Assert.True(ExectionThrown);
        }
    }
}