using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities
{
    [Table("Meals")]
    public record Meal : AuditableEntity
    {
        [Key]
        public Guid id { get; init; }
        [Required]
        [MaxLength(50)]
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
        [Required]
        [MaxLength(500)]
        public string ingredients { get init; }

    }
}
