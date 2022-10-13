using Application.Dtos.ProductDtos;
using Application.Interfaces;
using Application.Services;
using Domain.Additional_Structures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.JsonPatch;
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
        private readonly IProductsService _productsService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<ProductsController> _loger;
        public ProductsController(IProductsService productsService, IWebHostEnvironment webHostEnvironment, ILogger<ProductsController> loger)
        {
            _productsService = productsService;
            _webHostEnvironment = webHostEnvironment;
            _loger = loger;
        }
        [AllowAnonymous]
        [HttpGet("all")]
        [EnableQuery]
        public IQueryable<ProductDto> GetAllProducts()
        {
            return _productsService.GetAllProducts();
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetPagedProducts([FromQuery] PaginationFilter paginationFilter,
            [FromQuery] NutritionValuesFilter nutritionValuesFilter, [FromQuery] bool? ascendingSorting)
        {
            if (!nutritionValuesFilter.ValidFilterValues())
                return BadRequest("Invalid range of nutrition values");

            var nutritionRange = new NutritionRange(nutritionValuesFilter.MinCalories, nutritionValuesFilter.MaxCalories,
                nutritionValuesFilter.MinProtein, nutritionValuesFilter.MaxProtein, nutritionValuesFilter.MinCarbohydrates,
                nutritionValuesFilter.MaxCarbohydrates, nutritionValuesFilter.MinFat, nutritionValuesFilter.MaxFat, nutritionValuesFilter.Name);

            var validPaginationFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

            var products = await _productsService.GetProductsPagedAsync(validPaginationFilter.PageNumber, validPaginationFilter.PageSize, nutritionRange, ascendingSorting);

            var totalRecords = await _productsService.CountProductsAsync();

            return Ok(PaginationHelper.CreatePagedResponse(products, validPaginationFilter, totalRecords));
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            var product = await _productsService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(new Response<ProductDto>(product));
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddProduct([FromForm]CreateProductDto newProduct)
        {
            string productPhotoPath;

            if (newProduct.Image == null || newProduct.Image.Length == 0)
            {
                productPhotoPath = "";
            }
            else
            {
                string uniqueID = Guid.NewGuid().ToString();
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "UploadedImages", uniqueID + newProduct.Image.FileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await newProduct.Image.CopyToAsync(stream);
                    stream.Close();
                }
                productPhotoPath = path;
            }

            var product = await _productsService.AddProductAsync(newProduct, productPhotoPath);
            return Created($"/products.{product.Id}", new Response<ProductDto>(product));     
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct ([FromForm]UpdateProductDto updatedProduct, Guid id)
        {
            var product = await _productsService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound();

            string productPhotoPath = await _productsService.GetPathOfProductImage(id);
            if (System.IO.File.Exists(productPhotoPath))
                System.IO.File.Delete(productPhotoPath);

            if (updatedProduct.Image == null || updatedProduct.Image.Length == 0)
            {
                productPhotoPath = "";
            }
            else
            {
                string uniqueID = Guid.NewGuid().ToString();
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "UploadedImages", uniqueID + updatedProduct.Image.FileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await updatedProduct.Image.CopyToAsync(stream);
                    stream.Close();
                }
                productPhotoPath = path;
            }

            await _productsService.UpdateProductAsync(updatedProduct, id, productPhotoPath);
            return NoContent();
        }
        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchMeal(JsonPatchDocument patchedProduct, Guid id)
        {
            var product = await _productsService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound();

            await _productsService.PatchProductAsync(patchedProduct, id);
            return NoContent();
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct (Guid id)
        {
            var product = await _productsService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            string productPhotoPath = await _productsService.GetPathOfProductImage(id);
            if (System.IO.File.Exists(productPhotoPath))
                System.IO.File.Delete(productPhotoPath);

            await _productsService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
