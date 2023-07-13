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
    public class PersonFilterValidation: AbstractValidator<PersonFilter>
    {
        public PersonFilterValidation()
        {
           RuleFor(u=>u.Email).NotNull().NotEmpty().WithMessage(Constanties.EMAIL_REQUIRED).EmailAddress();
           RuleFor(u=>u.PhoneNumber).NotNull().NotEmpty().WithMessage(Constanties.PhoneNumber_REQUIRED);
           RuleFor(u=>u.Fullname).NotNull().NotEmpty().WithMessage(Constanties.FULLNAME_REQUIRED);
           RuleFor(u=>u.Take).NotNull().NotEmpty().WithMessage(Constanties.TASK_VALUE);
        }
    }
    
    public class PersonWithAddressFilterValidation: AbstractValidator<PersonWithAddressFilter>
    {
        public PersonWithAddressFilterValidation()
        {
            RuleFor(u=>u.Email).NotNull().NotEmpty().WithMessage(Constanties.EMAIL_REQUIRED).EmailAddress();
            RuleFor(u=>u.Address).NotNull().NotEmpty().WithMessage(Constanties.ADDRESS_REQUIRED);
            RuleFor(u=>u.City).NotNull().NotEmpty().WithMessage(Constanties.CITY_REQUIRED);
            RuleFor(u=>u.Take).NotNull().NotEmpty().WithMessage(Constanties.TASK_VALUE);
        }
    }
    
}
