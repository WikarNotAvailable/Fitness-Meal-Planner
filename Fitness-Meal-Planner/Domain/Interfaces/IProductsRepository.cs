using Domain.Additional_Structures;
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
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetProductsPagedAsync(int pageNumber, int pageSize, NutritionRange range, bool? ascendingSorting);
        Task<Product> GetProductByIdAsync(Guid _id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
        Task<int> CountProductsAsync();
    }
}
