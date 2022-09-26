using Domain.Additional_Structures;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    //interface for repository containing meals
    public interface IMealsRepository
    {
        IQueryable<Meal> GetAllMeals();
        Task<IEnumerable<Meal>> GetMealsPagedAsync(int pageNumber, int pageSize, NutritionRange range, bool? ascendingSort);
        Task<Meal> GetMealByIdAsync(Guid _id);
        Task AddMealAsync(Meal meal);
        Task UpdateMealAsync(Meal meal);
        Task DeleteMealAsync(Meal meal);
        Task<int> CountMealsAsync();
    }
}
