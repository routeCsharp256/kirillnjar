using System;
using System.Collections.Generic;
using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchApi.Domain.Exceptions.EmployeeAggregate;
using OzonEdu.MerchApi.Infrastructure.Repositories.Implementation.Mock;
using Xunit;

namespace OzonEdu.MerchApi.Domain.Tests
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
            var email = new Email(emailValue);

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
            Assert.Throws<InvalidEmailException>(() => new Email(emailValue));
        }
        
        [Fact]
        public void CreateEmailNotSuccess1()
        {
            //Arrange
            var qwe = new List<MerchRequest>
            {                
                new(1, new Employee(
                        new Email("iivanov@mail.com"),
                        new EmployeeFullName("Ivan", "Ivanov", "Ivanovich")),
                    1,
                    new MerchRequestDateTime(DateTime.Parse("21.12.2020")),
                    MerchRequestStatus.Done,
                    new MerchRequestFrom(MerchRequestFromType.Automatically)),
                new(2, new Employee(
                        new Email("ppetrov@mail.com"),
                        new EmployeeFullName("Petr", "Petrov", "Petrovich")),
                    2,
                    new MerchRequestDateTime(DateTime.Parse("22.12.2020")),
                    MerchRequestStatus.AwaitingDelivery,
                    new MerchRequestFrom(MerchRequestFromType.Manually)),
                new(3, new Employee(
                        new Email("aivanova@mail.com"),
                        new EmployeeFullName("Anna", "Ivanova", "Ivanovna")),
                    5,
                    new MerchRequestDateTime(DateTime.Parse("23.12.2020")),
                    MerchRequestStatus.Canceled,
                    new MerchRequestFrom(MerchRequestFromType.Manually))
            };
            //Act

            //Assert  
            Assert.Throws<InvalidEmailException>(() => new Email("emailValue"));
        }
    }
}