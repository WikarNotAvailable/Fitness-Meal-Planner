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
        [Required]
        [MaxLength(50)]
        public string Name { get; init; }
        [Required]
        public int WeightInGrams { get; init; }
        [Required]
        public decimal Calories { get; init; }
        [Required]
        public decimal Protein { get; init; }
        [Required]
        public decimal Carbohydrates { get; init; }
        [Required]
        public decimal Fat { get; init; }
        [Required]
        [MaxLength(500)]
        public string Ingredients { get; set; } 
        [Required]
        [MaxLength(1000)]
        public string Recipe { get; init; }
        public string MealPhotoPath { get; set; }
        public Meal() { }
        public Meal(string name, int weightInGrams, decimal calories, decimal protein,
            decimal carbohydrates, decimal fat, List<Ingredient>?  ingredientsList, string recipe) 
        {
            Id = Guid.NewGuid();
            Ingredients = IngredientsConverter.listToString(ingredientsList);
            (Name, WeightInGrams, Calories, Protein, Carbohydrates, Fat, Recipe) = (name,
                weightInGrams, calories, protein, carbohydrates, fat, recipe);
        }
    }
}
