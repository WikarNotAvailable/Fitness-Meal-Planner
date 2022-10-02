using Domain.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Additional
{
    //class used to convert list of ingredients to string and string to list of ingredients
    public static class IngredientsConverter
    {
        public static string listToString(List<Ingredient>? ingredientsList) 
        {
            string ingredients = "";
            if (ingredientsList == null)
            {
                return ingredients;
            }
            foreach (var ingredient in ingredientsList)
            {
                ingredients += ingredient.name + "=" + ingredient.weight + ",";
            }
            return ingredients.Remove(ingredients.Length - 1);
        }
        public static List<Ingredient> stringToList(string ingredients) 
        {
            List<Ingredient> list = new List<Ingredient>();
            if (ingredients == "")
            {
                return list;
            }
            string[] ingredientsListString = ingredients.Split(",".ToCharArray());

            foreach (var ingredient in ingredientsListString)
            {
                string[] dividedIngredient = ingredient.Split("=".ToCharArray());
                list.Add(new Ingredient
                {
                    name = dividedIngredient[0],
                    weight = Int32.Parse(dividedIngredient[1])
                });
            }
            return list;
        }
    }
}
