using Domain.Additional_Structures;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.JsonPatch;
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
        private readonly FitnessPlannerContext _context;
        public SQLMealsRepository(FitnessPlannerContext context)
        {
            _context = context;
        }
        public IQueryable<Meal> GetAllMeals()
        {
            return _context.Meals.AsQueryable();
        }
        public async Task<IEnumerable<Meal>> GetMealsPagedAsync(int pageNumber, int pageSize, NutritionRange range, bool? ascendingSort)
        {
            var meals = _context.Meals
                .Where(m => m.Calories <= range.MaxCalories && m.Calories >= range.MinCalories && m.Protein <= range.MaxProtein &&
                    m.Protein >= range.MinProtein && m.Carbohydrates >= range.MinCarbohydrates && m.Carbohydrates <= range.MaxCarbohydrates &&
                    m.Fat >= range.MinFat && m.Fat <= range.MaxFat)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            SearchByName(ref meals, range.Name);

            if (ascendingSort == null)
                return await meals.ToListAsync();
            else if ((bool)ascendingSort)
                return await meals.OrderBy(m => m.Name).ToListAsync();
            else
                return await meals.OrderByDescending(m => m.Name).ToListAsync();
        }
        public async Task<Meal> GetMealByIdAsync(Guid id)
        {
            return await _context.Meals.SingleOrDefaultAsync(x => x.Id == id);
        }
        public async Task AddMealAsync(Meal meal)
        {
            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }
        public async Task UpdateMealAsync(Meal meal)
        {
            _context.Meals.Update(meal);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }
        public async Task PatchMealAsync(JsonPatchDocument patchedMeal, Guid id)
        {
            var meal = await _context.Meals.FindAsync(id);
            patchedMeal.ApplyTo(meal);

            await _context.SaveChangesAsync();
        }
        public async Task DeleteMealAsync(Meal meal)
        {
            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }
        public async Task<int> CountMealsAsync()
        {
            return await _context.Meals.CountAsync();
        }
        private void SearchByName(ref IQueryable<Meal> meals, string nameOfMeal) 
        {
            if (!meals.Any() || string.IsNullOrWhiteSpace(nameOfMeal))
                return;

            meals = meals.Where(m => m.Name.ToLower().Contains(nameOfMeal.Trim().ToLower()));
        }


    }
}
