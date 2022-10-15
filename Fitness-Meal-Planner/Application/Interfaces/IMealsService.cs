using Application.Dtos.MealDtos;
using Domain.Additional_Structures;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMealsService
    {
        IQueryable<MealDto> GetAllMeals();
        Task<IEnumerable<MealDto>> GetMealsPagedAsync(int pageNumber, int pageSize, NutritionRange range, bool? ascendingSort);
        Task<MealDto> GetMealByIdAsync(Guid id);
        Task<MealDto> AddMealAsync(CreateMealDto newMeal, string mealPhotoPath);
        Task<MealDto> UpdateMealAsync(UpdateMealDto updatedMeal, Guid id, string mealPhotoPath);
        Task<MealDto> PatchMealAsync(JsonPatchDocument patchedMeal, Guid id);
        Task DeleteMealAsync(Guid id);
        Task<int> CountMealsAsync();
        Task<string> GetPathOfMealImage(Guid id);
    }
}
