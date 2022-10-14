using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(40);
            RuleFor(x => x.WeightInGrams).NotEmpty();
            RuleFor(x => x.Calories).NotEmpty();
            RuleFor(x => x.Protein).NotEmpty();
            RuleFor(x => x.Carbohydrates).NotEmpty();
            RuleFor(x => x.Fat).NotEmpty();
            RuleFor(x => x.Ingredients).MaximumLength(500);
            RuleFor(x => x.Description).MaximumLength(500);
        }
    }
}
