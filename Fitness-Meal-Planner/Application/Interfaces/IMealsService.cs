using Application.Dtos;
using Domain.Additional_Structures;
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
        Task<MealDto> AddMealAsync(CreateMealDto newMeal, string _mealPhotoPath);
        Task UpdateMealAsync(UpdateMealDto updatedMeal, Guid id, string _mealPhotoPath);
        Task DeleteMealAsync(Guid id);
        Task<int> CountMealsAsync();
        Task<string> GetPathOfMealImage(Guid id);
    }
}
