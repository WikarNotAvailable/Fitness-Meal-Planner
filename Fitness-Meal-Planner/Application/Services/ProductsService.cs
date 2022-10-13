using Application.Dtos.ProductDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Additional_Structures;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
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
        private readonly IProductsRepository _repository;
        private readonly IMapper _mapper;
        public ProductsService(IProductsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public IQueryable<ProductDto> GetAllProducts()
        {
            var products = _repository.GetAllProducts();
            return _mapper.ProjectTo<ProductDto>(products);
        }
        public async Task<IEnumerable<ProductDto>> GetProductsPagedAsync(int pageNumber, int pageSize, NutritionRange range, bool? ascendingSorting)
        {
            var products = await _repository.GetProductsPagedAsync(pageNumber, pageSize, range, ascendingSorting);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
        public async Task<ProductDto> AddProductAsync(CreateProductDto newProduct, string productPhotoPath)
        {
            var product = _mapper.Map<Product>(newProduct);
            product.ProductPhotoPath = productPhotoPath;
            await _repository.AddProductAsync(product);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task UpdateProductAsync(UpdateProductDto updatedProduct, Guid id, string productPhotoPath)
        {
            var existingProduct = await _repository.GetProductByIdAsync(id);
            var product = _mapper.Map(updatedProduct, existingProduct);
            product.ProductPhotoPath = productPhotoPath;
            await _repository.UpdateProductAsync(product);
        }
        public async Task PatchProductAsync(JsonPatchDocument patchedProduct, Guid id)
        {
            await _repository.PatchProductAsync(patchedProduct, id); 
        }
        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            await _repository.DeleteProductAsync(product);
        }
        public async Task<int> CountProductsAsync()
        {
            return await _repository.CountProductsAsync();
        }
        public async Task<string> GetPathOfProductImage(Guid id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            return product.ProductPhotoPath;
        }
    }
}
