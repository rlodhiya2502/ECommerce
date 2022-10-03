using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
            SeedData();
        }
        private void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Db.Product() { Id = 1, Name = "Keyboard", Price = 25, Inventory = 120 });
                dbContext.Products.Add(new Db.Product() { Id = 2, Name = "Mouse", Price = 8, Inventory = 150 });
                dbContext.Products.Add(new Db.Product() { Id = 3, Name = "Monitor", Price = 190, Inventory = 1000 });
                dbContext.Products.Add(new Db.Product() { Id = 4, Name = "CPU", Price = 250, Inventory = 2000 });
                dbContext.SaveChanges();
            }
        }


        public Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
