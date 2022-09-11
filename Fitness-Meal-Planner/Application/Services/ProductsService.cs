using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
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

        public IEnumerable<ProductDto> GetAllProducts()
        {
            var products = repository.GetAllProducts();
            return mapper.Map<IEnumerable<ProductDto>>(products);
        }
        public void AddProduct(CreateProductDto newProduct)
        {
            var product = mapper.Map<Product>(newProduct);
            repository.AddProduct(product);

        }
    }
}
