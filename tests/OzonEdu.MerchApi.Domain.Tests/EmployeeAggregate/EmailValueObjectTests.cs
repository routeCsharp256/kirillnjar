using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.Exceptions.EmployeeAggregate;
using Xunit;

namespace OzonEdu.MerchApi.Domain.Tests.EmployeeAggregate
{
    public class EmailValueObjectTests
    {
        [Theory]
        [InlineData("iivanov@mail.com")]
        [InlineData("wyuvrajanshus@bookea.site")]
        [InlineData("3espen.stirlin@osmye.com")]
        [InlineData("9kame@zipsq.site")]
        [InlineData("lnicolas.kiikan5@sauhasc.com")]
        public void CreateEmailSuccess(string emailValue)
        {
            //Arrange 

            //Act
            var email = Email.Create(emailValue);

            //Assert  
            Assert.Equal(email.Value, emailValue);
        }
        
        
        [Theory]
        [InlineData("iivanov")]
        [InlineData("@bookea.site")]
        [InlineData("3espen.stirlin@osmye")]
        [InlineData("9kame.zipsq.site")]
        public void CreateEmailNotSuccess(string emailValue)
        {
            //Arrange 
            //Act

            //Assert  
            Assert.Throws<InvalidEmailException>(() => Email.Create(emailValue));
        }
    }
}