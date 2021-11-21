using System;
using System.Linq;
using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate.EmployeeName;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchApi.Domain.Exceptions;
using OzonEdu.MerchApi.Domain.Exceptions.MerchRequestAggregate;
using OzonEdu.MerchApi.Domain.Models;
using Xunit;

namespace OzonEdu.MerchApi.Domain.Tests.MerchRequestAggregate
{
    public class MerchRequestEntityTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void SetAsAwaitingDeliveryTestSuccess(int merchRequestStatus)
        {
            //Arrange
            var currentDateTime = MerchRequestDateTime.Create(DateTime.Parse("16.11.2021"));
            var testRequest =
                new MerchRequest(
                    _merchRequest.Employee,
                    _merchRequest.MerchPackId,
                    _merchRequest.MerchRequestDateTime,
                    Enumeration
                        .GetAll<MerchRequestStatus>()
                        .FirstOrDefault(it => it.Id.Equals(merchRequestStatus)), 
                    _merchRequest.MerchRequestFrom);
                
            //Act
            testRequest.SetAsAwaitingDelivery(currentDateTime);
            //Assert  
            Assert.Equal(testRequest.MerchRequestStatus.Id, MerchRequestStatus.AwaitingDelivery.Id);
        }
        
        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public void SetAsAwaitingDeliveryTestNotSuccess(int merchRequestStatus)
        {
            //Arrange
            var currentDateTime = MerchRequestDateTime.Create(DateTime.Parse("16.11.2021"));
            var testRequest =
                new MerchRequest(
                    _merchRequest.Employee,
                    _merchRequest.MerchPackId,
                    _merchRequest.MerchRequestDateTime,
                    Enumeration
                        .GetAll<MerchRequestStatus>()
                        .FirstOrDefault(it => it.Id.Equals(merchRequestStatus)), 
                    _merchRequest.MerchRequestFrom);
            //Act
            
            //Assert  
            Assert.Throws<MerchRequestStatusException>(() => testRequest.SetAsAwaitingDelivery(currentDateTime));
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(4)]
        public void SetAsDoneTestSuccess(int merchRequestStatus)
        {
            //Arrange
            var currentDateTime = MerchRequestDateTime.Create(DateTime.Parse("16.11.2021"));
            var testRequest =
                new MerchRequest(
                    _merchRequest.Employee,
                    _merchRequest.MerchPackId,
                    _merchRequest.MerchRequestDateTime,
                    Enumeration
                        .GetAll<MerchRequestStatus>()
                        .FirstOrDefault(it => it.Id.Equals(merchRequestStatus)), 
                    _merchRequest.MerchRequestFrom);

            //Act
            testRequest.SetAsDone(currentDateTime);
            //Assert  
            Assert.Equal(testRequest.MerchRequestStatus.Id, MerchRequestStatus.Done.Id);
        }
        
        [Theory]
        [InlineData(3)]
        public void SetAsDoneTestNotSuccess(int merchRequestStatus)
        {
            //Arrange
            var currentDateTime = MerchRequestDateTime.Create(DateTime.Parse("16.11.2021"));
            var testRequest =
                new MerchRequest(
                    _merchRequest.Employee,
                    _merchRequest.MerchPackId,
                    _merchRequest.MerchRequestDateTime,
                    Enumeration
                        .GetAll<MerchRequestStatus>()
                        .FirstOrDefault(it => it.Id.Equals(merchRequestStatus)), 
                    _merchRequest.MerchRequestFrom);
            //Act
            
            //Assert  
            Assert.Throws<MerchRequestStatusException>(() => testRequest.SetAsDone(currentDateTime));
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void SetAsCanceledDeliveryTestSuccess(int merchRequestStatus)
        {
            //Arrange
            var currentDateTime = MerchRequestDateTime.Create(DateTime.Parse("16.11.2021"));
            var testRequest =
                new MerchRequest(
                    _merchRequest.Employee,
                    _merchRequest.MerchPackId,
                    _merchRequest.MerchRequestDateTime,
                    Enumeration
                        .GetAll<MerchRequestStatus>()
                        .FirstOrDefault(it => it.Id.Equals(merchRequestStatus)), 
                    _merchRequest.MerchRequestFrom);

            //Act
            testRequest.SetAsCanceled(currentDateTime);
            //Assert  
            Assert.Equal(testRequest.MerchRequestStatus.Id, MerchRequestStatus.Canceled.Id);
        }
        
        [Theory]
        [InlineData(4)]
        public void SetAsCanceledDeliveryTestNotSuccess(int merchRequestStatus)
        {
            //Arrange
            var currentDateTime = MerchRequestDateTime.Create(DateTime.Parse("16.11.2021"));
            var testRequest =
                new MerchRequest(
                    _merchRequest.Employee,
                    _merchRequest.MerchPackId,
                    _merchRequest.MerchRequestDateTime,
                    Enumeration
                        .GetAll<MerchRequestStatus>()
                        .FirstOrDefault(it => it.Id.Equals(merchRequestStatus)), 
                    _merchRequest.MerchRequestFrom);
            //Act
            
            //Assert  
            Assert.Throws<MerchRequestStatusException>(() => testRequest.SetAsCanceled(currentDateTime));
        }
        
        [Theory]
        [InlineData("01.01.2020", "31.12.2020")]
        [InlineData("01.01.1998", "05.06.1998")]
        public void IsIssuedLessYearTrue(string requestDateString, string currentDateString)
        {
            //Arrange
            var requestDateTime = DateTime.Parse(requestDateString);
            var currentDateTime = DateTime.Parse(currentDateString);
            
            var testRequest =
                new MerchRequest(
                    _merchRequest.Employee,
                    _merchRequest.MerchPackId,
                    MerchRequestDateTime.Create(requestDateTime),
                    MerchRequestStatus.Done,
                    _merchRequest.MerchRequestFrom);

            //Act
            var result = testRequest.IsIssuedLessYear(currentDateTime);
            //Assert  
            Assert.True(result);
        }

        [Theory]
        [InlineData("01.01.2020", "31.12.2021")]
        [InlineData("01.01.1998", "01.01.1999")]
        public void IsIssuedLessYearFalse(string requestDateString, string currentDateString)
        {
            //Arrange
            var requestDateTime = DateTime.Parse(requestDateString);
            var currentDateTime = DateTime.Parse(currentDateString);
            
            var testRequest =
                new MerchRequest(
                    _merchRequest.Employee,
                    _merchRequest.MerchPackId,
                    MerchRequestDateTime.Create(requestDateTime),
                    MerchRequestStatus.Done,
                    _merchRequest.MerchRequestFrom);

            //Act
            var result = testRequest.IsIssuedLessYear(currentDateTime);
            //Assert  
            Assert.False(result);
        }
        
        [Fact]
        public void CreateMerchRequestWithNulls()
        {
            //Arrange 
            //Act

            //Assert  
            Assert.Throws<RequiredEntityPropertyIsNullException>(() =>
                new MerchRequest(
                    null,
                    1,
                    null,
                    null,
                    null));
        }
        
        #region init

        private readonly MerchRequest _merchRequest =
            new(1, 
                new Employee(
                    Email.Create("iivanov@mail.com"),
                    FullName.Create("Ivan", "Ivanov", "Ivanovich")),
                1,
                MerchRequestDateTime.Create(DateTime.Parse("21.12.2020")),
                MerchRequestStatus.AwaitingDelivery,
                new MerchRequestFrom(MerchRequestFromType.Automatically));
       
        #endregion
    }
    
    
}