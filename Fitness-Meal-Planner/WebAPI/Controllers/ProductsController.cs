using Application.Dtos.MealDtos;
using Application.Dtos.ProductDtos;
using Application.Interfaces;
using Application.Services;
using Domain.Additional_Structures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using WebAPI.Exceptions;
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
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IProductsService productsService, IWebHostEnvironment webHostEnvironment, ILogger<ProductsController> logger)
        {
            _productsService = productsService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        [AllowAnonymous]
        [HttpGet("all")]
        [EnableQuery]
        public ActionResult<IQueryable<ProductDto>> GetAllProducts()
        {
            _logger.LogInformation("Getting all products from the database as queryable.");
            return Ok(_productsService.GetAllProducts());
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetPagedProducts([FromQuery] PaginationFilter paginationFilter,
            [FromQuery] NutritionValuesFilter nutritionValuesFilter, [FromQuery] bool? ascendingSorting)
        {
            _logger.LogInformation("Getting paged products from the database.");

            if (!nutritionValuesFilter.ValidFilterValues())
            {
                _logger.LogError("Invalid ranges for nutrition values filter were passed.");
                throw new InvalidFilterRangesException("Invalid ranges of nutrition values.");
            }

            var nutritionRange = new NutritionRange(nutritionValuesFilter.MinCalories, nutritionValuesFilter.MaxCalories,
                nutritionValuesFilter.MinProtein, nutritionValuesFilter.MaxProtein, nutritionValuesFilter.MinCarbohydrates,
                nutritionValuesFilter.MaxCarbohydrates, nutritionValuesFilter.MinFat, nutritionValuesFilter.MaxFat, nutritionValuesFilter.Name);

            var validPaginationFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

            var products = await _productsService.GetProductsPagedAsync(validPaginationFilter.PageNumber, validPaginationFilter.PageSize, nutritionRange, ascendingSorting);

            var totalRecords = await _productsService.CountProductsAsync();

            return Ok(PaginationHelper.CreatePagedResponse(products, validPaginationFilter, totalRecords));
        }
        [AllowAnonymous]
        [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new string[] { "id" })]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            _logger.LogInformation("Getting the product from the database by id.");

            var product = await _productsService.GetProductByIdAsync(id);
            if (product == null)
            {
                _logger.LogError("The product was not found in the database.");
                throw new EntityNotFoundException("The passed id is wrong - the product doesn't exist.");
            }

            return Ok(new Response<ProductDto>(product));
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddProduct([FromForm]CreateProductDto newProduct)
        {
            _logger.LogInformation("Adding the product to the database.");

            string productPhotoPath;

            if (newProduct.Image == null || newProduct.Image.Length == 0)
            {
                productPhotoPath = "";
                _logger.LogInformation("The meal is being added without a photo");
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
            if(product == null)
            {
                _logger.LogError("Adding product has not succeedeed.");
                throw new EntityValidatonException("The product you are trying to add has invalid properties.");
            }

            return Created($"/products.{product.Id}", new Response<ProductDto>(product));     
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct ([FromForm]UpdateProductDto updatedProduct, Guid id)
        {
            _logger.LogInformation("Updating the product from the database.");

            var product = await _productsService.GetProductByIdAsync(id);

            if (product == null)
            {
                _logger.LogError("The product to update was not found in the database.");
                throw new EntityNotFoundException("The passed id wrong.");
            }

            string productPhotoPath = await _productsService.GetPathOfProductImage(id);
            if (System.IO.File.Exists(productPhotoPath))
                System.IO.File.Delete(productPhotoPath);

            if (updatedProduct.Image == null || updatedProduct.Image.Length == 0)
            {
                productPhotoPath = "";
                _logger.LogInformation("The product is being updated without a photo.");
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
            var newProduct = await _productsService.UpdateProductAsync(updatedProduct, id, productPhotoPath);
            if(newProduct == null)
            {
                _logger.LogError("Updating product has not succeeded.");
                throw new EntityValidatonException("The updated properties are not valid.");
            }

            return CreatedAtAction("Update", new Response<ProductDto>(newProduct));
        }
       
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct (Guid id)
        {
            _logger.LogInformation("Deleting the meal from the database.");

            var product = await _productsService.GetProductByIdAsync(id);
            if (product == null)
            {
                _logger.LogError("The product to delete was not found in the database.");
                throw new EntityNotFoundException("The passed id is wrong.");
            }

            string productPhotoPath = await _productsService.GetPathOfProductImage(id);
            if (System.IO.File.Exists(productPhotoPath))
                System.IO.File.Delete(productPhotoPath);

            await _productsService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
