using NewProject.Domain;
using NewProject.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using NewProject.Domain.BaseEntities;
using System.Linq.Expressions;

namespace NewProject.Application;

public interface IAddressManager : ICrudGenericManager<Guid, Address, AddressDto, CreateAddressInput>
{
    public IEnumerable<AddressDto> GetAddressInCity();
}
