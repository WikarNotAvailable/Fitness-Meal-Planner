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
        IEnumerable<ProductDto> GetAllProducts();
        void AddProduct(CreateProductDto newProduct);
    }
}
