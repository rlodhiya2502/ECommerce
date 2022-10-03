using ECommerce.Api.Search.Interfaces;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly ICustomersService customersService;
        private readonly IProductsService productsService;
        private readonly IOrdersService ordersService;
        public SearchService(ICustomersService customersService, IProductsService productsService,
            IOrdersService ordersService)
        {
            this.customersService = customersService;
            this.productsService = productsService;
            this.ordersService = ordersService;

        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var customersResult = await customersService.GetCustomerAsync(customerId);
            var ordersResult = await ordersService.GetOrdersAsync(customerId);
            var productsResult = await productsService.GetProductsAsync();

            var result = new
            {
                Customer = customersResult.IsSuccess ?
                                customersResult.Customer :
                                new { Name = "Customer information is not available" }
            };

            return (true, result);

        }
    }
}
