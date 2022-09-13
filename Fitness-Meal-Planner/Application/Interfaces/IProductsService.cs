using Application.Dtos;
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
        Task <IEnumerable<ProductDto>> GetAllProductsAsync();
        Task <ProductDto> AddProductAsync(CreateProductDto newProduct);
        Task<ProductDto> GetProductByIdAsync(Guid id);
        Task UpdateProductAsync (UpdateProductDto updatedProduct, Guid id);
        Task DeleteProductAsync (Guid id);
    }
}
