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
        public async Task<IEnumerable<Meal>> GetAllMealsAsync()
        {
            return await context.Meals.ToListAsync();
        }
        public async Task<IEnumerable<Meal>> GetMealsPagedAsync(int pageNumber, int pageSize)
        {
            return await context.Meals.Skip((pageNumber-1) *pageSize)
                .Take(pageSize)
                .ToListAsync();
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

      
    }
}
