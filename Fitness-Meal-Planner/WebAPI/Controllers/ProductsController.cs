using Application.Dtos;
using Application.Interfaces;
using Domain.Additional_Structures;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;
using WebAPI.Helpers;
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
        [HttpGet("all")]
        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            return await productsService.GetAllProductsAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetPagedProducts([FromQuery] PaginationFilter paginationFilter,
            [FromQuery] NutritionValuesFilter nutritionValuesFilter, [FromQuery] bool? ascendingSorting)
        {
            if (!nutritionValuesFilter.ValidFilterValues())
                return BadRequest("Invalid range of nutrition values");

            var nutritionRange = new NutritionRange(nutritionValuesFilter.minCalories, nutritionValuesFilter.maxCalories,
                nutritionValuesFilter.minProtein, nutritionValuesFilter.maxProtein, nutritionValuesFilter.minCarbohydrates,
                nutritionValuesFilter.maxCarbohydrates, nutritionValuesFilter.minFat, nutritionValuesFilter.maxFat, nutritionValuesFilter.name);

            var validPaginationFilter = new PaginationFilter(paginationFilter.pageNumber, paginationFilter.pageSize);

            var products = await productsService.GetProductsPagedAsync(validPaginationFilter.pageNumber, validPaginationFilter.pageSize, nutritionRange, ascendingSorting);

            var totalRecords = await productsService.CountProductsAsync();

            return Ok(PaginationHelper.CreatePagedResponse(products, validPaginationFilter, totalRecords));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            var product = await productsService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(new Response<ProductDto>(product));
        }
        [HttpPost]
        public async Task<ActionResult> AddProduct(CreateProductDto newProduct)
        {
            var product = await productsService.AddProductAsync(newProduct);
            return Created($"/products.{product.id}", new Response<ProductDto>(product));     
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct (UpdateProductDto updatedProduct, Guid id)
        {
            var product = await productsService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound();

            await productsService.UpdateProductAsync(updatedProduct, id);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct (Guid id)
        {
            var product = await productsService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            await productsService.DeleteProductAsync(id);

            return NoContent();
        }
    }
}
