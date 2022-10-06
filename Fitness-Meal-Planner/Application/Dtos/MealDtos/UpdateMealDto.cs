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
        public string name { get; init; }
        public int weightInGrams { get; init; }
        public decimal calories { get; init; }
        public decimal protein { get; init; }
        public decimal carbohydrates { get; init; }
        public decimal fat { get; init; }
        public List<Ingredient>? ingredientsList { get; init; }
        public string recipe { get; init; }
        public IFormFile? image { get; init; }
    }
}
