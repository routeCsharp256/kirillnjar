using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.Exceptions;
using Xunit;

namespace OzonEdu.MerchApi.Domain.Tests.MerchPackAggregate
{
    public class MerchItemEntityTests
    {
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void CreateMerchItemSuccess(long sku)
        {
            //Arrange 
            var testSku = new Sku(sku);
            
            //Act
            var merchItem = new MerchItem(testSku);

            //Assert  
            Assert.Equal(merchItem.Sku.Value, merchItem.Sku.Value);
        }
        
        
        [Fact]
        public void CreateMerchItemWithNullSku()
        {
            //Arrange 
            //Act

            //Assert  
            Assert.Throws<RequiredEntityPropertyIsNullException>(() => new MerchItem(null));
        }
    }
}