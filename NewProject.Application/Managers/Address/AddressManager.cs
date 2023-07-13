using AutoMapper;

using NewProject.Domain;
using NewProject.Shared;

using System.Linq.Expressions;
using System.Text;

using Microsoft.AspNetCore.Identity;
using NewProject.Repositories;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace NewProject.Application;

public class AddressManager : CrudGenericManager<Guid, Address, AddressDto, CreateAddressInput>, IAddressManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public AddressManager(IUnitOfWork unitOfWork, IMapper mapper): base(unitOfWork.AddressRepo, mapper)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<AddressDto> GetAddressInCity()
    {
        return _mapper.Map<IEnumerable<AddressDto>>(_unitOfWork.AddressRepo.GetAddressInCity());
    }
}


