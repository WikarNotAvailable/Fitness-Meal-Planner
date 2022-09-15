using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Additional_Structures;
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
        public async Task<IEnumerable<MealDto>> GetMealsPagedAsync(int pageNumber, int pageSize, NutritionRange range)
        {
            var meals = await repository.GetMealsPagedAsync(pageNumber, pageSize, range);
            return mapper.Map<IEnumerable<MealDto>>(meals);
        }
        public async Task<MealDto> GetMealByIdAsync(Guid id)
        {
            var meal = await repository.GetMealByIdAsync(id);
            return mapper.Map<MealDto>(meal);
        }
        public async Task<MealDto> AddMealAsync(CreateMealDto newMeal)
        {
            var meal = mapper.Map<Meal>(newMeal);
            await repository.AddMealAsync(meal);
            return mapper.Map<MealDto>(meal);
        }

        public async Task UpdateMealAsync(UpdateMealDto updatedMeal, Guid id)
        {
            var existingMeal = await repository.GetMealByIdAsync(id);
            var meal = mapper.Map(updatedMeal, existingMeal);
            await repository.UpdateMealAsync(meal);
        }

        public async Task DeleteMealAsync(Guid id)
        {
            var meal = await repository.GetMealByIdAsync(id);
            await repository.DeleteMealAsync(meal);
        }
        public async Task<int> CountMealsAsync()
        {
            return await repository.CountMealsAsync();
        }

       
    }
}
