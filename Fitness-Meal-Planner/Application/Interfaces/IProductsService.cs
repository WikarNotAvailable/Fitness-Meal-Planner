using Application.Dtos;
using Domain.Additional_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    //interface for service working on products repository
    public interface IProductsService
    {
        IQueryable<ProductDto> GetAllProducts();
        Task<IEnumerable<ProductDto>> GetProductsPagedAsync(int pageNumber, int pageSize, NutritionRange range, bool? ascendingSorting);
        Task<ProductDto> AddProductAsync(CreateProductDto newProduct);
        Task<ProductDto> GetProductByIdAsync(Guid id);
        Task UpdateProductAsync (UpdateProductDto updatedProduct, Guid id);
        Task DeleteProductAsync (Guid id);
        Task<int> CountProductsAsync();
    }
}
