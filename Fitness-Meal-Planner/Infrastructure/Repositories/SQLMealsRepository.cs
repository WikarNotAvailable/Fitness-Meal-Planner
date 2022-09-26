using Domain.Additional_Structures;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    // class working on sql database, more specifically on products table
    public class SQLMealsRepository : IMealsRepository
    {
        private readonly FitnessPlannerContext context;
        public SQLMealsRepository(FitnessPlannerContext _context)
        {
            context = _context;
        }
        public IQueryable<Meal> GetAllMeals()
        {
            return context.Meals.AsQueryable();
        }
        public async Task<IEnumerable<Meal>> GetMealsPagedAsync(int pageNumber, int pageSize, NutritionRange range, bool? ascendingSort)
        {
            var meals = context.Meals
                .Where(m => m.calories <= range.maxCalories && m.calories >= range.minCalories && m.protein <= range.maxProtein &&
                    m.protein >= range.minProtein && m.carbohydrates >= range.minCarbohydrates && m.carbohydrates <= range.maxCarbohydrates &&
                    m.fat >= range.minFat && m.fat <= range.maxFat)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            SearchByName(ref meals, range.name);

            if (ascendingSort == null)
                return await meals.ToListAsync();
            else if ((bool)ascendingSort)
                return await meals.OrderBy(m => m.name).ToListAsync();
            else
                return await meals.OrderByDescending(m => m.name).ToListAsync();
        }
        public async Task<Meal> GetMealByIdAsync(Guid _id)
        {
            return await context.Meals.SingleOrDefaultAsync(x => x.id == _id);
        }
        public async Task AddMealAsync(Meal meal)
        {
            context.Meals.Add(meal);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
        }
        public async Task UpdateMealAsync(Meal meal)
        {
            context.Meals.Update(meal);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
        }
        public async Task DeleteMealAsync(Meal meal)
        {
            context.Meals.Remove(meal);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
        }
        public async Task<int> CountMealsAsync()
        {
            return await context.Meals.CountAsync();
        }
        private void SearchByName(ref IQueryable<Meal> meals, string nameOfMeal) 
        {
            if (!meals.Any() || string.IsNullOrWhiteSpace(nameOfMeal))
                return;

            meals = meals.Where(m => m.name.ToLower().Contains(nameOfMeal.Trim().ToLower()));
        }
    }
}
