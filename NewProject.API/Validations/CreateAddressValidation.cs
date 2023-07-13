using FluentValidation;
using NewProject.Application;
using NewProject.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewProject.Application.Filters;

namespace NewProject.API.Validations
{
    public class CreateAddressValidation: AbstractValidator<CreateAddressInput>
    {
        public CreateAddressValidation()
        {
            RuleFor(u=>u.address).NotNull().NotEmpty().WithMessage(Constanties.ADDRESS_REQUIRED);
            RuleFor(u=>u.City).NotNull().NotEmpty().WithMessage(Constanties.CITY_REQUIRED);
        }
    }
}
