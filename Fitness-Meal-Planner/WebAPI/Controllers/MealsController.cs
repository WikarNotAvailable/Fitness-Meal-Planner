using Application.Dtos.MealDtos;
using Application.Interfaces;
using Domain.Additional_Structures;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.IO;
using WebAPI.Filters;
using WebAPI.Helpers;
using WebAPI.Wrappers;

namespace WebAPI.Controllers
{
    //First version of meals' controller
    [ApiController]
    [ApiVersion("1.0")]
    [Route("meals")]
    public class MealsController : ControllerBase
    {
        private readonly IMealsService _mealsService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<MealsController> _logger;
        public MealsController(IMealsService service, IWebHostEnvironment webHostEnvironment, ILogger<MealsController> logger)
        {
            _mealsService = service;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        [AllowAnonymous]
        [HttpGet("all")]
        [EnableQuery]
        public IQueryable<MealDto> GetAllMeals()
        {
            _logger.LogInformation("Getting all meals from database as queryable.");
            return _mealsService.GetAllMeals();
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MealDto>>> GetPagedMeals([FromQuery] PaginationFilter paginationFilter,
            [FromQuery] NutritionValuesFilter nutritionValuesFilter, [FromQuery] bool? ascendingSort)
        {
            if (!nutritionValuesFilter.ValidFilterValues())
                return BadRequest("Invalid range of nutrition values");

            var nutritionRange = new NutritionRange(nutritionValuesFilter.MinCalories, nutritionValuesFilter.MaxCalories,
                nutritionValuesFilter.MinProtein, nutritionValuesFilter.MaxProtein, nutritionValuesFilter.MinCarbohydrates,
                nutritionValuesFilter.MaxCarbohydrates, nutritionValuesFilter.MinFat, nutritionValuesFilter.MaxFat, nutritionValuesFilter.Name);

            var validPaginationFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

            var meals = await _mealsService.GetMealsPagedAsync(validPaginationFilter.PageNumber, validPaginationFilter.PageSize, nutritionRange, ascendingSort);

            var totalRecords = await _mealsService.CountMealsAsync();

            return Ok(PaginationHelper.CreatePagedResponse(meals, validPaginationFilter, totalRecords));
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<MealDto>> GetMeal(Guid id)
        {
            var meal = await _mealsService.GetMealByIdAsync(id);

            if (meal == null)
                return NotFound();

            return Ok(new Response<MealDto>(meal));
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddMeal([FromForm]CreateMealDto newMeal)
        {
            string mealPhotoPath;
            if(newMeal.Image == null || newMeal.Image.Length == 0)
            {
                mealPhotoPath = "";
            }
            else
            {
                string uniqueID = Guid.NewGuid().ToString();
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "UploadedImages", uniqueID + newMeal.Image.FileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await newMeal.Image.CopyToAsync(stream);
                    stream.Close();
                }
               mealPhotoPath = path;
            }
            var meal = await _mealsService.AddMealAsync(newMeal, mealPhotoPath);
            return Created($"/meals.{meal.Id}", new Response<MealDto>(meal));
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMeal([FromForm]UpdateMealDto updatedMeal, Guid id)
        {
            var meal = await _mealsService.GetMealByIdAsync(id);

            if (meal == null)
                return NotFound();

            var mealPhotoPath = await _mealsService.GetPathOfMealImage(id);
            if (System.IO.File.Exists(mealPhotoPath))
                System.IO.File.Delete(mealPhotoPath);

            if (updatedMeal.Image == null || updatedMeal.Image.Length == 0)
            {
                mealPhotoPath = "";
            }
            else
            {
                string uniqueID = Guid.NewGuid().ToString();
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "UploadedImages", uniqueID + updatedMeal.Image.FileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await updatedMeal.Image.CopyToAsync(stream);
                    stream.Close();
                }
                mealPhotoPath = path;
            }
            await _mealsService.UpdateMealAsync(updatedMeal, id, mealPhotoPath);
            return NoContent();
        }
        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchMeal(JsonPatchDocument patchedMeal, Guid id)
        {
            var meal = await _mealsService.GetMealByIdAsync(id);

            if (meal == null)
                return NotFound();

            await _mealsService.PatchMealAsync(patchedMeal, id);
            return NoContent();
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMeal(Guid id)
        {
            var meal = await _mealsService.GetMealByIdAsync(id);

            if (meal == null)
                return NotFound();

            var mealPhotoPath = await _mealsService.GetPathOfMealImage(id);
            if (System.IO.File.Exists(mealPhotoPath))
                System.IO.File.Delete(mealPhotoPath);

            await _mealsService.DeleteMealAsync(id);
            return NoContent();
        }
    }
}
