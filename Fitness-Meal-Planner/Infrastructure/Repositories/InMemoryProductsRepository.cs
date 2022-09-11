using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
namespace Infrastructure.Repositories
{
    //trivial repository in memory containing products, only for testing purposes
    public class InMemoryProductsRepository : IProductsRepository
    {
        private static readonly List<Product> products = new()
        {
            new Product("Protein Bar", 45, 195, 20.2m, 14.1m, 3.2m, "Exemplary Ingredients", "Exemplaty Description")
        };



        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }
        public void AddProduct(Product product)
        {
            products.Add(product);
        }
    }
}
