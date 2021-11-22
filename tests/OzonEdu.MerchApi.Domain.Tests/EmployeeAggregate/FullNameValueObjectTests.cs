using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.Exceptions.EmployeeAggregate;
using Xunit;

namespace OzonEdu.MerchApi.Domain.Tests.EmployeeAggregate
{
    public class FullNameValueObjectTests
    {
        [Theory]
        [InlineData("ivanov", "ivan", "ivanovich")]
        [InlineData("Иванов", "Иван", "Иванович")]
        [InlineData("Харитонов", "Кондрат", "Антонович")]
        [InlineData("Быков", "Игорь", "Олегович")]
        [InlineData("Григорьев", "Исак", null)]
        public void Constructor_WhenFullNameValid_DoesNotThrow(string lastName, string firstName, string middleName)
        {
            //Arrange 

            //Act
            var fullName = FullName.Create(lastName, firstName, middleName);

            //Assert  
            Assert.Equal(fullName.FirstName.Value, firstName);
            Assert.Equal(fullName.LastName.Value, lastName);
            Assert.Equal(fullName.MiddleName.Value, middleName);
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
        public void Constructor_WhenFullNameInvalid_Throw(string lastName, string firstName, string middleName)
        {
            //Arrange 
            //Act

            //Assert  
            Assert.Throws<InvalidNameException>(() => FullName.Create(lastName, firstName, middleName));
        }
    }
}