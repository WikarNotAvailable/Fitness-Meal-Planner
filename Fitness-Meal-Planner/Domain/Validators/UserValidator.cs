using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Username).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Password).NotEmpty().MaximumLength(20);
            RuleFor(x => x.EmailAddress).EmailAddress();
            RuleFor(x => x.Role).NotEmpty().MaximumLength(20);
        }
    }
}
