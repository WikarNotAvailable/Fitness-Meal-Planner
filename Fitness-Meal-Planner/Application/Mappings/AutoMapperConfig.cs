using Application.Dtos;
using AutoMapper;
using Domain.Additional;
using Domain.Entities;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Application.Mappings
{
    //class for the configuration of automapper
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<CreateProductDto, Product>().ConstructUsing(x => new Product(x.name, x.weightInGrams, x.calories, x.protein,
                    x.carbohydrates, x.fat, x.ingredients, x.description));
                cfg.CreateMap<UpdateProductDto, Product>();
                cfg.CreateMap<Meal, MealDto>().ConstructUsing(x => new MealDto(x.id, x.name, x.weightInGrams, x.calories, x.protein, x.carbohydrates,
                    x.fat, IngredientsConverter.stringToList(x.ingredients), x.recipe)); 
                cfg.CreateMap<CreateMealDto, Meal>().ConstructUsing(x => new Meal(x.name, x.weightInGrams, x.calories, x.protein, x.carbohydrates,
                    x.fat, x.ingredientsList, x.recipe));
                cfg.CreateMap<UpdateMealDto, Meal>().ForMember(dest => dest.ingredients, 
                    opt => opt.MapFrom(src => IngredientsConverter.listToString(src.ingredientsList)));
            })
            .CreateMapper();
    }
}
