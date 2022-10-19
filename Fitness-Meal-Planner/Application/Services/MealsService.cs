using Application.Dtos.MealDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Additional;
using Domain.Additional_Structures;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;
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
        private readonly IMapper _mapper;
        private readonly IMealsRepository _repository;
        private readonly IValidator<Meal> _validator;
        public MealsService(IMapper mapper, IMealsRepository repository, IValidator<Meal> validator)
        {
            _mapper = mapper;
            _repository = repository;
            _validator = validator;
        }
        public IQueryable<MealDto> GetAllMeals()
        {
            var meals = _repository.GetAllMeals();
            return _mapper.ProjectTo<MealDto>(meals);
        }
        public async Task<IEnumerable<MealDto>> GetMealsPagedAsync(int pageNumber, int pageSize, NutritionRange range, bool? ascendingSort)
        {
            var meals = await _repository.GetMealsPagedAsync(pageNumber, pageSize, range, ascendingSort);
            return _mapper.Map<IEnumerable<MealDto>>(meals);
        }
        public async Task<MealDto> GetMealByIdAsync(Guid id)
        {
            var meal = await _repository.GetMealByIdAsync(id);
            return _mapper.Map<MealDto>(meal);
        }
        public async Task<MealDto> AddMealAsync(CreateMealDto newMeal, string mealImagePath)
        {
            var meal = _mapper.Map<Meal>(newMeal);
            meal.MealPhotoPath = mealImagePath;

            ValidationResult result = await _validator.ValidateAsync(meal);
            if (!result.IsValid)
                return null;

            await _repository.AddMealAsync(meal);
            return _mapper.Map<MealDto>(meal);
        }
        public async Task<MealDto> UpdateMealAsync(UpdateMealDto updatedMeal, Guid id, string mealPhotoPath)
        {
            var existingMeal = await _repository.GetMealByIdAsync(id);
            var meal = _mapper.Map(updatedMeal, existingMeal);
            meal.MealPhotoPath = mealPhotoPath;

            ValidationResult result = await _validator.ValidateAsync(meal);
            if (!result.IsValid)
                return null;

            await _repository.UpdateMealAsync(meal);
            return _mapper.Map<MealDto>(meal);
        }
        public async Task DeleteMealAsync(Guid id)
        {
            var meal = await _repository.GetMealByIdAsync(id);
            await _repository.DeleteMealAsync(meal);
        }
        public async Task<int> CountMealsAsync()
        {
            return await _repository.CountMealsAsync();
        }
        public async Task<string> GetPathOfMealImage(Guid id)
        {
            var meal = await _repository.GetMealByIdAsync(id);
            return meal.MealPhotoPath;
        }
    }
}
