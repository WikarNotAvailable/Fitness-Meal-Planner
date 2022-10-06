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
        private readonly IProductsRepository repository;
        private readonly IMapper mapper;
        public ProductsService(IProductsRepository _repository, IMapper _mapper)
        {
            repository = _repository;
            mapper = _mapper;
        }

        public IQueryable<ProductDto> GetAllProducts()
        {
            var products = repository.GetAllProducts();
            return mapper.ProjectTo<ProductDto>(products);
        }
        public async Task<IEnumerable<ProductDto>> GetProductsPagedAsync(int pageNumber, int pageSize, NutritionRange range, bool? ascendingSorting)
        {
            var products = await repository.GetProductsPagedAsync(pageNumber, pageSize, range, ascendingSorting);
            return mapper.Map<IEnumerable<ProductDto>>(products);
        }
        public async Task<ProductDto> AddProductAsync(CreateProductDto newProduct, string _productPhotoPath)
        {
            var product = mapper.Map<Product>(newProduct);
            product.productPhotoPath = _productPhotoPath;
            await repository.AddProductAsync(product);
            return mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var product = await repository.GetProductByIdAsync(id);
            return mapper.Map<ProductDto>(product);
        }

        public async Task UpdateProductAsync(UpdateProductDto updatedProduct, Guid id, string _productPhotoPath)
        {
            var existingProduct = await repository.GetProductByIdAsync(id);
            var product = mapper.Map(updatedProduct, existingProduct);
            product.productPhotoPath = _productPhotoPath;
            await repository.UpdateProductAsync(product);
        }
        public async Task PatchProductAsync(JsonPatchDocument patchedProduct, Guid id)
        {
            await repository.PatchProductAsync(patchedProduct, id); 
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

        public async Task<string> GetPathOfProductImage(Guid id)
        {
            var product = await repository.GetProductByIdAsync(id);
            return product.productPhotoPath;
        }

    }
}
