using System.Diagnostics;
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

public class PersonManager : IPersonManager
{
    private readonly UserManager<Person> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly SignInManager<Person> _SignInManager;
    IConfiguration Configuration;
    
    public PersonManager(UserManager<Person> PersonManager, IMapper mapper,
        IUnitOfWork unitOfWork, SignInManager<Person> signInManager,
        IConfiguration configuration, RoleManager<IdentityRole> roleManager)
    {
        _userManager = PersonManager;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _SignInManager = signInManager;
        Configuration = configuration;
        _roleManager = roleManager;
    }

    public PersonDto Add(CreatePersonInput dto)
    {
        throw new NotImplementedException();
    }

    public async Task<PersonDto> AddAsync(CreatePersonInput dto)
    {
        var entity = _mapper.Map<CreatePersonInput, Person>(dto);
        entity.UserName = dto.Email.Split("@")[0];
        var insertPerson = await _userManager.CreateAsync(entity, dto.PasswordHash);
        
        if (insertPerson.Succeeded)
        {
            return this.GetBy(p=>entity.Email == entity.Email);
        }
        return null;
    }

    public async Task<LoginResultDto>? Login(LoginDto model)
    {
        var Person = await _userManager.FindByEmailAsync(model.Email);
        
        var checkSignIn = await _SignInManager.PasswordSignInAsync(Person.UserName, model.Password, model.RememberMe, true);

        var result = new LoginResultDto();
        if (checkSignIn.Succeeded)
        {
            var claim = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, Person?.UserName ?? ""),
                new Claim(ClaimTypes.NameIdentifier, Person?.Id ??""),
                new Claim(ClaimTypes.Email, Person?.Email ?? ""),
            };

            if (Person is not null)
            {
                var roles = await _userManager.GetRolesAsync(Person);

                roles.ToList().ForEach(r =>
                {
                    claim.Add(new Claim(ClaimTypes.Role, r));
                    result.Roles.Add(r);
                });
                
            }

            var claims = new ClaimsIdentity(claim);
            var key = Encoding.ASCII.GetBytes(Configuration["JWTSettings:Key"]);
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(int.Parse(Configuration["JWTSettings:expiryInDays"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var tokenString = jwtTokenHandler.WriteToken(token);
            result.UserId = Person.Id;
            result.Token = tokenString;
            result.FullName = Person.FullName;
            result.ExpiresIn = tokenDescriptor.Expires;

        }
        return result;
    }
    
    public int Count(Expression<Func<Person, bool>> expression)
    {
        return _unitOfWork.PersonRepo.Count(expression);
    }
    
    public async Task<OperationResult> ChangePasswordAsync(string PersonId, string currentPassword, string newPassword)
    {
        var selectedPerson = await _userManager.FindByIdAsync(PersonId);
        if (selectedPerson != null)
        {
            var result = await _userManager.ChangePasswordAsync(selectedPerson, currentPassword, newPassword);
            if (result.Succeeded)
            {
                return OperationResult.Succeeded();
            }
        }
        return OperationResult.Failed();
    }
    
    public async Task<PersonDto> Update(CreatePersonInput dto, string id)
    {
        var obj = _mapper.Map<Person>(dto);
        obj.Id = id;
        var result = _unitOfWork.PersonRepo.Update(obj);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<PersonDto>(result.Entity);
    }

    public bool Remove(Person entity)
    {
        _unitOfWork.PersonRepo.Remove(entity);
        _unitOfWork.SaveChanges();
        return true;
    }

    public async Task<IEnumerable<PersonDto>> GetAll(Expression<Func<Person, bool>> expression, int? take, int? skip,
        Expression<Func<Person, object>> orderBy, string orderDirection = Constanties.ORDERASC, params string[] includes)
    {
        var data = _unitOfWork.PersonRepo.Get(expression: expression, take: take, skip: skip, orderby: orderBy,
            orderbyDirection: orderDirection, include: includes);
        return _mapper.Map<IEnumerable<PersonDto>>(data);
    }

    public async Task<IEnumerable<PersonDto>> GetAll(Expression<Func<Person, bool>> expression, params string[] includes)
    {
        var data = _unitOfWork.PersonRepo.Get(expression: expression, include: includes);
        return _mapper.Map<IEnumerable<PersonDto>>(data);
    }

    public PersonDto GetBy(Expression<Func<Person, bool>> expression)
    {
        var obj = _unitOfWork.PersonRepo.GetBy(expression);
        return _mapper.Map<PersonDto>(obj);
    }

    public async Task<PersonDto> GetById(string Id)
    {
        var obj = await _unitOfWork.PersonRepo.GetById(Id);
        return _mapper.Map<PersonDto>(obj);
    }
    public async Task<Person> GetModelById(string Id)
    {
        return await _unitOfWork.PersonRepo.GetById(Id);
    }
    public bool IsExisted(Expression<Func<Person, bool>> expression)
    {
        return _unitOfWork.PersonRepo.IsExisted(expression);
    }
    public async Task<bool> IsExistedAsync(Expression<Func<Person, bool>> expression)
    {
        return await _unitOfWork.PersonRepo.IsExistedAsync(expression);
    }
    public async Task<PersonDto> UpdateById(string Id, CreatePersonInput dto)
    {
        var obj = await GetModelById(Id);
        obj.Id = Id;
        var result = _unitOfWork.PersonRepo.Update(obj);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<PersonDto>(result.Entity);
    }
}


