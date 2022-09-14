using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Wrappers;

namespace WebAPI.Controllers
{
    //First version of meals' controller
    [ApiController]
    [ApiVersion("1.0")]
    [Route("meals")]
    public class MealsController : ControllerBase
    {
        private readonly IMealsService service;
        public MealsController(IMealsService _service)
        {
            service = _service;
        }
        [HttpGet]
        public async Task<IEnumerable<MealDto>> GetAllMeals()
        {
            return await service.GetAllMealsAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MealDto>> GetMeal(Guid id)
        {
            var meal = await service.GetMealByIdAsync(id);

            if (meal == null)
                return NotFound();

            return Ok(meal);
        }
        [HttpPost]
        public async Task<ActionResult> AddMeal(CreateMealDto newMeal)
        {
            var meal = await service.AddMealAsync(newMeal);
            return Created($"/meals.{meal.id}", new Response<MealDto>(meal));
        }
    }
}
