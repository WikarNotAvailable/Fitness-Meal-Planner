using Application.Dtos;
using Application.Interfaces;
using Application.Services;
using Domain.Additional_Structures;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
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
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductsController(IProductsService _productsService, IWebHostEnvironment _webHostEnvironment)
        {
            productsService = _productsService;
            webHostEnvironment = _webHostEnvironment;
        }
        [HttpGet("all")]
        [EnableQuery]
        public IQueryable<ProductDto> GetAllProducts()
        {
            return productsService.GetAllProducts();
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
        public async Task<ActionResult> AddProduct([FromForm]CreateProductDto newProduct)
        {
            string productPhotoPath;

            if (newProduct.image == null || newProduct.image.Length == 0)
            {
                productPhotoPath = "";
            }
            else
            {
                string uniqueID = Guid.NewGuid().ToString();
                var path = Path.Combine(webHostEnvironment.WebRootPath, "UploadedImages", uniqueID + newProduct.image.FileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await newProduct.image.CopyToAsync(stream);
                    stream.Close();
                }
                productPhotoPath = path;
            }

            var product = await productsService.AddProductAsync(newProduct, productPhotoPath);
            return Created($"/products.{product.id}", new Response<ProductDto>(product));     
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct ([FromForm]UpdateProductDto updatedProduct, Guid id)
        {
            var product = await productsService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound();

            string productPhotoPath = await productsService.GetPathOfProductImage(id);
            if (System.IO.File.Exists(productPhotoPath))
                System.IO.File.Delete(productPhotoPath);

            if (updatedProduct.image == null || updatedProduct.image.Length == 0)
            {
                productPhotoPath = "";
            }
            else
            {
                string uniqueID = Guid.NewGuid().ToString();
                var path = Path.Combine(webHostEnvironment.WebRootPath, "UploadedImages", uniqueID + updatedProduct.image.FileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await updatedProduct.image.CopyToAsync(stream);
                    stream.Close();
                }
                productPhotoPath = path;
            }

            await productsService.UpdateProductAsync(updatedProduct, id, productPhotoPath);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct (Guid id)
        {
            var product = await productsService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            string productPhotoPath = await productsService.GetPathOfProductImage(id);
            if (System.IO.File.Exists(productPhotoPath))
                System.IO.File.Delete(productPhotoPath);

            await productsService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
