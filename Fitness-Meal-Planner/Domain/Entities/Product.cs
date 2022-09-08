using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public record Product
    {
        [Key]
        public Guid id { get; init; }
        [Required]
        public int weightInGrams { get; init; }
        [Required]
        public decimal calories { get; init; }
        [Required]
        public decimal protein { get; init; }
        [Required]
        public decimal carbohydrates { get; init; }
        [Required]
        public decimal fat { get; set; }
        [MaxLength(1000)]
        public string ingredients { get; set; }
        [MaxLength(500)]
        public string description { get; set; }
    }
}
