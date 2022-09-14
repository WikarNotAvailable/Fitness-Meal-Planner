using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMealsService mealsService;
        public MealsController(IMealsService _service)
        {
            mealsService = _service;
        }
        [HttpGet("all")]
        public async Task<IEnumerable<MealDto>> GetAllMeals()
        {
            return await mealsService.GetAllMealsAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MealDto>>> GetPagedMeals([FromQuery] PaginationFilter paginationFilter)
        {
            var validPaginationFilter = new PaginationFilter(paginationFilter.pageNumber, paginationFilter.pageSize);

            var meals = await mealsService.GetMealsPagedAsync(validPaginationFilter.pageNumber, validPaginationFilter.pageSize);

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
        public async Task<ActionResult> AddMeal(CreateMealDto newMeal)
        {
            var meal = await mealsService.AddMealAsync(newMeal);
            return Created($"/meals.{meal.id}", new Response<MealDto>(meal));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMeal(UpdateMealDto updatedMeal, Guid id)
        {
            var meal = await mealsService.GetMealByIdAsync(id);

            if (meal == null)
                return NotFound();

            await mealsService.UpdateMealAsync(updatedMeal, id);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMeal(Guid id)
        {
            var meal = await mealsService.GetMealByIdAsync(id);

            if (meal == null)
                return NotFound();

            await mealsService.DeleteMealAsync(id);
            return NoContent();
        }
    }
}
