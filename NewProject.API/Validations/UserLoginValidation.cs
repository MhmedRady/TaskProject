using FluentValidation;
using NewProject.Application;
using NewProject.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NewProject.Application.Managers;

namespace NewProject.API.Validations
{
    public class UserLoginValidation : AbstractValidator<LoginDto>
    {
        private readonly IPersonManager PersonManager;
        public UserLoginValidation()
        {
            RuleFor(u=> u.Email).NotNull().WithMessage(Constanties.EMAIL_REQUIRED);
            RuleFor(u=> u.Email).EmailAddress();
            RuleFor(l => l.Password).NotNull().NotEmpty().WithMessage(Constanties.PASSWORD_REQUIRED);

            
        }
    }
}


