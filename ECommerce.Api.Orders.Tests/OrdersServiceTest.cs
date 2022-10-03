using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Profiles;
using ECommerce.Api.Orders.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ECommerce.Api.Orders.Tests
{
    public class OrdersServiceTest
    {
        [Fact]
        public async Task GetOrdersReturnsOrdersByValidCustomerId()
        {
            var options = new DbContextOptionsBuilder<OrdersDbContext>()
                .UseInMemoryDatabase(nameof(GetOrdersReturnsOrdersByValidCustomerId))
                .Options;
            var dbContext = new OrdersDbContext(options);
            CreateOrders(dbContext);

            var orderProfile = new OrderProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(orderProfile));
            var mapper = new Mapper(configuration);

            var ordersProvider = new OrdersProvider(dbContext, null, mapper);

            var order = await ordersProvider.GetOrdersAsync(1);
            Assert.NotNull(order.Orders);
            Assert.True(order.IsSuccess);
            Assert.Null(order.ErrorMessage);
        }

        [Fact]
        public async Task GetOrdersReturnsErrorByInvalidCustomerId()
        {
            var options = new DbContextOptionsBuilder<OrdersDbContext>()
                .UseInMemoryDatabase(nameof(GetOrdersReturnsErrorByInvalidCustomerId))
                .Options;
            var dbContext = new OrdersDbContext(options);
            CreateOrders(dbContext);

            var orderProfile = new OrderProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(orderProfile));
            var mapper = new Mapper(configuration);

            var ordersProvider = new OrdersProvider(dbContext, null, mapper);

            var order = await ordersProvider.GetOrdersAsync(-1);
            Assert.Null(order.Orders);
            Assert.False(order.IsSuccess);
            Assert.NotNull(order.ErrorMessage);
            

        }

        private void CreateOrders(OrdersDbContext dbContext)
        {
            for (int i = 1; i <= 5; i++)
            {
                dbContext.Orders.Add(new Order()
                {
                    Id = i,
                    CustomerId = i,
                    OrderDate = DateTime.Now,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
            }
            dbContext.SaveChanges();
        }
    }
}
