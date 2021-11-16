using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.Exceptions.EmployeeAggregate;
using Xunit;

namespace OzonEdu.MerchApi.Domain.Tests.EmployeeAggregate
{
    public class EmployeeFullNameValueObjectTests
    {
        [Theory]
        [InlineData("ivanov", "ivan", "ivanovich")]
        [InlineData("Иванов", "Иван", "Иванович")]
        [InlineData("Харитонов", "Кондрат", "Антонович")]
        [InlineData("Быков", "Игорь", "Олегович")]
        [InlineData("Григорьев", "Исак", null)]
        public void CreateEmployeeFullNameSuccess(string lastName, string firstName, string middleName)
        {
            //Arrange 

            //Act
            var fullName = new EmployeeFullName(lastName, firstName, middleName);

            //Assert  
            Assert.Equal(fullName.FirstName, firstName);
            Assert.Equal(fullName.LastName, lastName);
            Assert.Equal(fullName.MiddleName, middleName);
        }
        
        
        [Theory]
        [InlineData("", "", "")]
        [InlineData(null, "Иван", "Иванович")]
        [InlineData("Харитонов", null, "Антонович")]
        [InlineData("Быков123", "Игорь", "Олегович")]
        [InlineData("Григорьев", "Исак123", null)]
        [InlineData(" Сергеев", "Алексей", null)]
        [InlineData("Сергеев", " Алексей", null)]
        [InlineData("Сергеев", "Алексей ", null)]
        [InlineData("Сергеев ", "Алексей", null)]
        [InlineData("Харитонов", "Иван", " Антонович")]
        [InlineData("Харитонов", "Иван", "Антонович ")]
        [InlineData("Харитонов", "Иван", "Антон ович")]
        [InlineData("Харитонов", "Иван", "123123")]
        public void CreateEmployeeFullNameNotSuccess(string lastName, string firstName, string middleName)
        {
            //Arrange 
            //Act

            //Assert  
            Assert.Throws<InvalidNameException>(() => new EmployeeFullName(lastName, firstName, middleName));
        }
    }
}