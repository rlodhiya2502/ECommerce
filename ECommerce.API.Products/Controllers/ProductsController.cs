using ECommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Products.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider productsProvider;

        public ProductsController(IProductsProvider productsProvider)
        {
            this.productsProvider = productsProvider;
        }

        //todo: getproducts
        //todo: get products by id.
    }
}
