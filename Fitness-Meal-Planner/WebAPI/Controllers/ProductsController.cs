using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    //First version of products' controller
    [ApiController]
    [ApiVersion("1.0")]
    [Route("products")]
    public class ProductsController
    {
        private readonly IProductsService productsService;
        public ProductsController(IProductsService _productsService)
        {
            productsService = _productsService;
        }
        [HttpGet]
        public IEnumerable<ProductDto> GetAllProducts()
        {
            var products = productsService.GetAllProducts();
            return products;
        }
        [HttpPost]
        public void AddProduct(CreateProductDto newProduct)
        {
            productsService.AddProduct(newProduct);
        }
    }
}
