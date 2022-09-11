using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    //interface for repository cotaining products
    public interface IProductsRepository
    {
        IEnumerable<Product> GetAllProducts();
        void AddProduct(Product product);
    }
}
