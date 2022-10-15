using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.MealDtos
{
    public record MealDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public int WeightInGrams { get; init; }
        public decimal Calories { get; init; }
        public decimal Protein { get; init; }
        public decimal Carbohydrates { get; init; }
        public decimal Fat { get; init; }
        public List<Ingredient>? IngredientsList { get; init; }
        public string Recipe { get; init; }
        public string MealPhotoPath { get; init; }
        public MealDto(Guid id, string name, int weightInGrams, decimal calories, decimal protein,
           decimal carbohydrates, decimal fat, List<Ingredient>? ingredientsList, string recipe, string mealPhotoPath)
        {
            (Id, Name, WeightInGrams, Calories, Protein, Carbohydrates, Fat, IngredientsList, Recipe, MealPhotoPath) = (id, name,
                weightInGrams, calories, protein, carbohydrates, fat, ingredientsList, recipe, mealPhotoPath);
        }
    }
}
