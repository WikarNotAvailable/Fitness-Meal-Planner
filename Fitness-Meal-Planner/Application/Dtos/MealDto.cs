using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public record MealDto
    {
        public Guid id { get; init; }
        public string name { get; init; }
        public int weightInGrams { get; init; }
        public decimal calories { get; init; }
        public decimal protein { get; init; }
        public decimal carbohydrates { get; init; }
        public decimal fat { get; init; }
        public List<Ingredient>? ingredientsList { get; init; }
        public string recipe { get; init; }
        public MealDto(Guid _id,string _name, int _weightInGrams, decimal _calories, decimal _protein,
           decimal _carbohydrates, decimal _fat, List<Ingredient>? _ingredientsList, string _recipe)
        {
            (id, name, weightInGrams, calories, protein, carbohydrates, fat, ingredientsList, recipe) = (_id,_name,
                _weightInGrams, _calories, _protein, _carbohydrates, _fat, _ingredientsList, _recipe);
        }
    }
}
