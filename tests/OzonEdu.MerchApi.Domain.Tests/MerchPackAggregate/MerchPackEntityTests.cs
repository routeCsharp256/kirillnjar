using System.Collections.Generic;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.Exceptions;
using Xunit;

namespace OzonEdu.MerchApi.Domain.Tests.MerchPackAggregate
{
    public class MerchPackEntityTests
    {

        [Fact]
        public void CreateMerchPackSuccess()
        {
            //Arrange 
            var type = MerchPackType.WelcomePack;
            var items = new Dictionary<MerchItem, MerchItemsQuantity>
            {
                {new MerchItem(1, new Sku(1)), new MerchItemsQuantity(1)},
                {new MerchItem(2, new Sku(2)), new MerchItemsQuantity(1)},
                {new MerchItem(3, new Sku(3)), new MerchItemsQuantity(1)},
                {new MerchItem(4, new Sku(4)), new MerchItemsQuantity(2)}
            };
        

            //Act
            var merchPack = new MerchPack(type, items);

            //Assert  
            Assert.Equal(merchPack.Type, type);
            Assert.NotNull(merchPack.Items);
            Assert.NotEmpty(merchPack.Items);
        }
        
        
        [Fact]
        public void CreateMerchPackWithNullType()
        {
            //Arrange 
            var items = new Dictionary<MerchItem, MerchItemsQuantity>
            {
                {new MerchItem(1, new Sku(1)), new MerchItemsQuantity(1)},
                {new MerchItem(2, new Sku(2)), new MerchItemsQuantity(1)},
                {new MerchItem(3, new Sku(3)), new MerchItemsQuantity(1)},
                {new MerchItem(4, new Sku(4)), new MerchItemsQuantity(2)}
            };
            //Act

            //Assert  
            Assert.Throws<RequiredEntityPropertyIsNullException>(() => new MerchPack(null, items));
        }
        
        [Fact]
        public void CreateMerchPackWithNullItems()
        {
            //Arrange 
            var type = MerchPackType.WelcomePack;
            //Act

            //Assert  
            Assert.Throws<RequiredEntityPropertyIsNullException>(() => new MerchPack(type, null));
        }
    }
}