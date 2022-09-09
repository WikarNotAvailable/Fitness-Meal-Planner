using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    // Dto for products
    public record ProductDto : IMap
    {
        public Guid id { get; init; }
        public string name { get; init; }
        public int weightInGrams { get; init; }
        public decimal calories { get; init; }
        public decimal protein { get; init; }
        public decimal carbohydrates { get; init; }
        public decimal fat { get; init; }
        public string ingredients { get; init; }
        public string description { get; init; }
        public void Mapping (Profile profile)
        {
            profile.CreateMap<Product, ProductDto>();
        }
    }
}
