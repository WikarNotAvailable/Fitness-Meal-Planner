using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    //service operating on a meals' repository, meals controller is using its functions
    public class MealsService : IMealsService
    {
        private readonly IMapper mapper;
        private readonly IMealsRepository repository;
        public MealsService(IMapper _mapper, IMealsRepository _repository)
        {
            mapper = _mapper;
            repository = _repository;
        }
        public async Task<IEnumerable<MealDto>> GetAllMealsAsync()
        {
            var meals = await repository.GetAllMealsAsync();
            return mapper.Map<IEnumerable<MealDto>>(meals);
        }
        public async Task<MealDto> AddMealAsync(CreateMealDto newMeal)
        {
            var meal = mapper.Map<Meal>(newMeal);
            await repository.AddMealAsync(meal);
            return mapper.Map<MealDto>(meal);
        }

     
    }
}
