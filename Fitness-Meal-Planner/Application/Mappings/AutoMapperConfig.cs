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
using Application.Dtos.MealDtos;
using Application.Dtos.ProductDtos;
using Application.Dtos.UserDtos;

namespace Application.Mappings
{
    //class for the configuration of automapper
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<CreateProductDto, Product>().ConstructUsing(x => new Product(x.Name, x.WeightInGrams, x.Calories, x.Protein,
                    x.Carbohydrates, x.Fat, x.Ingredients, x.Description));
                cfg.CreateMap<UpdateProductDto, Product>();
                cfg.CreateMap<Meal, MealDto>().ConstructUsing(x => new MealDto(x.Id, x.Name, x.WeightInGrams, x.Calories, x.Protein, x.Carbohydrates,
                    x.Fat, IngredientsConverter.stringToList(x.Ingredients), x.Recipe, x.MealPhotoPath)); 
                cfg.CreateMap<CreateMealDto, Meal>().ConstructUsing(x => new Meal(x.Name, x.WeightInGrams, x.Calories, x.Protein, x.Carbohydrates,
                    x.Fat, x.IngredientsList, x.Recipe));
                cfg.CreateMap<UpdateMealDto, Meal>().ForMember(dest => dest.Ingredients, 
                    opt => opt.MapFrom(src => IngredientsConverter.listToString(src.IngredientsList)));
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<RegisterDto, User>();
                cfg.CreateMap<User, User>();
            })
            .CreateMapper();
    }
}
