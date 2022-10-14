using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators
{
    public class MealValidator : AbstractValidator<Meal>
    {
        public MealValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.WeightInGrams).NotEmpty();
            RuleFor(x => x.Calories).NotEmpty();
            RuleFor(x => x.Protein).NotEmpty();
            RuleFor(x => x.Carbohydrates).NotEmpty();
            RuleFor(x => x.Fat).NotEmpty();
            RuleFor(x => x.Ingredients).NotEmpty().MaximumLength(1000);
            RuleFor(x => x.Recipe).NotEmpty().MaximumLength(1000);
        }
    }
}
