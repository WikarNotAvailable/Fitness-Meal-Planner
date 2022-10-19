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
    public class SQLProductsRepository : IProductsRepository
    {
        private readonly FitnessPlannerContext _context;
        public SQLProductsRepository(FitnessPlannerContext context)
        {
            _context = context;
        }
        public IQueryable<Product> GetAllProducts()
        {
            return _context.Products.AsQueryable();
        }
        public async Task<IEnumerable<Product>> GetProductsPagedAsync(int pageNumber, int pageSize, NutritionRange range, bool? ascendingSort)
        {
            var products = _context.Products
                .Where(m => m.Calories <= range.MaxCalories && m.Calories >= range.MinCalories && m.Protein <= range.MaxProtein &&
                    m.Protein >= range.MinProtein && m.Carbohydrates >= range.MinCarbohydrates && m.Carbohydrates <= range.MaxCarbohydrates &&
                    m.Fat >= range.MinFat && m.Fat <= range.MaxFat)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            SearchByName(ref products, range.Name);

            if (ascendingSort == null)
                return await products.ToListAsync();
            else if ((bool)ascendingSort)
                return await products.OrderBy(p => p.Name).ToListAsync();
            else
                return await products.OrderByDescending(p => p.Name).ToListAsync();         
        }
        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _context.Products.SingleOrDefaultAsync(x => x.Id == id);
        }
        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }
        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }
        public async Task DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }
        public async Task<int> CountProductsAsync()
        {
            return await _context.Products.CountAsync();
        }
        private void SearchByName(ref IQueryable<Product> products, string nameOfProduct)
        {
            if (!products.Any() || string.IsNullOrWhiteSpace(nameOfProduct))
                return;

            products = products.Where(m => m.Name.ToLower().Contains(nameOfProduct.Trim().ToLower()));
        }
    }
}
