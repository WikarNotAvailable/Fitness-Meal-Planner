using Application.Dtos.MealDtos;
using Application.Interfaces;
using Domain.Additional_Structures;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.IO;
using WebAPI.Filters;
using WebAPI.Helpers;
using WebAPI.Wrappers;
using static System.Net.Mime.MediaTypeNames;

namespace WebAPI.Controllers
{
    //First version of meals' controller
    [ApiController]
    [ApiVersion("1.0")]
    [Route("meals")]
    public class MealsController : ControllerBase
    {
        private readonly IMealsService mealsService;
        private readonly IWebHostEnvironment webHostEnvironment;
        public MealsController(IMealsService _service, IWebHostEnvironment _webHostEnvironment)
        {
            mealsService = _service;
            webHostEnvironment = _webHostEnvironment;
        }
        [HttpGet("all")]
        [EnableQuery]
        public IQueryable<MealDto> GetAllMeals()
        {
            return mealsService.GetAllMeals();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MealDto>>> GetPagedMeals([FromQuery] PaginationFilter paginationFilter,
            [FromQuery] NutritionValuesFilter nutritionValuesFilter, [FromQuery] bool? ascendingSort)
        {
            if (!nutritionValuesFilter.ValidFilterValues())
                return BadRequest("Invalid range of nutrition values");

            var nutritionRange = new NutritionRange(nutritionValuesFilter.minCalories, nutritionValuesFilter.maxCalories,
                nutritionValuesFilter.minProtein, nutritionValuesFilter.maxProtein, nutritionValuesFilter.minCarbohydrates,
                nutritionValuesFilter.maxCarbohydrates, nutritionValuesFilter.minFat, nutritionValuesFilter.maxFat, nutritionValuesFilter.name);

            var validPaginationFilter = new PaginationFilter(paginationFilter.pageNumber, paginationFilter.pageSize);

            var meals = await mealsService.GetMealsPagedAsync(validPaginationFilter.pageNumber, validPaginationFilter.pageSize, nutritionRange, ascendingSort);

            var totalRecords = await mealsService.CountMealsAsync();

            return Ok(PaginationHelper.CreatePagedResponse(meals, validPaginationFilter, totalRecords));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MealDto>> GetMeal(Guid id)
        {
            var meal = await mealsService.GetMealByIdAsync(id);

            if (meal == null)
                return NotFound();

            return Ok(new Response<MealDto>(meal));
        }
        [HttpPost]
        public async Task<ActionResult> AddMeal([FromForm]CreateMealDto newMeal)
        {
            string mealPhotoPath;
            if(newMeal.image == null || newMeal.image.Length == 0)
            {
                mealPhotoPath = "";
            }
            else
            {
                string uniqueID = Guid.NewGuid().ToString();
                var path = Path.Combine(webHostEnvironment.WebRootPath, "UploadedImages", uniqueID + newMeal.image.FileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await newMeal.image.CopyToAsync(stream);
                    stream.Close();
                }
               mealPhotoPath = path;
            }
            var meal = await mealsService.AddMealAsync(newMeal, mealPhotoPath);
            return Created($"/meals.{meal.id}", new Response<MealDto>(meal));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMeal([FromForm]UpdateMealDto updatedMeal, Guid id)
        {
            var meal = await mealsService.GetMealByIdAsync(id);

            if (meal == null)
                return NotFound();

            var mealPhotoPath = await mealsService.GetPathOfMealImage(id);
            if (System.IO.File.Exists(mealPhotoPath))
                System.IO.File.Delete(mealPhotoPath);

            if (updatedMeal.image == null || updatedMeal.image.Length == 0)
            {
                mealPhotoPath = "";
            }
            else
            {
                string uniqueID = Guid.NewGuid().ToString();
                var path = Path.Combine(webHostEnvironment.WebRootPath, "UploadedImages", uniqueID + updatedMeal.image.FileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await updatedMeal.image.CopyToAsync(stream);
                    stream.Close();
                }
                mealPhotoPath = path;
            }
            await mealsService.UpdateMealAsync(updatedMeal, id, mealPhotoPath);
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchMeal(JsonPatchDocument patchedMeal, Guid id)
        {
            var meal = await mealsService.GetMealByIdAsync(id);

            if (meal == null)
                return NotFound();

            await mealsService.PatchMealAsync(patchedMeal, id);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMeal(Guid id)
        {
            var meal = await mealsService.GetMealByIdAsync(id);

            if (meal == null)
                return NotFound();

            var mealPhotoPath = await mealsService.GetPathOfMealImage(id);
            if (System.IO.File.Exists(mealPhotoPath))
                System.IO.File.Delete(mealPhotoPath);

            await mealsService.DeleteMealAsync(id);
            return NoContent();
        }
    }
}
