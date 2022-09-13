using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                cfg.CreateMap<Meal, MealDto>();
                cfg.CreateMap<CreateMealDto, Meal>().ConstructUsing(x => new Meal(x.name, x.weightInGrams, x.calories, x.protein, x.carbohydrates,
                    x.fat, x.ingredients, x.recipe));
            })
            .CreateMapper();
    }
}
