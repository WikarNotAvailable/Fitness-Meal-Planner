using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Additional_Structures;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    //service operating on a products' repository, products controller is using its functions
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository repository;
        private readonly IMapper mapper;
        public ProductsService(IProductsRepository _repository, IMapper _mapper)
        {
            repository = _repository;
            mapper = _mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await repository.GetAllProductsAsync();
            return mapper.Map<IEnumerable<ProductDto>>(products);
        }
        public async Task<IEnumerable<ProductDto>> GetProductsPagedAsync(int pageNumber, int pageSize, NutritionRange range, bool? ascendingSorting)
        {
            var products = await repository.GetProductsPagedAsync(pageNumber, pageSize, range, ascendingSorting);
            return mapper.Map<IEnumerable<ProductDto>>(products);
        }
        public async Task<ProductDto> AddProductAsync(CreateProductDto newProduct)
        {
            var product = mapper.Map<Product>(newProduct);
            await repository.AddProductAsync(product);
            return mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var product = await repository.GetProductByIdAsync(id);
            return mapper.Map<ProductDto>(product);
        }

        public async Task UpdateProductAsync(UpdateProductDto updatedProduct, Guid id)
        {
            var existingProduct = await repository.GetProductByIdAsync(id);
            var product = mapper.Map(updatedProduct, existingProduct);
            await repository.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await repository.GetProductByIdAsync(id);
            await repository.DeleteProductAsync(product);
        }
        public async Task<int> CountProductsAsync()
        {
            return await repository.CountProductsAsync();
        }
    }
}
