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
        [HttpPost]
        public async Task<ActionResult> AddMeal(CreateMealDto newMeal)
        {
            var meal = await service.AddMealAsync(newMeal);
            return Created($"/meals.{meal.id}", new Response<MealDto>(meal));
        }
    }
}
