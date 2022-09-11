using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SQLProductsRepository : IProductsRepository
    {
        private readonly FitnessPlannerContext context;
        public SQLProductsRepository(FitnessPlannerContext _context)
        {
            context = _context;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return context.Products;
        }
        public void AddProduct(Product product)
        {
            product.created = DateTime.Now; 
            context.Add(product);
            context.SaveChanges();
        }

        
    }
}
