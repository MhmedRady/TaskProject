using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using NewProject.Application;
using NewProject.WebAPI.Extensions;

using NewProject.Shared;
using NewProject.API.Validations;
using FluentValidation.Results;
using NewProject.Application.Filters;
using NewProject.Application.Managers;
using NewProject.Domain;

namespace NewProject.WebAPI.Controllers
{
    public class PersonController : AppBaseController
    {
        private readonly IPersonManager _userManager;

        public PersonController(IPersonManager userManager)
        {
            _userManager = userManager;
        }
        
        [HttpPost]
        [Route("GetAllPersons")]
        public async Task<IActionResult> GetAll([FromForm] PersonFilter filterModel)
        {
            
            PersonFilterValidation validator = new PersonFilterValidation();
            ValidationResult ValidationResult = validator.Validate(filterModel);
            if (!ValidationResult.IsValid)
            {
                return this.ValidationErrors(ValidationResult.Errors);
            }
            Expression<Func<Person, bool>> whereExpression = person => (
                   person.Email.StartsWith(filterModel.Email??"")
                || person.PhoneNumber.StartsWith(filterModel.PhoneNumber??"")
                || person.FullName.Contains(filterModel.Fullname??""));
            
            var result = await _userManager.GetAll(whereExpression, take: filterModel.Take, null, person => person.Id, "Address");
            
            return this.Accepted(result);
        }
        
        [HttpPost]
        [Route("GetPerosnWithAddress")]
        public async Task<IActionResult> GetAll([FromForm] PersonWithAddressFilter filterModel)
        {
            PersonWithAddressFilterValidation validator = new PersonWithAddressFilterValidation();
            ValidationResult ValidationResult = validator.Validate(filterModel);
            if (!ValidationResult.IsValid)
            {
                return this.ValidationErrors(ValidationResult.Errors);
            }
            Expression<Func<Person, bool>> whereExpression = person => (
                person.Email.Contains(filterModel.Email.Trim(),
                    StringComparison.InvariantCultureIgnoreCase)
                || person.PhoneNumber.Contains(filterModel.Fullname.Trim(),
                    StringComparison.InvariantCultureIgnoreCase)
                || person.Address.address.Contains(filterModel.Address.Trim(),
                    StringComparison.InvariantCultureIgnoreCase));
            var result = await _userManager.GetAll(whereExpression, take: filterModel.Take, 0, person => person.Id, "Address");
            return this.Accepted(result);
        }

        [HttpPost]
        [Route("Regeste")]
        public async Task<IActionResult> Add([FromForm] CreatePersonInput model)
        {
            CreatePersonValidation validator = new CreatePersonValidation(_userManager);
            ValidationResult ValidationResult = validator.Validate(model);
            if (!ValidationResult.IsValid)
            {
                return this.ValidationErrors(ValidationResult.Errors);
            }
            
            var result = await _userManager.AddAsync(model);
            
            if (result.Id == null)
                return this.AppFailed(new { status = false, data = result ,Message = "An error"});
            return this.AppSuccess(result);
        }

        [HttpGet]
        [Authorize]
        [Route("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            var result = _userManager.GetBy(u => u.Id == UserId);
            return result is not null ? this.AppSuccess(result) : this.AppNotFound(msg: "Sorry This User Is not Exist!");
        }
        [HttpPut]
        [Authorize]
        [Route("UpdateProfile")]
        public async Task<IActionResult> UpdateUserProfile(UserProfileDto model)
        {
            UserProfileValidation validator = new UserProfileValidation();
            ValidationResult ValidationResult = validator.Validate(model);
            if (!ValidationResult.IsValid)
            {
                return this.ValidationErrors(ValidationResult.Errors);
            }
            var loggedInUser = _userManager.IsExisted(user => user.Email == model.Email && user.Id != UserId);
            
            if (!loggedInUser)
            {
                return this.AppFailed(message: Constanties.USER_NOT_EXIST);
            }
            else
            {
                var user = _userManager.GetBy(u => u.Email == model.Email);
                var createUser = new CreatePersonInput();
                createUser.FullName = model.FullName;
                createUser.PhoneNumber = user.PhoneNumber;
                
                var result = await _userManager.Update(createUser, user.Id);
                return this.AppSuccess(result);
            }
            
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromForm] LoginDto model)
        {
            UserLoginValidation validator = new UserLoginValidation();
            ValidationResult ValidationResult = validator.Validate(model);
            if (!ValidationResult.IsValid)
            {
                return this.ValidationErrors(ValidationResult.Errors);
            }
            var isExist = _userManager.IsExisted(p => p.Email == model.Email);
            if (!isExist)
            {
                this.AppFailed(message: Constanties.USERLOGINERROR);
            }
            
            var result = await _userManager.Login(model);

            return result.UserId is not null ? this.Accepted(result) : this.AppFailed(message: Constanties.USERLOGINERROR);
        }
        [HttpPost]
        [Authorize]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordDto model)
        {
            var result = await _userManager.ChangePasswordAsync(UserId, model.CurrentPassword, model.NewPassword);
            return Ok(result);
        }

    }
}
