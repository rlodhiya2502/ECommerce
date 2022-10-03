using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Profiles;
using ECommerce.Api.Customers.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ECommerce.Api.Customers.Tests
{
    public class CustomersServiceTest
    {
        [Fact]
        public async Task GetCustomersReturnAllCustomers()
        {
            var options = new DbContextOptionsBuilder<CustomersDbContext>()
                .UseInMemoryDatabase(nameof(GetCustomersReturnAllCustomers))
                .Options;
            var dbContext = new CustomersDbContext(options);
            CreateCustomers(dbContext);

            var customerProfile = new CustomerProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(customerProfile));
            var mapper = new Mapper(configuration);

            var customersProvider = new CustomersProvider(dbContext, null, mapper);

            var customer = await customersProvider.GetCustomersAsync();
            Assert.True(customer.IsSuccess);
            Assert.True(customer.Customers.Any());
            Assert.Null(customer.ErrorMessage);
        }


        [Fact]
        public async Task GetCustomerReturnsCustomerUsingValidId()
        {
            var options = new DbContextOptionsBuilder<CustomersDbContext>()
               .UseInMemoryDatabase(nameof(GetCustomersReturnAllCustomers))
               .Options;
            var dbContext = new CustomersDbContext(options);
            CreateCustomers(dbContext);

            var customerProfile = new CustomerProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(customerProfile));
            var mapper = new Mapper(configuration);

            var customersProvider = new CustomersProvider(dbContext, null, mapper);

            var customer = await customersProvider.GetCustomerAsync(1);
            Assert.True(customer.IsSuccess);
            Assert.NotNull(customer.Customer);
            Assert.True(customer.Customer.Id == 1);
            Assert.Null(customer.ErrorMessage);

        }

        [Fact]
        public async Task GetCustomerReturnsErrorUsingInvalidId()
        {
            var options = new DbContextOptionsBuilder<CustomersDbContext>()
               .UseInMemoryDatabase(nameof(GetCustomersReturnAllCustomers))
               .Options;
            var dbContext = new CustomersDbContext(options);
            CreateCustomers(dbContext);

            var customerProfile = new CustomerProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(customerProfile));
            var mapper = new Mapper(configuration);

            var customersProvider = new CustomersProvider(dbContext, null, mapper);

            var customer = await customersProvider.GetCustomerAsync(-1);
            Assert.False(customer.IsSuccess);
            Assert.Null(customer.Customer);
            Assert.NotNull(customer.ErrorMessage);

        }

        private void CreateCustomers(CustomersDbContext dbContext)
        {
            for (int i = 1; i <= 10; i++)
            {
                dbContext.Customers.Add(new Customer()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Address = Guid.NewGuid().ToString(),    
                });
            }
            dbContext.SaveChanges();
        }
    }
}
