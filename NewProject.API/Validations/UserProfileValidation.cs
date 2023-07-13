using FluentValidation;
using NewProject.Application;
using NewProject.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewProject.Application.Managers;

namespace NewProject.API.Validations
{
    public class UserProfileValidation : AbstractValidator<UserProfileDto>
    {
        public UserProfileValidation()
        {
            RuleFor(u=> u.FullName).NotNull().WithMessage(Constanties.FULLNAME_REQUIRED);
            RuleFor(l => l.PhoneNumber).NotNull().NotEmpty().WithMessage(Constanties.PhoneNumber_REQUIRED);
        }
    }
}


