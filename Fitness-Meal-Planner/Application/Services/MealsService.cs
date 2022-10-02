using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Additional;
using Domain.Additional_Structures;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
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
        public IQueryable<MealDto> GetAllMeals()
        {
            var meals = repository.GetAllMeals();
            return mapper.ProjectTo<MealDto>(meals);
        }
        public async Task<IEnumerable<MealDto>> GetMealsPagedAsync(int pageNumber, int pageSize, NutritionRange range, bool? ascendingSort)
        {
            var meals = await repository.GetMealsPagedAsync(pageNumber, pageSize, range, ascendingSort);
            return mapper.Map<IEnumerable<MealDto>>(meals);
        }
        public async Task<MealDto> GetMealByIdAsync(Guid id)
        {
            var meal = await repository.GetMealByIdAsync(id);
            return mapper.Map<MealDto>(meal);
        }
        public async Task<MealDto> AddMealAsync(CreateMealDto newMeal, string _mealImagePath)
        {
            var meal = mapper.Map<Meal>(newMeal);
            meal.mealPhotoPath = _mealImagePath;
            await repository.AddMealAsync(meal);
            //return mapper.Map<MealDto>(meal);
            var Mealzzz = new MealDto(meal.id, meal.name, meal.weightInGrams, meal.calories, meal.protein, meal.carbohydrates, meal.fat, IngredientsConverter.stringToList(meal.ingredients), meal.recipe);
            return Mealzzz;
        }

        public async Task UpdateMealAsync(UpdateMealDto updatedMeal, Guid id, string _mealPhotoPath)
        {
            var existingMeal = await repository.GetMealByIdAsync(id);
            var meal = mapper.Map(updatedMeal, existingMeal);
            meal.mealPhotoPath = _mealPhotoPath;
            await repository.UpdateMealAsync(meal);
        }
        public async Task PatchMealAsync(JsonPatchDocument patchedMeal, Guid id)
        {
            await repository.PatchMealAsync(patchedMeal, id);
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

        public async Task<string> GetPathOfMealImage(Guid id)
        {
            var meal = await repository.GetMealByIdAsync(id);
            return meal.mealPhotoPath;
        }


    }
}
