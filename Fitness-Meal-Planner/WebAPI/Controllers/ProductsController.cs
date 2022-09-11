using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Wrappers;

namespace WebAPI.Controllers
{
    //First version of products' controller
    [ApiController]
    [ApiVersion("1.0")]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;
        public ProductsController(IProductsService _productsService)
        {
            productsService = _productsService;
        }
        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var products = await productsService.GetAllProductsAsync();
            return products;
        }
        [HttpPost]
        public async Task<ActionResult> AddProduct(CreateProductDto newProduct)
        {
            var product = await productsService.AddProductAsync(newProduct);
            return Created($"/products.{product.id}", new Response<ProductDto>(product));     
        }
    }
}
