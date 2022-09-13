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
    // class working on sql database
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
    }
}
