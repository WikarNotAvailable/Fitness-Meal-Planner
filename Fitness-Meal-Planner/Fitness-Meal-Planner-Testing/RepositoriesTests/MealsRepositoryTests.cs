using Application.Dtos.MealDtos;
using Application.Mappings;
using Domain.Additional_Structures;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Repositories;
using k8s.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Meal_Planner_Testing.RepositoriesTests
{
    public class MealsRepositoryTests
    {
        private async Task<FitnessPlannerContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<FitnessPlannerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new FitnessPlannerContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Meals.CountAsync() <= 0)
            {
                    databaseContext.Meals.Add(
                    new Meal()
                    {
                        Id = Guid.Parse("08754fc3-9fad-4081-a738-c608f8a95973"),
                        Name = "Chicken" + 1,
                        WeightInGrams = 1,
                        Calories = 1,
                        Protein = 1,
                        Carbohydrates = 1,
                        Fat = 1,
                        Ingredients = "meat=5,",
                        Recipe = "do the meal",
                        MealPhotoPath = ""
                    }); 
                    databaseContext.Meals.Add(
                    new Meal()
                    {
                        Id = Guid.Parse("08754fc3-9fad-4081-a738-c608f8a95974"),
                        Name = "Chicken" + 2,
                        WeightInGrams = 2,
                        Calories = 2,
                        Protein = 2,
                        Carbohydrates = 2,
                        Fat = 2,
                        Ingredients = "meat=5,",
                        Recipe = "do the meal",
                        MealPhotoPath = ""
                    }); 

                    databaseContext.Meals.Add(
                    new Meal()
                    {
                        Id = Guid.Parse("08754fc3-9fad-4081-a738-c608f8a95975"),
                        Name = "Chicken" + 3,
                        WeightInGrams = 3,
                        Calories = 3,
                        Protein = 3,
                        Carbohydrates = 3,
                        Fat = 3,
                        Ingredients = "meat=5,",
                        Recipe = "do the meal",
                        MealPhotoPath = ""
                    });
            await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }
        [Fact]
        public async void MealsRepository_GetAllMeals_ReturnsMeals()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var repository = new SQLMealsRepository(dbContext);

            //Act
            var result = repository.GetAllMeals();

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
        }
        [Fact]
        public async void MealsRepository_GetMealByID_ReturnsMeal()
        {
            //Arrange
            var id = Guid.Parse("08754fc3-9fad-4081-a738-c608f8a95973");
            var dbContext = await GetDatabaseContext();
            var repository = new SQLMealsRepository(dbContext);

            //Act
            var result = await repository.GetMealByIdAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Meal>();
        }
        
        [Fact]
        public async void MealsRepository_GetMealsPagedAsync_ReturnsMealsFiltered()
        {
            //Arrange
            var pageNumber = 1;
            var pageSize = 3;
            var nutritionRange = new NutritionRange(2,2,2,2,2,2,2,2,"");
            bool? ascendingSort = null;
            var dbContext = await GetDatabaseContext();
            var repository = new SQLMealsRepository(dbContext);

            //Act
            var result = await repository.GetMealsPagedAsync(pageNumber, pageSize, nutritionRange, ascendingSort);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Meal>>();
            result.Should().HaveCount(1);
        }
        [Fact]
        public async void MealsRepository_GetMealsPagedAsync_ReturnsFirstTwoMeals()
        {
            //Arrange
            var pageNumber = 1;
            var pageSize = 2;
            var nutritionRange = new NutritionRange(0, 999, 0, 999, 0, 999, 0, 999, "");
            bool? ascendingSort = null;
            var dbContext = await GetDatabaseContext();
            var repository = new SQLMealsRepository(dbContext);

            //Act
            var result = await repository.GetMealsPagedAsync(pageNumber, pageSize, nutritionRange, ascendingSort);

            //Assert
            var meal = result.Where(m => m.Id == new Guid("08754fc3-9fad-4081-a738-c608f8a95973"));
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Meal>>();
            result.Should().HaveCount(2);
            meal.Should().NotBeNull();
        }
        [Fact]
        public async void MealsRepository_GetMealsPagedAsync_ReturnsMealsSorted()
        {
            //Arrange
            var pageNumber = 1;
            var pageSize = 3;
            var nutritionRange = new NutritionRange(0, 999, 0, 999, 0, 999, 0, 999, "");
            bool? ascendingSort = false;
            var dbContext = await GetDatabaseContext();
            var repository = new SQLMealsRepository(dbContext);

            //Act
            var result = await repository.GetMealsPagedAsync(pageNumber, pageSize, nutritionRange, ascendingSort);

            //Assert
            var meal = result.First();
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Meal>>();
            result.Should().HaveCount(3);
            meal.Should().NotBeNull();
            meal.Id.Equals(new Guid("08754fc3-9fad-4081-a738-c608f8a95975"));
        }
        [Fact]
        public async void MealsRepository_AddNewMealAsync_AddsNewMeal()
        {
            //Arrange
            var meal = new Meal()
            {
                Id = Guid.Parse("08754fc3-9fad-4081-a738-c608f8a95976"),
                Name = "Chicken" + 4,
                WeightInGrams = 4,
                Calories = 4,
                Protein = 4,
                Carbohydrates = 4,
                Fat = 4,
                Ingredients = "meat=5,",
                Recipe = "do the meal",
                MealPhotoPath = ""
            };
            var nutritionRange = new NutritionRange(0, 999, 0, 999, 0, 999, 0, 999, "");
            bool? ascendingSort = null;
            var dbContext = await GetDatabaseContext();
            var repository = new SQLMealsRepository(dbContext);

            //Act
            await repository.AddMealAsync(meal);
            var result = await repository.GetMealsPagedAsync(1, 4, nutritionRange, ascendingSort);

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(4);
            result.Should().BeOfType<List<Meal>>();
        }
        [Fact]
        public async void MealsRepository_UpdateMealAsync_UpdatesMeal()
        {
            //Arrange
            var mealProperties = new UpdateMealDto()
            {
                Name = "Chicken" + 4,
                WeightInGrams = 4,
                Calories = 4,
                Protein = 4,
                Carbohydrates = 4,
                Fat = 4,
                Recipe = "do the meal",
            };
            var nutritionRange = new NutritionRange(0, 999, 0, 999, 0, 999, 0, 999, "");
            bool? ascendingSort = null;
            var mapper = AutoMapperConfig.Initialize();
            var dbContext = await GetDatabaseContext();
            var repository = new SQLMealsRepository(dbContext);

            var oldMeal = await repository.GetMealByIdAsync(Guid.Parse("08754fc3-9fad-4081-a738-c608f8a95973"));
            var meal = mapper.Map(mealProperties, oldMeal);
            
            //Act
            await repository.UpdateMealAsync(meal);
            var result = await repository.GetMealsPagedAsync(1, 3, nutritionRange, ascendingSort);

            //Assert
            var mealAfterUpdate = result.SingleOrDefault(m => m.Id == Guid.Parse("08754fc3-9fad-4081-a738-c608f8a95973"));
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            result.Should().BeOfType<List<Meal>>();
            mealAfterUpdate.Name.Equals("Chicken4");
        }
        [Fact]
        public async void MealsRepository_DeleteMealAsync_DeletesMeal()
        {
            //Arrange
            var nutritionRange = new NutritionRange(0, 999, 0, 999, 0, 999, 0, 999, "");
            bool? ascendingSort = null;
            var dbContext = await GetDatabaseContext();
            var repository = new SQLMealsRepository(dbContext);

            var meal = await repository.GetMealByIdAsync(Guid.Parse("08754fc3-9fad-4081-a738-c608f8a95973"));

            //Act
            await repository.DeleteMealAsync(meal);
            var result = await repository.GetMealsPagedAsync(1, 3, nutritionRange, ascendingSort);

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeOfType<List<Meal>>();
        }
        [Fact]
        public async void MealsRepository_CountMealsAsync_ReturnsCountOfMeals()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var repository = new SQLMealsRepository(dbContext);

            //Act
            var result = await repository.CountMealsAsync();

            //Assert
            result.Equals(3);
            result.Should().BeOfType(typeof(int));
        }
    }
}
