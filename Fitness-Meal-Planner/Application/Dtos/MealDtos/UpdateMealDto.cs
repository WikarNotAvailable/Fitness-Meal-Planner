using Domain.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.MealDtos
{
    public record UpdateMealDto
    {
        public string Name { get; init; }
        public int WeightInGrams { get; init; }
        public decimal Calories { get; init; }
        public decimal Protein { get; init; }
        public decimal Carbohydrates { get; init; }
        public decimal Fat { get; init; }
        public List<Ingredient>? IngredientsList { get; init; }
        public string Recipe { get; init; }
        public IFormFile? Image { get; init; }
    }
}
