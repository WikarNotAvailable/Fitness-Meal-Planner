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
        Task<IEnumerable<Meal>> GetAllMealsAsync();
        Task<Meal> GetMealById(Guid _id);
        Task AddMealAsync(Meal meal);
    }
}
