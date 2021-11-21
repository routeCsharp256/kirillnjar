using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.Exceptions.MerchPackAggregate;
using Xunit;

namespace OzonEdu.MerchApi.Domain.Tests.MerchPackAggregate
{
    public class MerchItemQuantityValueObjectTests
    {
        
        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(6)]
        public void MerchItemQuantitySuccess(int quantity)
        {
            //Arrange 

            //Act
            var testQuantity = MerchItemsQuantity.Create(quantity);

            //Assert  
            Assert.Equal(testQuantity.Value, quantity);
        }
        
        
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-6)]
        public void MerchItemQuantityNotSuccess(int quantity)
        {
            //Arrange 
            //Act

            //Assert  
            Assert.Throws<InvalidQuantityException>(() => MerchItemsQuantity.Create(quantity));
        }
    }
}