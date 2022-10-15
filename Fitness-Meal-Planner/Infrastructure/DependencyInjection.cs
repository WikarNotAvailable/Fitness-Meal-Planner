using Domain.Entities;
using Domain.Interfaces;
using Domain.Validators;
using FluentValidation;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IProductsRepository, SQLProductsRepository>();
            services.AddScoped<IMealsRepository, SQLMealsRepository>();
            services.AddScoped<IUsersRepository, SQLUsersRepository>();

            services.AddScoped<IValidator<Meal>, MealValidator>();
            services.AddScoped<IValidator<User>, UserValidator>();
            services.AddScoped<IValidator<Product>, ProductValidator>();

            return services;
        }
    }
}
