using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMealsService
    {
        Task<IEnumerable<MealDto>> GetAllMealsAsync();
        Task<IEnumerable<MealDto>> GetMealsPagedAsync(int pageNumber, int pageSize);
        Task<MealDto> GetMealByIdAsync(Guid id);
        Task<MealDto> AddMealAsync(CreateMealDto newMeal);
        Task UpdateMealAsync(UpdateMealDto updatedMeal, Guid id);
        Task DeleteMealAsync(Guid id);
        Task<int> CountMealsAsync();
    }
}
