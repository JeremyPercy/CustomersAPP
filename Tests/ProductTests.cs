using System;
using Xunit;
using SupportecCustomer.API.Models;
using SupportecCustomer.API.Data;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace Tests
{
    public class ProductTests
    {
        [Fact]
        public void GetAllItems_ReturnListItems()
        {
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => (repo.GetItems())).ReturnsAsync(GetItems);

            var result = mockRepo.Object.GetItems().ToAsyncEnumerable();

            Assert.IsNotType<List<Item>>(result);
        }


        [Fact]
        public void GetTotalPriceTest()
        {
            var items = GetItems();

            decimal totalPrice = 0;

            foreach (var item in items)
            {
                totalPrice += item.Price;
            }

            Assert.Equal(98, totalPrice);
        }

        public List<Item> GetItems()
        {
            return new List<Item>(){
            new Item()
            {
                Code = "132431",
                Description = "Test 1",
                Price = 20
            },
            new Item()
            {
                Code = "45435435",
                Description = "Test 2",
                Price = 78
            }
            };
        }
    }
}
