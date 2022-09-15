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
    public class SQLProductsRepository : IProductsRepository
    {
        private readonly FitnessPlannerContext context;
        public SQLProductsRepository(FitnessPlannerContext _context)
        {
            context = _context;
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await context.Products.ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsPagedAsync(int pageNumber, int pageSize, NutritionRange range)
        {
            return await context.Products
                .Where(m => m.calories <= range.maxCalories && m.calories >= range.minCalories && m.protein <= range.maxProtein &&
                    m.protein >= range.minProtein && m.carbohydrates >= range.minCarbohydrates && m.carbohydrates <= range.maxCarbohydrates &&
                    m.fat >= range.minFat && m.fat <= range.maxFat)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<Product> GetProductByIdAsync(Guid _id)
        {
            return await context.Products.SingleOrDefaultAsync(x => x.id == _id);
        }
        public async Task AddProductAsync(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
        }
        
        public async Task UpdateProductAsync(Product product)
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task DeleteProductAsync(Product product)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
        }
        public async Task<int> CountProductsAsync()
        {
            return await context.Products.CountAsync();
        }

        
    }
}
