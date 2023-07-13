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
    public class AddressFilterValidation: AbstractValidator<AddressFilter>
    {
        public AddressFilterValidation()
        {
            RuleFor(u=>u.Address).NotNull().NotEmpty().WithMessage(Constanties.ADDRESS_REQUIRED);
            RuleFor(u=>u.City).NotNull().NotEmpty().WithMessage(Constanties.CITY_REQUIRED);
            RuleFor(u=>u.Take).NotNull().NotEmpty().WithMessage(Constanties.TASK_VALUE);
        }
    }
}
