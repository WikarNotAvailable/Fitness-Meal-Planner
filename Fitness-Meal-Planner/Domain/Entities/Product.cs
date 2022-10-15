using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    //table containing products
    [Table("Products")]
    public record Product : AuditableEntity
    {
        [Key]
        public Guid Id { get; init; }
        public string Name { get; init; }
        public int WeightInGrams { get; init; }
        public decimal Calories { get; init; }
        public decimal Protein { get; init; }
        public decimal Carbohydrates { get; init; }
        public decimal Fat { get; init; }
        public string Ingredients { get; init; }
        public string Description { get; init; }
        public string ProductPhotoPath { get; set; }
        public Product() {}
        public Product(string name, int weight, decimal calories, decimal protein, 
            decimal carbohydrates, decimal fat, string ingredients, string description) 
        {
            Id = Guid.NewGuid();
            (Name, WeightInGrams, Calories, Protein, Carbohydrates, Fat, Ingredients, Description) = (name, weight, calories, protein,
                carbohydrates, fat, ingredients, description);
        }
    }
}
