using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext dbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomersDbContext dbContext,ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Db.Customer() { Id = 1, Name = "Krystof Raska", Address = "2 Portsmith St." });
                dbContext.Customers.Add(new Db.Customer() { Id = 2, Name = "Nalin Jhwari", Address = "132 Windrush Drive" });
                dbContext.Customers.Add(new Db.Customer() { Id = 3, Name = "Will Smith", Address = "1 Palm Drive" });
                dbContext.SaveChanges();
            }
        }


        public Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<(bool IsSuccess, IEnumerable<Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
