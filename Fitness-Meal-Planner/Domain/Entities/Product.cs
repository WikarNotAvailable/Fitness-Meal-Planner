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
        public Guid id { get; init; }
        [Required]
        [MaxLength(100)]
        public string name { get; init; }
        [Required]
        public int weightInGrams { get; init; }
        [Required]
        public decimal calories { get; init; }
        [Required]
        public decimal protein { get; init; }
        [Required]
        public decimal carbohydrates { get; init; }
        [Required]
        public decimal fat { get; init; }
        [MaxLength(1000)]
        public string ingredients { get; init; }
        [MaxLength(500)]
        public string description { get; init; }
        public string productPhotoPath { get; set; }
        public Product() {}
        public Product(string _name, int _weight, decimal _calories, decimal _protein, 
            decimal _carbohydrates, decimal _fat, string _ingredients, string _description) 
        {
            id = Guid.NewGuid();
            (name, weightInGrams, calories, protein, carbohydrates, fat, ingredients, description) = (_name, _weight, _calories, _protein,
                _carbohydrates, _fat, _ingredients, _description);
        }
    }
}
