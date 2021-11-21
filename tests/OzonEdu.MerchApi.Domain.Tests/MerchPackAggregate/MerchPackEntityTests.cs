using System.Collections.Generic;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.Exceptions;
using Xunit;

namespace OzonEdu.MerchApi.Domain.Tests.MerchPackAggregate
{
    public class MerchPackEntityTests
    {

        [Fact]
        public void Constructor_WhenMerchPackValid_DoesNotThrow()
        {
            //Arrange 
            var type = MerchPackType.WelcomePack;
            var items = new Dictionary<MerchItem, MerchItemsQuantity>
            {
                {new MerchItem(1, Sku.Create(1)), MerchItemsQuantity.Create(1)},
                {new MerchItem(2, Sku.Create(2)), MerchItemsQuantity.Create(1)},
                {new MerchItem(3, Sku.Create(3)), MerchItemsQuantity.Create(1)},
                {new MerchItem(4, Sku.Create(4)), MerchItemsQuantity.Create(2)}
            };
        

            //Act
            var merchPack = new MerchPack(type, items);

            //Assert  
            Assert.Equal(merchPack.Type, type);
            Assert.NotNull(merchPack.Items);
            Assert.NotEmpty(merchPack.Items);
        }
        
        
        [Fact]
        public void Constructor_WhenMerchPackTypeNull_Throw()
        {
            //Arrange 
            var items = new Dictionary<MerchItem, MerchItemsQuantity>
            {
                {new MerchItem(1, Sku.Create(1)), MerchItemsQuantity.Create(1)},
                {new MerchItem(2, Sku.Create(2)), MerchItemsQuantity.Create(1)},
                {new MerchItem(3, Sku.Create(3)), MerchItemsQuantity.Create(1)},
                {new MerchItem(4, Sku.Create(4)), MerchItemsQuantity.Create(2)}
            };
            //Act

            //Assert  
            Assert.Throws<RequiredEntityPropertyIsNullException>(() => new MerchPack(null, items));
        }
        
        [Fact]
        public void Constructor_WhenMerchPackItemsNull_Throw()
        {
            //Arrange 
            var type = MerchPackType.WelcomePack;
            //Act

            //Assert  
            Assert.Throws<RequiredEntityPropertyIsNullException>(() => new MerchPack(type, null));
        }
    }
}