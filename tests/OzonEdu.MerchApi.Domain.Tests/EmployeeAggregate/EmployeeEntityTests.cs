using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate.EmployeeName;
using OzonEdu.MerchApi.Domain.Exceptions;
using Xunit;

namespace OzonEdu.MerchApi.Domain.Tests.EmployeeAggregate
{
    public class EmployeeEntityTests
    {
        [Theory]
        [InlineData("iivanov@mail.com", "ivanov", "ivan", "ivanovich")]
        [InlineData("wyuvrajanshus@bookea.site", "Иванов", "Иван", "Иванович")]
        [InlineData("3espen.stirlin@osmye.com", "Харитонов", "Кондрат", "Антонович")]
        [InlineData("9kame@zipsq.site", "Быков", "Игорь", "Олегович")]
        [InlineData("lnicolas.kiikan5@sauhasc.com", "Григорьев", "Исак", null)]
        public void CreateEmployeeSuccess(string email, string lastName, string firstName, string middleName)
        {
            //Arrange 
            var testEmail = Email.Create(email);
            var testFullName = FullName.Create(lastName, firstName, middleName);
            
            //Act
            var employee = new Employee(testEmail, testFullName);

            //Assert  
            Assert.Equal(employee.Email.Value, email);
            Assert.Equal(employee.Name.FirstName.Value, firstName);
            Assert.Equal(employee.Name.LastName.Value, lastName);
            Assert.Equal(employee.Name.MiddleName.Value, middleName);
        }
        
        
        [Fact]
        public void CreateEmployeeWithNullEmail()
        {
            //Arrange 
            var testFullName = FullName.Create("ivanov", "ivan", "ivanovich");
            
            //Act

            //Assert  
            Assert.Throws<RequiredEntityPropertyIsNullException>(() => new Employee(null, testFullName));
        }
        
        [Fact]
        public void CreateEmployeeWithNullName()
        {
            //Arrange 
            var testEmail = Email.Create("iivanov@mail.com");
            
            //Act

            //Assert  
            Assert.Throws<RequiredEntityPropertyIsNullException>(() => new Employee(testEmail, null));
        }
    }
}