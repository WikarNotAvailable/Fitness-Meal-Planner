﻿using Domain.Common;
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
        public Guid id { get; init; }
        [Required]
        [MaxLength(50)]
        public string name { get; init; }
        [Required]
        public int weightInGrams { get; init; }
        [Required]
        public decimal calories { get; init; }
        [Required]
        public decimal protein { get; init; }
        [Required]
        public decimal carbohydrates { get; init; }
        [Required]
        public decimal fat { get; init; }
        [Required]
        [MaxLength(500)]
        public string ingredients { get; set; } 
        [Required]
        [MaxLength(1000)]
        public string recipe { get; init; }
        public string mealPhotoPath { get; set; }
        public Meal() { }
        public Meal(string _name, int _weightInGrams, decimal _calories, decimal _protein,
            decimal _carbohydrates, decimal _fat, List<Ingredient>?  _ingredientsList, string _recipe) 
        {
            id = Guid.NewGuid();
            ingredients = IngredientsConverter.listToString(_ingredientsList);
            (name, weightInGrams, calories, protein, carbohydrates, fat, recipe) = (_name,
                _weightInGrams, _calories, _protein, _carbohydrates, _fat, _recipe);
        }
    }
}
