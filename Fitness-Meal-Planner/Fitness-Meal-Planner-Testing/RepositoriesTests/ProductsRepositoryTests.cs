using Application.Dtos.ProductDtos;
using Application.Mappings;
using Domain.Additional_Structures;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Meal_Planner_Testing.RepositoriesTests
{
    public class ProductsRepositoryTests
    {
        private async Task<FitnessPlannerContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<FitnessPlannerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new FitnessPlannerContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Products.CountAsync() <= 0)
            {
                databaseContext.Products.Add(
                new Product()
                {
                    Id = Guid.Parse("08754fc3-9fad-4081-a738-c608f8a95973"),
                    Name = "Chicken" + 1,
                    WeightInGrams = 1,
                    Calories = 1,
                    Protein = 1,
                    Carbohydrates = 1,
                    Fat = 1,
                    Ingredients = "meat 5g",
                    Description = "this is tasty chicken",
                    ProductPhotoPath = ""
                });
                databaseContext.Products.Add(
                new Product()
                {
                    Id = Guid.Parse("08754fc3-9fad-4081-a738-c608f8a95974"),
                    Name = "Chicken" + 2,
                    WeightInGrams = 2,
                    Calories = 2,
                    Protein = 2,
                    Carbohydrates = 2,
                    Fat = 2,
                    Ingredients = "meat 5g",
                    Description = "this is tasty chicken",
                    ProductPhotoPath = ""
                });
                databaseContext.Products.Add(
                new Product()
                {
                    Id = Guid.Parse("08754fc3-9fad-4081-a738-c608f8a95975"),
                    Name = "Chicken" + 3,
                    WeightInGrams = 3,
                    Calories = 3,
                    Protein = 3,
                    Carbohydrates = 3,
                    Fat = 3,
                    Ingredients = "meat 5g",
                    Description = "this is tasty chicken",
                    ProductPhotoPath = ""
                });
                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }
        [Fact]
        public async void ProductsRepository_GetAllProducts_ReturnsProducts()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var repository = new SQLProductsRepository(dbContext);

            //Act
            var result = repository.GetAllProducts();

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
        }
        [Fact]
        public async void ProductsRepository_GetProductByID_ReturnsProduct()
        {
            //Arrange
            var id = Guid.Parse("08754fc3-9fad-4081-a738-c608f8a95973");
            var dbContext = await GetDatabaseContext();
            var repository = new SQLProductsRepository(dbContext);

            //Act
            var result = await repository.GetProductByIdAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Product>();
        }
        [Fact]
        public async void ProductsRepository_GetProductsPagedAsync_ReturnsProductsFiltered()
        {
            //Arrange
            var pageNumber = 1;
            var pageSize = 3;
            var nutritionRange = new NutritionRange(2, 2, 2, 2, 2, 2, 2, 2, "");
            bool? ascendingSort = null;
            var dbContext = await GetDatabaseContext();
            var repository = new SQLProductsRepository(dbContext);

            //Act
            var result = await repository.GetProductsPagedAsync(pageNumber, pageSize, nutritionRange, ascendingSort);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Product>>();
            result.Should().HaveCount(1);
        }
        [Fact]
        public async void ProductsRepository_GetProductsPagedAsync_ReturnsFirstTwoProducts()
        {
            //Arrange
            var pageNumber = 1;
            var pageSize = 2;
            var nutritionRange = new NutritionRange(0, 999, 0, 999, 0, 999, 0, 999, "");
            bool? ascendingSort = null;
            var dbContext = await GetDatabaseContext();
            var repository = new SQLProductsRepository(dbContext);

            //Act
            var result = await repository.GetProductsPagedAsync(pageNumber, pageSize, nutritionRange, ascendingSort);

            //Assert
            var product = result.Where(m => m.Id == new Guid("08754fc3-9fad-4081-a738-c608f8a95973"));
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Product>>();
            result.Should().HaveCount(2);
            product.Should().NotBeNull();
        }
        [Fact]
        public async void ProductsRepository_GetProductsPagedAsync_ReturnsProductsSorted()
        {
            //Arrange
            var pageNumber = 1;
            var pageSize = 3;
            var nutritionRange = new NutritionRange(0, 999, 0, 999, 0, 999, 0, 999, "");
            bool? ascendingSort = false;
            var dbContext = await GetDatabaseContext();
            var repository = new SQLProductsRepository(dbContext);

            //Act
            var result = await repository.GetProductsPagedAsync(pageNumber, pageSize, nutritionRange, ascendingSort);

            //Assert
            var product = result.First();
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Product>>();
            result.Should().HaveCount(3);
            product.Should().NotBeNull();
            product.Id.Equals(new Guid("08754fc3-9fad-4081-a738-c608f8a95975"));
        }
        [Fact]
        public async void ProductsRepository_AddNewProductAsync_AddsNewProduct()
        {
            //Arrange
            var product = new Product()
            {
                Id = Guid.Parse("08754fc3-9fad-4081-a738-c608f8a95976"),
                Name = "Chicken" + 4,
                WeightInGrams = 4,
                Calories = 4,
                Protein = 4,
                Carbohydrates = 4,
                Fat = 4,
                Ingredients = "meat 5g",
                Description = "product",
                ProductPhotoPath = ""
            };
            var nutritionRange = new NutritionRange(0, 999, 0, 999, 0, 999, 0, 999, "");
            bool? ascendingSort = null;
            var dbContext = await GetDatabaseContext();
            var repository = new SQLProductsRepository(dbContext);

            //Act
            await repository.AddProductAsync(product);
            var result = await repository.GetProductsPagedAsync(1, 4, nutritionRange, ascendingSort);

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(4);
            result.Should().BeOfType<List<Product>>();
        }
        [Fact]
        public async void ProductsRepository_UpdateProductAsync_UpdatesProduct()
        {
            //Arrange
            var productProperties = new UpdateProductDto()
            {
                Name = "Chicken" + 4,
                WeightInGrams = 4,
                Calories = 4,
                Protein = 4,
                Carbohydrates = 4,
                Fat = 4,
                Description = "product",
            };
            var nutritionRange = new NutritionRange(0, 999, 0, 999, 0, 999, 0, 999, "");
            bool? ascendingSort = null;
            var mapper = AutoMapperConfig.Initialize();
            var dbContext = await GetDatabaseContext();
            var repository = new SQLProductsRepository(dbContext);

            var oldProduct = await repository.GetProductByIdAsync(Guid.Parse("08754fc3-9fad-4081-a738-c608f8a95973"));
            var product = mapper.Map(productProperties, oldProduct);

            //Act
            await repository.UpdateProductAsync(product);
            var result = await repository.GetProductsPagedAsync(1, 3, nutritionRange, ascendingSort);

            //Assert
            var productAfterUpdate = result.SingleOrDefault(m => m.Id == Guid.Parse("08754fc3-9fad-4081-a738-c608f8a95973"));
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            result.Should().BeOfType<List<Product>>();
            productAfterUpdate.Name.Equals("Chicken4");
        }
        [Fact]
        public async void ProducsRepository_DeleteProductAsync_DeletesProduct()
        {
            //Arrange
            var nutritionRange = new NutritionRange(0, 999, 0, 999, 0, 999, 0, 999, "");
            bool? ascendingSort = null;
            var dbContext = await GetDatabaseContext();
            var repository = new SQLProductsRepository(dbContext);

            var product = await repository.GetProductByIdAsync(Guid.Parse("08754fc3-9fad-4081-a738-c608f8a95973"));

            //Act
            await repository.DeleteProductAsync(product);
            var result = await repository.GetProductsPagedAsync(1, 3, nutritionRange, ascendingSort);

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeOfType<List<Product>>();
        }
        [Fact]
        public async void ProductsRepository_CountProductsAsync_ReturnsCountOfProducts()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var repository = new SQLProductsRepository(dbContext);

            //Act
            var result = await repository.CountProductsAsync();

            //Assert
            result.Equals(3);
            result.Should().BeOfType(typeof(int));
        }
    }
}
