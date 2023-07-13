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
using ApiControllerExtensions = NewProject.API.ApiControllerExtensions;

namespace NewProject.WebAPI.Controllers
{
    public class AddressController : AppBaseController
    {
        private readonly IAddressManager _addressManager;

        public AddressController(IAddressManager addressManager)
        {
            _addressManager = addressManager;
        }
        
        [HttpPost]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll([FromForm] FilterModel filterModel)
        {
            var result = await _addressManager.GetAll(null, take: filterModel.Take, skip: 0, address => address.Id, "Persons");
            return this.Accepted(result);
        }
        
        [HttpPost]
        [Route("GetAddressWithPersons")]
        public async Task<IActionResult> GetAddressWithPersons([FromForm] AddressFilter filterModel)
        {
            AddressFilterValidation validator = new AddressFilterValidation();
            ValidationResult ValidationResult = validator.Validate(filterModel);
            if (!ValidationResult.IsValid)
            {
                return this.ValidationErrors(ValidationResult.Errors);
            }
            
            Expression<Func<Address, bool>> whereExpression = person => (
                person.address.Contains(filterModel.Address.Trim(), StringComparison.InvariantCultureIgnoreCase)
                || person.City.Contains(filterModel.City.Trim(), StringComparison.InvariantCultureIgnoreCase));
            
            var result = await _addressManager.GetAll(whereExpression, take: filterModel.Take, skip: 0, person => person.Id, "Persons");
            return this.Accepted(result);
        }
        
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromForm] CreateAddressInput model)
        {
            CreateAddressValidation validator = new CreateAddressValidation();
            ValidationResult ValidationResult = validator.Validate(model);
            if (!ValidationResult.IsValid)
            {
                return this.ValidationErrors(ValidationResult.Errors);
            }
            
            var result = await _addressManager.AddAsync(model);
            
            return result is not null? this.AppSuccess(result) :this.AppNotFound();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Guid id ,[FromHeader] CreateAddressInput model)
        {
            CreateAddressValidation validator = new CreateAddressValidation();
            ValidationResult ValidationResult = validator.Validate(model);
            if (!ValidationResult.IsValid)
            {
                return this.ValidationErrors(ValidationResult.Errors);
            }
            
            var result = await _addressManager.Update(model, id);
            
            if (result.Id == null)
                return this.AppNotFound();
            return this.AppSuccess(result);
        }
        
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var e = await _addressManager.GetById(id);
            
            if (e is null)
            {
                return this.AppNotFound();
            }
            var result = await _addressManager.Remove(id);
            return result ? this.AppSuccess( message: Constanties.SUCCESS_DELETED, data: e): this.AppDeleteFailed();
        }
        
    }
}
