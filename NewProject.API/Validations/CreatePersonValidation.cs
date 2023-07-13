using FluentValidation;
using NewProject.Application;
using NewProject.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.API.Validations
{
    public class CreatePersonValidation:AbstractValidator<CreatePersonInput>
    {
        private readonly IPersonManager _personManager;
        public CreatePersonValidation(IPersonManager personManager)
        {
            _personManager = personManager;
            RuleFor(u=>u.PhoneNumber).NotNull().NotEmpty().WithMessage(Constanties.PhoneNumber_REQUIRED);
           RuleFor(u=>u.FullName).NotNull().NotEmpty().WithMessage(Constanties.FULLNAME_REQUIRED);
           RuleFor(u=>u.PasswordHash).NotNull().NotEmpty().WithMessage(Constanties.PASSWORD_REQUIRED)
               .MinimumLength(5);
           RuleFor(u=>u.Email).NotNull().NotEmpty().WithMessage(Constanties.EMAIL_REQUIRED).EmailAddress()
               .Must(CheckEmailIsExist).WithMessage("This Email Is Already Exist!");
           
           
        }

        private bool CheckEmailIsExist(string email)
        {
            return !_personManager.IsExisted(p => p.Email == email.ToLower());
        }
    }
}
