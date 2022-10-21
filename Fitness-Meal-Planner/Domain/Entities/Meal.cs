using Domain.Common;
using Domain.Additional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Domain.Entities
{
    //table containing meals

    [Table("Meals")]
    public record Meal : AuditableEntity
    {
        [Key]
        public Guid Id { get; init; }
        public string Name { get; init; }
        public int WeightInGrams { get; init; }
        public decimal Calories { get; init; }
        public decimal Protein { get; init; }
        public decimal Carbohydrates { get; init; }
        public decimal Fat { get; init; }
        public string Ingredients { get; set; } 
        public string Recipe { get; init; }
        public string MealPhotoPath { get; set; }
        public Meal(string name, int weightInGrams, decimal calories, decimal protein,
            decimal carbohydrates, decimal fat, List<Ingredient>?  ingredientsList, string recipe) 
        {
            Id = Guid.NewGuid();
            Ingredients = IngredientsConverter.listToString(ingredientsList);
            (Name, WeightInGrams, Calories, Protein, Carbohydrates, Fat, Recipe) = (name,
                weightInGrams, calories, protein, carbohydrates, fat, recipe);
        }

       public Meal()
        {
        }
    }
}
