using Domain.Additional_Structures;
using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
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
        IQueryable<Product> GetAllProducts();
        Task<IEnumerable<Product>> GetProductsPagedAsync(int pageNumber, int pageSize, NutritionRange range, bool? ascendingSorting);
        Task<Product> GetProductByIdAsync(Guid _id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task PatchProductAsync(JsonPatchDocument patchedProduct, Guid id);
        Task DeleteProductAsync(Product product);
        Task<int> CountProductsAsync();
    }
}
