using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
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
